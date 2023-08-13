using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface ICarreteraRepositorio: IRepositorio<Carretera>
    {
        Task<Carretera> Actualizar(Carretera entidad);
    }
}
