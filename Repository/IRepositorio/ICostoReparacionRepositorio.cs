using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface ICostoReparacionRepositorio : IRepositorio<CostoReparacion>
    {
        Task<CostoReparacion> Actualizar(CostoReparacion entidad);
    }
}
