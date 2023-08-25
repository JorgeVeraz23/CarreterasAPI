using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface IiluminacionRepositorio : IRepositorio<Iluminacion>
    {
        Task<Iluminacion> Actualizar(Iluminacion entidad);
    }
}
