using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaMedico.Data;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Repositories.Interfaces;
using SistemaMedico.Utilies;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SistemaMedico.Repositories
{
    public class TratamentoPacienteRepository : ITratamentoPacienteRepository
    {
        private readonly SistemaMedicoDBContex _dbContext;
        private readonly IMapper _mapper;
        private readonly string _pagSeguroToken = "E9A92DDA91BB426888EB8B2C2440585C";

        public TratamentoPacienteRepository(SistemaMedicoDBContex sistemaMedicoDBContex, IMapper mapper)
        {
            _dbContext = sistemaMedicoDBContex;
            _mapper = mapper;
        }

        public async Task<PagedResult<TratamentoPacienteListDTO>> DoctorPacientes(string token, int pageNumber, int pageSize, string? search, string? filterTratamento)
        {
            int doutorId = ExtrairDoutorIdDoToken(token);

            var query = _dbContext.TratamentosPacientes
                .Include(tp => tp.Paciente)
                .Include(tp => tp.Etapa)
                    .ThenInclude(e => e.Tratamento)
                        .ThenInclude(t => t.Especialidade)  
                .Where(tp => tp.Etapa.Tratamento.Especialidade.DoutorEspecialidades.Any(de => de.DoutorId == doutorId))
                .Where(tp => !tp.Status);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(tp => tp.Paciente.Codigo.Contains(search) || tp.Paciente.Nome.Contains(search));
            }

            if (!string.IsNullOrEmpty(filterTratamento))
            {
                int tratamentoId = Convert.ToInt32(filterTratamento);

                query = query.Where(tp => tp.Etapa.TratamentoId == tratamentoId);
            }


            var tratamentosPacientesDTO = await query
                .Select(tp => new TratamentoPacienteListDTO
                {
                    Id = tp.Id,
                    Created_at = tp.Created_at,
                    Finalizado = tp.Status,
                    Paciente = new PacienteDTO
                    {
                        Id = tp.Paciente.Id,
                        Codigo = tp.Paciente.Codigo,
                        Nome = tp.Paciente.Nome,
                        Email = tp.Paciente.Email,
                        Telefone = tp.Paciente.Telefone,
                        Cpf = tp.Paciente.Cpf,
                        Endereco = tp.Paciente.Endereco
                    },
                    Tratamento = new TratamentoDTO
                    {
                        Id = tp.Etapa.Tratamento.Id,
                        Nome = tp.Etapa.Tratamento.Nome,
                        Tempo = tp.Etapa.Tratamento.Tempo,
                        EspecialidadeId = tp.Etapa.Tratamento.Especialidade.Id,
                        NomeEspecialidade = tp.Etapa.Tratamento.Especialidade.Nome
                    },
                    EtapaAtual = new EtapaDTO
                    {
                        Id = tp.Etapa.Id,
                        Numero = tp.Etapa.Numero,
                        Titulo = tp.Etapa.Titulo,
                        Descricao = tp.Etapa.Descricao
                    },
                }).ToListAsync();

            var totalItems = await _dbContext.TratamentosPacientes
                .Where(tp => tp.Etapa.Tratamento.Especialidade.DoutorEspecialidades
                    .Any(de => de.DoutorId == doutorId))
                .CountAsync();

            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var paginatedPacientes = tratamentosPacientesDTO
                .OrderByDescending(d => d.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PagedResult<TratamentoPacienteListDTO>
            {
                TotalPages = totalPages,
                Items = paginatedPacientes
            };

            return result;
        }

        public async Task<TratamentoPacienteModel> Add(string token, TratamentoPacienteAddDTO tratamento)
        {
            var doctorId = ExtrairDoutorIdDoToken(token);
            var doctor = await SearchDoutor(doctorId) ?? throw new ArgumentException("Doutor não encontrado.");
            var paciente = await SearchPaciente(tratamento.Codigo) ?? throw new ArgumentException("Paciente não encontrado.");
            var tratamentoModel = await SearchTratamento(tratamento.TratamentoId) ?? throw new ArgumentException("Tratamento não encontrado.");

            if (_dbContext.TratamentosPacientes.Any(tp =>
                tp.PacienteId == paciente.Id &&
                tp.Etapa.TratamentoId == tratamentoModel.Id &&
                !tp.Status))
            {
                throw new ArgumentException("O paciente já está realizando este tratamento.");
            }

            var primeiraEtapa = tratamentoModel.Etapas.OrderBy(et => et.Id).FirstOrDefault() ?? throw new ArgumentException("Tratamento não possui etapas.");

            var tratamentoPaciente = new TratamentoPacienteModel
            {
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
                Status = false,
                Paciente = paciente,
                Etapa = primeiraEtapa
            };

            _dbContext.TratamentosPacientes.Add(tratamentoPaciente);
            await _dbContext.SaveChangesAsync();

            var pagamento = new PagamentoModel
            {
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
                TratamentoPacienteId = tratamentoPaciente.Id,
                TratamentoPaciente = tratamentoPaciente
            };

            _dbContext.Pagamentos.Add(pagamento);
            await _dbContext.SaveChangesAsync();

            
            var auditoria = new AuditoriaModel
            {
                Acao = "Iniciou o tratamento",
                DataHora = DateTime.Now,
                TratamentoPacienteId = tratamentoPaciente.Id,
                TratamentoPaciente = tratamentoPaciente,
                DoutorId = doctor.Id,
                Doutor = doctor
            };

            _dbContext.Auditorias.Add(auditoria);
            await _dbContext.SaveChangesAsync();
            

            if (tratamento.Arquivos != null && tratamento.Arquivos.Count > 0)
            {
                foreach (var arquivo in tratamento.Arquivos)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(arquivo.FileName);
                    var filePath = Path.Combine("Storage/TratamentosPacientesDocs", uniqueFileName);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await arquivo.CopyToAsync(fileStream);
                    }

                    var arquivoModel = new ArquivosTratamentoPacienteModel
                    {
                        Nome = uniqueFileName,
                        DataUpload = DateTime.Now,
                        TratamentoPaciente = tratamentoPaciente
                    };

                    _dbContext.ArquivosTratamentoPaciente.Add(arquivoModel);
                }

                await _dbContext.SaveChangesAsync();
            }

            return tratamentoPaciente;
        }

        public async Task<TratamentoPacienteModel> Att(List<IFormFile> Arquivos, int id, string token, int? novaEtapaId)
        {
            var doctorId = ExtrairDoutorIdDoToken(token);
            var doctor = await SearchDoutor(doctorId) ?? throw new ArgumentException("Doutor não encontrado.");
            var TratamentoSearch = await Search(id) ?? throw new Exception($"Tratamento não encontrado!");

            if (novaEtapaId.HasValue)
            {
                TratamentoSearch.EtapaId = novaEtapaId.Value;
                TratamentoSearch.Updated_at = DateTime.Now;

                _dbContext.Entry(TratamentoSearch).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                var auditoriaadd = new AuditoriaModel
                {
                    Acao = "Etapa Atualizada",
                    DataHora = DateTime.Now,
                    TratamentoPacienteId = TratamentoSearch.Id,
                    TratamentoPaciente = TratamentoSearch,
                    DoutorId = doctorId,
                    Doutor = doctor
                };

                _dbContext.Auditorias.Add(auditoriaadd);
                await _dbContext.SaveChangesAsync();
            }

            if (Arquivos != null && Arquivos.Count > 0)
            {
                foreach (var arquivo in Arquivos)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(arquivo.FileName);
                    var filePath = Path.Combine("Storage/TratamentosPacientesDocs", uniqueFileName);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await arquivo.CopyToAsync(fileStream);
                    }

                    var arquivoModel = new ArquivosTratamentoPacienteModel
                    {
                        Nome = uniqueFileName,
                        DataUpload = DateTime.Now,
                        TratamentoPacienteId = TratamentoSearch.Id,
                    };

                    _dbContext.ArquivosTratamentoPaciente.Add(arquivoModel);
                }

                await _dbContext.SaveChangesAsync();

                var auditoria = new AuditoriaModel
                {
                    Acao = "Adicionou arquivos ao tratamento",
                    DataHora = DateTime.Now,
                    TratamentoPacienteId = TratamentoSearch.Id,
                    DoutorId = doctorId,
                };

                _dbContext.Auditorias.Add(auditoria);
                await _dbContext.SaveChangesAsync();
            }

            return TratamentoSearch;
        }

        public async Task<bool> Destroy(int id)
        {
            TratamentoPacienteModel TratamentoSearch = await Search(id) ?? throw new Exception($"Tratamento não encontrado!");

            _dbContext.TratamentosPacientes.Remove(TratamentoSearch);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<TratamentoPacienteViewDTO> GetTratamentoPaciente(string token, int tratamentoPacienteId)
        {
            var doutorId = ExtrairDoutorIdDoToken(token);

            var doutor = await _dbContext.Doutores
                .Include(d => d.DoutorEspecialidades)
                    .ThenInclude(de => de.Especialidade)
                .FirstOrDefaultAsync(d => d.Id == doutorId) ?? throw new Exception($"Doutor não encontrado!");

            var tratamentoPaciente = await _dbContext.TratamentosPacientes
                .Include(tp => tp.Paciente)
                .Include(tp => tp.Arquivos)
                .Include(tp => tp.Etapa)
                    .ThenInclude(e => e.Tratamento)
                          .ThenInclude(t => t.Etapas.OrderBy(e => e.Numero))
                .Include(tp => tp.Etapa)
                    .ThenInclude(e => e.Tratamento)
                        .ThenInclude(t => t.Especialidade)
                 .Include(tp => tp.Auditorias)
                 .Include(tp => tp.Pagamento)
                    .ThenInclude(pe => pe.PagamentoEtapas)
                .Where(tp => tp.Id == tratamentoPacienteId)
                .FirstOrDefaultAsync() ?? throw new Exception($"Tratamento não encontrado!");

            if (tratamentoPaciente.Pagamento != null)
            {
                foreach (var pagamento in tratamentoPaciente.Pagamento.PagamentoEtapas)
                {
                    await VerificarStatusPagamento(pagamento);
                }
            }

            var especialidadeIds = doutor.DoutorEspecialidades.Select(de => de.EspecialidadeId);
            if (!especialidadeIds.Contains(tratamentoPaciente.Etapa.Tratamento.EspecialidadeId))
            {
                throw new Exception($"Não aautorizado!");
            }

            var pacienteDTO = new PacienteDTO
            {
                Id = tratamentoPaciente.Paciente.Id,
                Codigo = tratamentoPaciente.Paciente.Codigo,
                Nome = tratamentoPaciente.Paciente.Nome,
                Email = tratamentoPaciente.Paciente.Email,
                Telefone = tratamentoPaciente.Paciente.Telefone,
                Cpf = tratamentoPaciente.Paciente.Cpf,
                Endereco = tratamentoPaciente.Paciente.Endereco
            };

            var tratamentoDTO = new TratamentoDTO
            {
                Id = tratamentoPaciente.Etapa.Tratamento.Id,
                Nome = tratamentoPaciente.Etapa.Tratamento.Nome,
                Tempo = tratamentoPaciente.Etapa.Tratamento.Tempo,
                NomeEspecialidade = tratamentoPaciente.Etapa.Tratamento.Especialidade.Nome,                
                EspecialidadeId = tratamentoPaciente.Etapa.Tratamento.Especialidade.Id,
                Etapas = tratamentoPaciente.Etapa.Tratamento.Etapas.OrderBy(e => e.Numero).Select(e => new EtapaDTO
                {
                    Id = e.Id,
                    Numero = e.Numero,
                    Titulo = e.Titulo,
                    Descricao = e.Descricao
                }).ToList()
            };

            var etapaAtualDTO = new EtapaDTO
            {
                Id = tratamentoPaciente.Etapa.Id,
                Numero = tratamentoPaciente.Etapa.Numero,
                Titulo = tratamentoPaciente.Etapa.Titulo,
                Descricao = tratamentoPaciente.Etapa.Descricao
            };

            var pagamentoDTO = new PagamentoDTO
            {
                Id = tratamentoPaciente.Pagamento != null ? tratamentoPaciente.Pagamento.Id : (int?)null,
                PagamentoEtapas = tratamentoPaciente.Pagamento != null ?
                tratamentoPaciente.Pagamento.PagamentoEtapas.Select(pagamentoEtapa => new PagamentoEtapaDTO
                {
                    Id = pagamentoEtapa.Id,
                    Valor = pagamentoEtapa.Valor,
                    Pago = pagamentoEtapa.Pago,
                    PagamentoId = pagamentoEtapa.PagamentoId,
                    EtapaId = pagamentoEtapa.EtapaId
                }).ToList() : null
            };

            var arquivosDTO = tratamentoPaciente.Arquivos.Select(a => new ArquivosTratamentoPacienteDTO
            {
                Id = a.Id,
                Nome = a.Nome,
                DataUpload = a.DataUpload
            }).ToList();

            var auditoriasDTO = tratamentoPaciente.Auditorias?.Select(a => new AuditoriaDTO
            {
                Id = a.Id,
                Acao = a.Acao,
                DataHora = a.DataHora,
                DoutorNome = a.Doutor?.Nome 
            }).ToList();

            var tratamentoPacienteDTO = new TratamentoPacienteViewDTO
            {
                Id = tratamentoPaciente.Id,
                Updated_at = tratamentoPaciente.Updated_at ?? tratamentoPaciente.Created_at,
                Created_at = tratamentoPaciente.Created_at,
                Finalizado = tratamentoPaciente.Status,
                Paciente = pacienteDTO,
                Tratamento = tratamentoDTO,
                EtapaAtual = etapaAtualDTO,
                Pagamento = pagamentoDTO,
                Auditorias = auditoriasDTO,
                Arquivos = arquivosDTO
            };

            return tratamentoPacienteDTO;
        }
        public async Task<FileStream> GetArquivo(string nomeArquivo)
        {
            var filePath = Path.Combine("Storage/TratamentosPacientesDocs", nomeArquivo);

            if (!File.Exists(filePath))
            {
                return null;
            }

            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }

        private async Task VerificarStatusPagamento(PagamentoEtapaModel pagamento)
        {
            if (!pagamento.Pago && !string.IsNullOrEmpty(pagamento.UrlCheck))
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _pagSeguroToken);

                var response = await client.GetAsync(pagamento.UrlCheck);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(responseBody);

                    if (jsonDocument.RootElement.TryGetProperty("status", out JsonElement status))
                    {
                        if (status.GetString() == "PAID")
                        {
                            pagamento.Pago = true;
                            _dbContext.PagamentoEtapas.Update(pagamento);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }
            }
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

            // Validar o token
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);

            // Extrair o ID do doutor do token
            var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            throw new Exception("Falha ao extrair o ID do doutor do token.");
        }

        private async Task<TratamentoPacienteModel> Search(int id)
        {
            return await _dbContext.TratamentosPacientes
                .Include(e => e.Paciente)
                .Include(et => et.Etapa)
                .Include(et => et.Arquivos)
                .FirstOrDefaultAsync(x => x.Id == id);
        } 

        private async Task<TratamentoModel> SearchTratamento(int id)
        {
            return await _dbContext.Tratamentos
               .Include(e => e.Especialidade)
               .Include(et => et.Etapas)
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<PacienteModel> SearchPaciente(string codigo)
        {
            return await _dbContext.Pacientes.FirstOrDefaultAsync(x => x.Codigo == codigo);
        }
        
        private async Task<DoutorModel> SearchDoutor(int id)
        {
            return await _dbContext.Doutores.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
