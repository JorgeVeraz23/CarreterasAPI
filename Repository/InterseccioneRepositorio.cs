using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class InterseccioneRepositorio : Repositorio<Interseccione>, IInterseccioneRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public InterseccioneRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Interseccione> Actualizar(Interseccione entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Intersecciones.Update(entidad);
            _db.SaveChangesAsync();
            return entidad;
        }
    }
}
