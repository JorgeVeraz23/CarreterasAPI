using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface IServicioRepositorio : IRepositorio<Servicio>
    {
        Task<Servicio> Actualizar(Servicio entidad);

    }
}
