using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface ICamaraDeSeguridadRepositorio: IRepositorio<CamarasDeSeguridad>
    {
        Task<CamarasDeSeguridad> Actualizar(CamarasDeSeguridad entidad);
    }
}
