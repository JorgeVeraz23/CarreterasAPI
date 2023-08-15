using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface IAlcantarilladoRepositorio : IRepositorio<Alcantarillado>
    {
        Task<Alcantarillado> Actualizar(Alcantarillado entidad);
    }
}
