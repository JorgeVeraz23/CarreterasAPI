using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class IluminacionRepositorio : Repositorio<Iluminacion>, IiluminacionRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public IluminacionRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Iluminacion> Actualizar(Iluminacion entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Iluminacions.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
