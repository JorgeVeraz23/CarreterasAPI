using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface ITramoRepositorio : IRepositorio<Tramo>
    {
        Task<Tramo> Actualizar(Tramo entidad);
    }
}
