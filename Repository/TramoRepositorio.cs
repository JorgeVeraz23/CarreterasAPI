using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Repository
{
    public class TramoRepositorio : Repositorio<Tramo>, ITramoRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public TramoRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Tramo> Actualizar(Tramo entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Tramos.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }

}
