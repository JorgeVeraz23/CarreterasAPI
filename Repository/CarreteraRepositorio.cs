using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class CarreteraRepositorio : Repositorio<Carretera>, ICarreteraRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public CarreteraRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Carretera> Actualizar(Carretera entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Carreteras.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
