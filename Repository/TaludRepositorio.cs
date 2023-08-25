using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class TaludRepositorio : Repositorio<Talud>, ITaludRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public TaludRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Talud> Actualizar(Talud entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Taluds.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
