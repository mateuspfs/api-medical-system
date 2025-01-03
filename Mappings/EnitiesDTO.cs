using AutoMapper;
using SistemaMedico.DTOs;
using SistemaMedico.Models;

namespace SistemaMedico.Mappings
{
    public class EnitiesDTO : Profile 
    {
        public EnitiesDTO() {
            CreateMap<DoutorModel, DoutorDTO>().ReverseMap();
            CreateMap<DoutorModel, DoutorDTOView>().ReverseMap();
            CreateMap<PacienteModel, PacienteDTO>().ReverseMap();
            CreateMap<TratamentoModel, TratamentoDTO>().ReverseMap();
            CreateMap<TratamentoModel, TratamentoAddDTO>().ReverseMap();
            CreateMap<TratamentoPacienteModel, TratamentoPacienteListDTO>().ReverseMap();
            CreateMap<PagamentoModel, PagamentoDTO>().ReverseMap();
            CreateMap<EspecialidadeModel, EspecialidadeDTO>().ReverseMap();
            CreateMap<DoutorEspecialidadeModel, DoutorEspecialidadeDTO>().ReverseMap();
            CreateMap<AdminModel, AdminModel>().ReverseMap();
            CreateMap<EtapaModel, EtapaDTO>().ReverseMap();
            CreateMap<AuditoriaModel, AuditoriaDTO>().ReverseMap();
            CreateMap<PagamentoEtapaModel, PagamentoEtapaDTO>().ReverseMap();
        }
    }
}