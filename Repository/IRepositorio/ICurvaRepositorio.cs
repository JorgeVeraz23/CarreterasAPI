using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface ICurvaRepositorio : IRepositorio<Curva>
    {
        Task<Curva> Actualizar(Curva entidad);
    }
}
