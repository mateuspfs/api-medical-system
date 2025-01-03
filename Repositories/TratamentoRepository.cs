using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaMedico.Data;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Utilies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaMedico.Repositories.Interfaces
{
    public class TratamentoRepository : ITratamentoRepository
    {
        private readonly SistemaMedicoDBContex _dbContext;

        public TratamentoRepository(SistemaMedicoDBContex sistemaMedicoDBContex)
        {
            _dbContext = sistemaMedicoDBContex;
        }

        public async Task<PagedResult<TratamentoDTO>> Filter(string? filterNome, string? filterEspecialidade, int pageNumber, int pageSize)
        {
            var query = _dbContext.Tratamentos
                .Select(t => new TratamentoDTO
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    Tempo = t.Tempo,
                    NomeEspecialidade = t.Especialidade.Nome,
                    EspecialidadeId = t.EspecialidadeId,
                    Etapas = t.Etapas.Select(et => new EtapaDTO { Id = et.Id, Titulo = et.Titulo, Descricao = et.Descricao }).ToList()
                });

            if (!string.IsNullOrEmpty(filterNome))
                query = query.Where(t => t.Nome.Contains(filterNome));;

            if (!string.IsNullOrEmpty(filterEspecialidade))
            {
                query = query.Where(t => t.NomeEspecialidade.Contains(filterEspecialidade));
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var paginatedTratamentoes = await query
                .OrderByDescending(d => d.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<TratamentoDTO>
            {
                TotalPages = totalPages,
                Items = paginatedTratamentoes
            };

            return result;
        }

        public async Task<PagedResult<TratamentoDTO>> All([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var Tratamentos = await _dbContext.Tratamentos
                            .Select(t => new TratamentoDTO
                            {
                                Id = t.Id,
                                Nome = t.Nome,
                                Tempo = t.Tempo,
                                NomeEspecialidade = t.Especialidade.Nome,
                                EspecialidadeId = t.EspecialidadeId,
                                Etapas = t.Etapas.Select(et => new EtapaDTO { Id = et.Id, Titulo = et.Titulo, Descricao = et.Descricao }).ToList()
                            })
                            .ToListAsync();

            var totalItems = Tratamentos.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var paginatedTratamento = Tratamentos
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PagedResult<TratamentoDTO>
            {
                TotalPages = totalPages,
                Items = paginatedTratamento
            };

            return result;
        }

        public async Task<TratamentoModel> Search(int id)
        {
            return await _dbContext.Tratamentos
                .Include(e => e.Especialidade)
                .Include(et => et.Etapas)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TratamentoModel> Add(TratamentoModel tratamento)
        {
            var especialidade = await _dbContext.Especialidades.FindAsync(tratamento.EspecialidadeId) ?? throw new ArgumentException("Especialidade não encontrada.");
            tratamento.Especialidade = especialidade;

            especialidade.Tratamentos ??= new List<TratamentoModel>();

            if (!especialidade.Tratamentos.Any(t => t.Id == tratamento.Id))
            {
                especialidade.Tratamentos.Add(tratamento);
            }

            await _dbContext.Tratamentos.AddAsync(tratamento);
            await _dbContext.SaveChangesAsync();

            return tratamento;
        }

        public async Task<TratamentoModel> Att(TratamentoModel tratamento, int id)
        {
            TratamentoModel TratamentoSearch = await Search(id) ?? throw new Exception($"Tratamento para o ID: {id} não foi encontrado no banco!");
           
            var especialidade = await _dbContext.Especialidades.FindAsync(tratamento.EspecialidadeId) ?? throw new ArgumentException("Especialidade não encontrada.");

            var oldEspecialidade = await _dbContext.Especialidades.FindAsync(TratamentoSearch.EspecialidadeId);
            oldEspecialidade?.Tratamentos.Remove(TratamentoSearch);

            especialidade.Tratamentos ??= new List<TratamentoModel>();

            TratamentoSearch.Nome = tratamento.Nome;
            TratamentoSearch.Tempo = tratamento.Tempo;
            TratamentoSearch.EspecialidadeId = tratamento.EspecialidadeId;
            TratamentoSearch.Especialidade = especialidade;

            if (!especialidade.Tratamentos.Any(t => t.Id == TratamentoSearch.Id))
            {
                especialidade.Tratamentos.Add(TratamentoSearch);
            }

            _dbContext.Tratamentos.Update(TratamentoSearch);
            await _dbContext.SaveChangesAsync();

            return TratamentoSearch;
        }

        public async Task<bool> Destroy(int id)
        {
            TratamentoModel TratamentoSearch = await Search(id) ?? throw new Exception($"Tratamento para o ID: {id} não foi encontrado no banco!");
            
            _dbContext.Tratamentos.Remove(TratamentoSearch);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

