using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class DanoRepositorio : Repositorio<Dano>, IDanosRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public DanoRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Dano> Actualizar(Dano entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Danos.Update(entidad);
            _db.SaveChangesAsync();
            return entidad;
        }
    }
}
