using Microsoft.AspNetCore.Mvc;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Utilies;

namespace SistemaMedico.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<PagedResult<AdminModel>> All([FromQuery] int pageNumber, [FromQuery] int pageSize);
        Task<PagedResult<AdminModel>> Filter(string search, [FromQuery] int pageNumber, [FromQuery] int pageSize);
        Task<AdminModel> Search(int id);
        Task<AdminModel> Add(AdminModel Admin);
        Task<AdminModel> Att(AdminModel Admin, int id);
        Task<bool> Destroy(int id);
    }
}
