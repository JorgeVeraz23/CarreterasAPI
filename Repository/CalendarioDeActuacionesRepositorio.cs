using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class CalendarioDeActuacionesRepositorio : Repositorio<CalendarioDeActuacione>, ICalendarioDeActuacionesRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public CalendarioDeActuacionesRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<CalendarioDeActuacione> Actualizar(CalendarioDeActuacione entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.CalendarioDeActuaciones.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
