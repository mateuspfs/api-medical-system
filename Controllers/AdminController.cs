using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Repositories.Interfaces;
using SistemaMedico.Utilies;

namespace SistemaMedico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _AdminRepository;
        private readonly IMapper _mapper;

        public AdminController(IAdminRepository AdminRepository, IMapper mapper)
        {
            _AdminRepository = AdminRepository;
            _mapper = mapper;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<AdminModel>>> All([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var Admins = await _AdminRepository.All(pageNumber, pageSize);
            return Ok(Admins);
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult<PagedResult<AdminModel>>> Filter(string search, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var Admins = await _AdminRepository.Filter(search, pageNumber, pageSize);
            return Ok(Admins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminModel>> Search(int id)
        {
            AdminModel Admin = await _AdminRepository.Search(id);
            return Ok(Admin);
        }

        [HttpPost]
        public async Task<ActionResult<AdminModel>> Add(AdminModel admin)
        {
            AdminModel newAdmin = await _AdminRepository.Add(admin);
            return Ok(newAdmin);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AdminModel>> Att(AdminModel admin, int id)
        {
            AdminModel AdminAtt = await _AdminRepository.Att(admin, id);
            return Ok(AdminAtt);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AdminModel>> Destroy(int id)
        {
            bool destroy = await _AdminRepository.Destroy(id);
            return Ok(destroy);
        }
    }
}
