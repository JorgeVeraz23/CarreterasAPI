using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface ITuneleRepositorio : IRepositorio<Tunele>
    {
        Task<Tunele> Actualizar(Tunele entidad);
    }
}
