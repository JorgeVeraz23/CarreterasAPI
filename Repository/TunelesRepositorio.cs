using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class TunelesRepositorio : Repositorio<Tunele>, ITuneleRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public TunelesRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Tunele> Actualizar(Tunele entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Tuneles.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
