using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class AlcantarilladoUpdateDto
    {
        [Required]
        public int IdAlcantarillado { get; set; }

        [Required]
        public int? IdTramo { get; set; }

        
        public string? TipoAlcantarillado { get; set; }

        
        public double? LongitudDuctoKm { get; set; }

        public decimal? Latitud { get; set; }

        public string? Estado { get; set; }
    }
}
