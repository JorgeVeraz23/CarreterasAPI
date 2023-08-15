using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface IPuenteRepositorio : IRepositorio<Puente>
    {
        Task<Puente> Actualizar(Puente entidad);
    }
}
