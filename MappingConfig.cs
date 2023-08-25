using APICarreteras.Models;
using APICarreteras.Models.Dto;
using APICarreteras.Repository;
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
            
            CreateMap<Tramo, TramoDto>().ReverseMap();
            CreateMap<Tramo, TramoCreateDto>().ReverseMap();
            CreateMap<Tramo, TramoUpdateDto>().ReverseMap();

            CreateMap<Alcantarillado, AlcantarilladoDto>().ReverseMap();
            CreateMap<Alcantarillado, AlcantarilladoCreateDto>().ReverseMap();
            CreateMap<Alcantarillado, AlcantarilladoUpdateDto>().ReverseMap();

            CreateMap<Accesorio, AccesorioDto>().ReverseMap();
            CreateMap<Accesorio, AccesorioCreateDto>().ReverseMap();
            CreateMap<Accesorio, AccesorioUpdateDto>().ReverseMap();

            CreateMap<Puente, PuenteDto>().ReverseMap();   
            CreateMap<Puente, PuenteCreateDto>().ReverseMap();
            CreateMap<Puente, PuenteUpdateDto>().ReverseMap();
            
            CreateMap<Talud,  TaludDto>().ReverseMap();
            CreateMap<Talud, TaludCreateDto>().ReverseMap();
            CreateMap<Talud, TaludUpdateDto>().ReverseMap();

            CreateMap<Curva, CurvaDto>().ReverseMap();
            CreateMap<Curva, CurvaCreateDto>().ReverseMap();
            CreateMap<Curva, CurvaUpdateDto>().ReverseMap();

            CreateMap<Cunetum, CunetaDto>().ReverseMap();
            CreateMap<Cunetum, CunetaCreateDto>().ReverseMap();
            CreateMap<Cunetum, CunetaUpdateDto>().ReverseMap();

            CreateMap<Tunele, TunelesDto>().ReverseMap();
            CreateMap<Tunele, TunelesCreateDto>().ReverseMap();
            CreateMap<Tunele, TunelesUpdateDto>().ReverseMap();

            CreateMap<Iluminacion, IluminacionDto>().ReverseMap();
            CreateMap<Iluminacion, IluminacionCreateDto>().ReverseMap();
            CreateMap<Iluminacion, IluminacionUpdateDto>().ReverseMap();

            CreateMap<Servicio, ServiciosDto>().ReverseMap();
            CreateMap<Servicio, ServicioCreateDto>().ReverseMap();
            CreateMap<Servicio, ServicioUpdateDto>().ReverseMap();

            CreateMap<CamarasDeSeguridad, CamarasDeSeguridadDto>().ReverseMap();
            CreateMap<CamarasDeSeguridad, CamarasDeSeguridadCreateDto>().ReverseMap();
            CreateMap<CamarasDeSeguridad, CamarasDeSeguridadUpdateDto>().ReverseMap();

        }
    }
}
