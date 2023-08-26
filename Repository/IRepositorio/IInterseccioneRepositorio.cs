using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface IInterseccioneRepositorio : IRepositorio<Interseccione>
    {
        Task<Interseccione> Actualizar(Interseccione entidad);
    }
}
