using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface ICunetaRepositorio : IRepositorio<Cunetum>
    {
        Task<Cunetum> Actualizar(Cunetum entidad);
    }
}
