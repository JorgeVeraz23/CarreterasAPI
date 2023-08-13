using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;
using Azure.Core;
namespace APICarreteras.Repository
{
    public class TipoDeViaRepositorio : Repositorio<TipoDeVium>, ITipoDeViaRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public TipoDeViaRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<TipoDeVium> Actualizar(TipoDeVium entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.TipoDeVia.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
