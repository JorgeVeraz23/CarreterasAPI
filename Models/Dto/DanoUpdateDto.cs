using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class DanoUpdateDto
    {
        [Required]
        public int IdDanos { get; set; }

        [Required]
        public int? IdTramo { get; set; }

        
        public string? TipoDanos { get; set; }

        
        public string? Descripcion { get; set; }

        
        public decimal? Latitud { get; set; }

        
        public decimal? Longitud { get; set; }
    }
}
