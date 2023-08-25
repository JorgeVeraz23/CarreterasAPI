using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class CunetaRepositorio : Repositorio<Cunetum>, ICunetaRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public CunetaRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Cunetum> Actualizar(Cunetum entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Cuneta.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
