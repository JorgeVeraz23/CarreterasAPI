
using APICarreteras.Models;
using APICarreteras.Repository;

namespace APICarreteras.Repository.IRepositorio
{
    public interface ICantonRepositorio : CantonRepositorio<Canton>
    {
        Task<Canton> Actualizar(Canton entidad);
    }
}
