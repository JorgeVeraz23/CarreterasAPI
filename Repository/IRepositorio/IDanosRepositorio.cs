using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface IDanosRepositorio : IRepositorio<Dano>
    {
        Task<Dano> Actualizar(Dano entidad);

    }
}
