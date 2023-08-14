using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class TramoUpdateDto
    {
        [Required]
        public int IdTramo { get; set; }

        [Required]
        public int? IdCarretera { get; set; }

        
        public string? Nombre { get; set; }

        
        public string? TipoTramos { get; set; }

      
        public decimal? Latitud { get; set; }

        
        public decimal? Longitud { get; set; }

       
        public string? Estado { get; set; }
    }
}
