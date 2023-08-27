using APICarreteras.Models;
using APICarreteras.Repository.IRepositorio;

namespace APICarreteras.Repository
{
    public class CarreteraDetalleRepositorio : Repositorio<CarreteraDetalle>, ICarreteraDetalleRepositorio
    {
        public readonly RedesVialesDbContext _db;
        public CarreteraDetalleRepositorio(RedesVialesDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<CarreteraDetalle> Actualizar(CarreteraDetalle entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.CarreteraDetalles.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
