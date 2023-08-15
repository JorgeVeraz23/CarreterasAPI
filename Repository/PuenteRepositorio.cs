using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class PuenteRepositorio : Repositorio<Puente>, IPuenteRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public PuenteRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Puente> Actualizar(Puente entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Puentes.Update(entidad);
            _db.SaveChangesAsync();
            return entidad;
        }
    }
}
