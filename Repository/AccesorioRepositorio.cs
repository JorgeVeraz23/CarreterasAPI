using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class AccesorioRepositorio : Repositorio<Accesorio>, IAccesorioRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public AccesorioRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Accesorio> Actualizar(Accesorio entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Accesorios.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
