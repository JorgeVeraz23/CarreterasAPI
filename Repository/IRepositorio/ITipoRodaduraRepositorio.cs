using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface ITipoRodaduraRepositorio : IRepositorio<TipoRodadura>
    {
        Task<TipoRodadura> Actualizar(TipoRodadura entidad);
    }
}
