using APICarreteras.Models;

namespace APICarreteras.Repository.IRepositorio
{
    public interface ICalendarioDeActuacionesRepositorio : IRepositorio<CalendarioDeActuacione>
    {
        Task<CalendarioDeActuacione> Actualizar(CalendarioDeActuacione entidad);
    }
}
