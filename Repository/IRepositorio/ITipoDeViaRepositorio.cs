using APICarreteras.Models;
using APICarreteras.Repository;

namespace APICarreteras.Repository.IRepositorio
{
    public interface ITipoDeViaRepositorio : IRepositorio<TipoDeVium>
    {
        Task<TipoDeVium> Actualizar(TipoDeVium entidad);
    }
}

