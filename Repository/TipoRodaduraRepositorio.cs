using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class TipoRodaduraRepositorio : Repositorio<TipoRodadura>, ITipoRodaduraRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public TipoRodaduraRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<TipoRodadura> Actualizar(TipoRodadura entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.TipoRodaduras.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }


    }

}
