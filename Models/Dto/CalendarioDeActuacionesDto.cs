using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class CalendarioDeActuacionesDto
    {
        [Required]
        public int IdCalendario { get; set; }

        [Required]
        public int IdTramo { get; set; }

        
        public short? Año { get; set; }

        
        public string? Descripcion { get; set; }

        
        public string? Codigo { get; set; }

        [Required]
        public int IdCostoReparacion { get; set; }

        
        public double? CantidadDeTrabajo { get; set; }
    }
}
