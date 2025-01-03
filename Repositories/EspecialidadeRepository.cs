using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMedico.Data;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Utilies;
using System;

namespace SistemaMedico.Repositories.Interfaces
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        private readonly SistemaMedicoDBContex _dbContext;

        public EspecialidadeRepository(SistemaMedicoDBContex sistemaMedicoDBContex)
        {
            _dbContext = sistemaMedicoDBContex;
        }
        public async Task<List<EspecialidadeModel>> All()
        {
            return await _dbContext.Especialidades.ToListAsync();
        }

        public async Task<PagedResult<EspecialidadeDTO>> paginateAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var Especialidades = await _dbContext.Especialidades
                                    .Include(t => t.Tratamentos)
                                    .Include(d => d.DoutorEspecialidades)
                                    .ToListAsync();

            var totalItems = Especialidades.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var paginatedEspecialidades = Especialidades
                .OrderByDescending(d => d.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var especialidadeDTOs = paginatedEspecialidades.Select(especialidade =>
            {
                return new EspecialidadeDTO
                {
                    Id = especialidade.Id,
                    Codigo = especialidade.Codigo,
                    Nome = especialidade.Nome,
                    CountTratamentos = especialidade.Tratamentos.Count,
                    CountDoutores = especialidade.DoutorEspecialidades.Count
                };
            }).ToList();

            var result = new PagedResult<EspecialidadeDTO>
            {
                TotalPages = totalPages,
                Items = especialidadeDTOs
            };

            return result;
        }
        public async Task<PagedResult<EspecialidadeDTO>> Filter(string? search, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            IQueryable<EspecialidadeModel> query = _dbContext.Especialidades
                            .Include(e => e.Tratamentos)
                            .Include(e => e.DoutorEspecialidades)
                                .ThenInclude(de => de.Doutor);

            // Aplicar filtros conforme necessário
            if (!string.IsNullOrEmpty(search))
                query = query.Where(e => e.Nome.Contains(search) || e.Codigo.Contains(search));

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var paginatedEspecialidades = await query
                .OrderByDescending(d => d.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var especialidadeDTOs = paginatedEspecialidades.Select(especialidade =>
            {
                return new EspecialidadeDTO
                {
                    Id = especialidade.Id,
                    Codigo = especialidade.Codigo,
                    Nome = especialidade.Nome,
                    CountTratamentos = especialidade.Tratamentos.Count,
                    CountDoutores = especialidade.DoutorEspecialidades.Count
                };
            }).ToList();

            var result = new PagedResult<EspecialidadeDTO>
            {
                TotalPages = totalPages,
                Items = especialidadeDTOs
            };

            return result;
        }

        public async Task<EspecialidadeModel> Search(int id)
        {
            return await _dbContext.Especialidades
                .Include(t => t.Tratamentos)
                .Include(d => d.DoutorEspecialidades)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EspecialidadeModel> Add(string nome)
        {
            var especialidade = new EspecialidadeModel
            {
                Codigo = GenerateRandomCode(),
                Nome = nome
            };

            while (await _dbContext.Especialidades.AnyAsync(p => p.Codigo == especialidade.Codigo))
            {
                especialidade.Codigo = GenerateRandomCode();
            }

            await _dbContext.Especialidades.AddAsync(especialidade);
            await _dbContext.SaveChangesAsync();

            return especialidade;
        }

        public async Task<EspecialidadeModel> Att(string nome, int id)
        {
            EspecialidadeModel especialidadeSearch = await Search(id) ?? throw new Exception($"Doutor para o ID: {id} não foi encontrado no banco!");

            especialidadeSearch.Nome = nome;

            _dbContext.Especialidades.Update(especialidadeSearch);
            await _dbContext.SaveChangesAsync();

            return especialidadeSearch;
        }

        public async Task<bool> Destroy(int id)
        {
            EspecialidadeModel especialidadeSearch = await Search(id) ?? throw new Exception($"Especialidade para o ID: {id} não foi encontrada no banco!");

            _dbContext.Especialidades.Remove(especialidadeSearch);

            await _dbContext.SaveChangesAsync();

            List<DoutorModel> doutoresSemEspecialidades = await _dbContext.Doutores
                .Where(d => !d.DoutorEspecialidades.Any())
                .ToListAsync();

            _dbContext.Doutores.RemoveRange(doutoresSemEspecialidades);

            await _dbContext.SaveChangesAsync();

            return true;
        }
        private string GenerateRandomCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
