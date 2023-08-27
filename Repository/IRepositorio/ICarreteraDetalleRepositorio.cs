using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface ICarreteraDetalleRepositorio : IRepositorio<CarreteraDetalle>
    {
        Task<CarreteraDetalle> Actualizar(CarreteraDetalle entidad);
    }
}
