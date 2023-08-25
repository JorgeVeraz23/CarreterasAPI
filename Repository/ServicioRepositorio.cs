using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class ServicioRepositorio : Repositorio<Servicio>, IServicioRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public ServicioRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Servicio> Actualizar(Servicio entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Servicios.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
