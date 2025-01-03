using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SistemaMedico.Data;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Utilies;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Uol.PagSeguro;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;

namespace SistemaMedico.Repositories.Interfaces
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly SistemaMedicoDBContex _dbContext;
        private readonly IOptions<EmailSettingsDTO> _emailSettings;
        private readonly PagSeguroSettingsDTO _pagSeguroSettings;
        private readonly IMapper _mapper;

        public PagamentoRepository(SistemaMedicoDBContex sistemaMedicoDBContex, IMapper mapper, IOptions<PagSeguroSettingsDTO> pagSeguroSettings)
        {
            _dbContext = sistemaMedicoDBContex;
            _mapper = mapper;
            _pagSeguroSettings = pagSeguroSettings.Value;
        }

        public async Task<PagamentoEtapaModel> Search(int id)
        {
            return await _dbContext.PagamentoEtapas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagamentoResponse> Add(string token, PagamentoEtapaDTO pagamentoDTO, int tratamentoId)
        {
            try
            {
                var doutorId = ExtrairDoutorIdDoToken(token);

                var existingPagamento = await _dbContext.PagamentoEtapas
                                        .FirstOrDefaultAsync(p =>
                                            p.EtapaId == pagamentoDTO.EtapaId &&
                                            p.PagamentoId == pagamentoDTO.PagamentoId);

                if (existingPagamento != null)
                {
                    throw new Exception($"Pagamento já emitido!");
                }

                TratamentoPacienteModel searchTP = await _dbContext.TratamentosPacientes
                                                    .Include(tp => tp.Paciente)
                                                    .Include(tp => tp.Etapa)
                                                        .ThenInclude(t => t.Tratamento)
                                                    .FirstOrDefaultAsync(x => x.Id == tratamentoId);

                var pagamentoModel = _mapper.Map<PagamentoEtapaModel>(pagamentoDTO);

                await _dbContext.PagamentoEtapas.AddAsync(pagamentoModel);
                await _dbContext.SaveChangesAsync();

                var pagamento = await _dbContext.PagamentoEtapas
                           .Include(p => p.Etapa)
                               .ThenInclude(e => e.Tratamento)
                           .FirstOrDefaultAsync(p => p.Id == pagamentoModel.Id);

                string cpfFormatado = string.Join("", System.Text.RegularExpressions.Regex.Split(searchTP.Paciente.Cpf, @"[^\d]"));
                string valorFormatado = (pagamento.Valor).ToString(); var expirationDate = DateTime.Now.AddDays(10);
                var expirationDateString = expirationDate.ToString("yyyy-MM-ddTHH:mm:sszzz");

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _pagSeguroSettings.Token);

                var requestBody = new
                {
                    customer = new
                    {
                        name = searchTP.Paciente.Nome,
                        email = searchTP.Paciente.Email,
                        tax_id = cpfFormatado
                    },
                    payment_methods = new object[]
                    {
                        new { type = "credit_card", brands = new[] { "mastercard" } },
                        new { type = "credit_card", brands = new[] { "visa" } },
                        new { type = "debit_card", brands = new[] { "visa" } },
                        new { type = "PIX" },
                        new { type = "BOLETO" }
                    },
                    reference_id = searchTP.Id.ToString(),
                    expiration_date = expirationDateString,
                    customer_modifiable = true,
                    items = new[]
                    {
                        new { reference_id = searchTP.Id, name = $"Pagamento da Etapa {searchTP.Etapa.Titulo} do Tratamento { searchTP.Etapa.Tratamento.Nome}", quantity = 1, unit_amount = valorFormatado}
                    },
                    additional_amount = 0,
                    discount_amount = 0,
                    payment_methods_configs = new[]
                    {
                        new
                        {
                            type = "credit_card",
                            config_options = new[]
                            {
                                new { option = "installments_limit", value = "1" }
                            }
                        }
                    },
                    soft_descriptor = "xxxx",
                    redirect_url = "https://pagseguro.uol.com.br",
                    return_url = "https://pagseguro.uol.com.br",
                    notification_urls = new[] { "https://pagseguro.uol.com.br" }
                };

                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestBody));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("https://sandbox.api.pagseguro.com/checkouts", content);

                // Verifica se a resposta não foi bem-sucedida
                if (!response.IsSuccessStatusCode)
                {
                    // Lança uma exceção personalizada com detalhes da resposta
                    throw new Exception($"Erro ao fazer a requisição ao serviço de pagamento: {response.StatusCode}");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                var Links = await GetLinks(responseBody);

                var emailService = new EmailService(_emailSettings);
                await emailService.SendPaymentLinkAsync(searchTP.Paciente.Email, Links[1]);

                pagamentoModel.UrlCheck = Links[0];
                _dbContext.PagamentoEtapas.Update(pagamentoModel);
                await _dbContext.SaveChangesAsync();

                var auditoria = new AuditoriaModel
                {
                    Acao = $"Emitido Pagamento para Etapa {pagamento.Etapa.Titulo}",
                    DataHora = DateTime.Now,
                    TratamentoPacienteId = searchTP.Id,
                    TratamentoPaciente = searchTP,
                    DoutorId = doutorId
                };

                _dbContext.Auditorias.Add(auditoria);
                await _dbContext.SaveChangesAsync();

                return new PagamentoResponse
                {
                    PagamentoEtapa = pagamentoDTO,
                    responsePagseguro = responseBody
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar pagamento: {ex.Message}");
                throw; 
            }
        }

        public async Task<PagamentoEtapaModel> Att(PagamentoEtapaModel pagamento, int id)
        {
            PagamentoEtapaModel pagamentoSearch = await Search(id) ?? throw new Exception($"Pagamento para o ID: {id} não foi encontrado no banco!");


            _dbContext.PagamentoEtapas.Update(pagamentoSearch);
            await _dbContext.SaveChangesAsync();
            return pagamentoSearch;
        }

        public async Task<bool> Destroy(int id)
        {
            PagamentoEtapaModel pagamentoSearch = await Search(id) ?? throw new Exception($"Pagamento para o ID: {id} não foi encontrado no banco!");

            _dbContext.PagamentoEtapas.Remove(pagamentoSearch);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task<string[]> GetLinks(string responseBody)
        {
            var getlinks = new List<string>();

            using (JsonDocument doc = JsonDocument.Parse(responseBody))
            {
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("links", out JsonElement links))
                {
                    foreach (JsonElement link in links.EnumerateArray())
                    {
                        if (link.TryGetProperty("href", out JsonElement href))
                        {
                            getlinks.Add(href.GetString());
                        }
                    }
                }
            }

            if (getlinks.Count == 0)
            {
                throw new InvalidOperationException("Nenhum link de pagamento encontrado na resposta do PagSeguro.");
            }

            return getlinks.ToArray();
        }

        private static int ExtrairDoutorIdDoToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("2o3@%7slh2kVdF9&nD$");

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);

            var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            throw new Exception("Falha ao extrair o ID do doutor do token.");
        }
    }
}
