using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class CarreteraUpdateDto
    {

        [Required]
        public int IdCarretera { get; set; }
        [Required]
        public int? IdCanton { get; set; }

        public string? Nombre { get; set; }
        public decimal? Latitud { get; set; }

        public decimal? Longitud { get; set; }

        public string? Estado { get; set; }

        [Required]
        public int? IdTipoVia { get; set; }

    }
}
