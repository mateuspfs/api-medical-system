using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMedico.Data;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Utilies;

namespace SistemaMedico.Repositories.Interfaces
{
    public class AdminRepository : IAdminRepository
    {
        private readonly SistemaMedicoDBContex _dbContext;

        public AdminRepository(SistemaMedicoDBContex sistemaMedicoDBContex)
        {
            _dbContext = sistemaMedicoDBContex;
        }

        public async Task<PagedResult<AdminModel>> All([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var admins = await _dbContext.Admins.ToListAsync();
            var totalItems = admins.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var paginatedAdmins = admins
                .OrderByDescending(d => d.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PagedResult<AdminModel>
            {
                TotalPages = totalPages,
                Items = paginatedAdmins
            };

            return result;
        }

        public async Task<PagedResult<AdminModel>> Filter(string search, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var admins = await _dbContext.Admins
                         .Where(a => a.Name.ToLower().Contains(search) || a.Email.ToLower().Contains(search))
                         .ToListAsync();

            var totalItems = admins.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var paginatedAdmins = admins
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PagedResult<AdminModel>
            {
                TotalPages = totalPages,
                Items = paginatedAdmins
            };

            return result;
        }

        public async Task<AdminModel> Search(int id)
        {
            return await _dbContext.Admins.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<AdminModel> Add(AdminModel Admin)
        {
            await _dbContext.Admins.AddAsync(Admin);
            await _dbContext.SaveChangesAsync();

            return Admin;
        }

        public async Task<AdminModel> Att(AdminModel Admin, int id)
        {
            AdminModel AdminSearch = await Search(id) ?? throw new Exception($"Doutor para o ID: {id} não foi encontrado no banco!");

            AdminSearch.Name = Admin.Name;
            AdminSearch.Email = Admin.Email;

            _dbContext.Admins.Update(AdminSearch);
            await _dbContext.SaveChangesAsync();
            return AdminSearch;
        }

        public async Task<bool> Destroy(int id)
        {
            AdminModel AdminSearch = await Search(id) ?? throw new Exception($"Doutor para o ID: {id} não foi encontrado no banco!");

            _dbContext.Admins.Remove(AdminSearch);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
