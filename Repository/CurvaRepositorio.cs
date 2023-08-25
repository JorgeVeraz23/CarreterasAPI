using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class CurvaRepositorio : Repositorio<Curva>, ICurvaRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public CurvaRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Curva> Actualizar(Curva entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Curvas.Update(entidad);
            _db.SaveChangesAsync();
            return entidad;
        }
    }
}
