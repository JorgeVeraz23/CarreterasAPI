using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class PuenteDto
    {
        [Required]
        public int IdPuente { get; set; }

        [Required]
        public int? IdTramo { get; set; }

        
        public string? Nombre { get; set; }

        
        public string? Material { get; set; }

       
        public decimal? Latitud { get; set; }

        public decimal? Longitud { get; set; }
    }
}
