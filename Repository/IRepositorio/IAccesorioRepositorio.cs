using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface IAccesorioRepositorio : IRepositorio<Accesorio>
    {
        Task<Accesorio> Actualizar(Accesorio entidad);
    }
}
