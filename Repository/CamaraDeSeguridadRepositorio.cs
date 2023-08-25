using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class CamaraDeSeguridadRepositorio : Repositorio<CamarasDeSeguridad>, ICamaraDeSeguridadRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public CamaraDeSeguridadRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<CamarasDeSeguridad> Actualizar(CamarasDeSeguridad entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.CamarasDeSeguridads.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
