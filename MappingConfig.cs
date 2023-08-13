using APICarreteras.Models;
using APICarreteras.Models.Dto;
using AutoMapper;



namespace APICarreteras
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Canton, CantonDto>();
            CreateMap<CantonDto, Canton>();

            CreateMap<Canton, CantonCreateDto>().ReverseMap();
            CreateMap<CantonUpdateDto, Canton>().ReverseMap();

            CreateMap<TipoDeVium, TipoDeViaDto>().ReverseMap();
            CreateMap<TipoDeVium, TipoDeViaCreateDto>().ReverseMap();
            CreateMap<TipoDeVium, TipoDeViaUpdateDto>().ReverseMap();

            CreateMap<Carretera, CarreteraDto>().ReverseMap();
            CreateMap<Carretera, CarreteraCreateDto>().ReverseMap();
            CreateMap<Carretera, CarreteraUpdateDto>().ReverseMap();

            

        }
    }
}
