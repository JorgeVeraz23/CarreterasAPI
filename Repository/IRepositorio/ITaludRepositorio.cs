using APICarreteras.Models;
using APICarreteras.Repository;

namespace APICarreteras.Repository.IRepositorio
{
    public interface ITaludRepositorio : IRepositorio<Talud>
    {
        Task<Talud> Actualizar(Talud entidad);
    }
}
