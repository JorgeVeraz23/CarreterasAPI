using APICarreteras.Models;
using APICarreteras.Repository;
using APICarreteras.Repository.IRepositorio;
using Azure.Core;

namespace APICarreteras.Repository
{
    public class CantonRepositorio : Repositorio<Canton>, ICantonRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public CantonRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Canton> Actualizar(Canton entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Cantons.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
