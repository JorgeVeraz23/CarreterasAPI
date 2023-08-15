using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class AlcantarilladoRepositorio : Repositorio<Alcantarillado>, IAlcantarilladoRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public AlcantarilladoRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Alcantarillado> Actualizar(Alcantarillado entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Alcantarillados.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
