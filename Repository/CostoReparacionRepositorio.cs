using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class CostoReparacionRepositorio : Repositorio<CostoReparacion>, ICostoReparacionRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public CostoReparacionRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<CostoReparacion> Actualizar(CostoReparacion entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.CostoReparacions.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
