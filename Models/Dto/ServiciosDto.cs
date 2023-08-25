using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class ServiciosDto
    {

        [Required]
        public int IdUbicacion { get; set; }

        
        public string TipoServicios { get; set; } = null!;

        
        public decimal? Latitud { get; set; }

        
        public string? Estado { get; set; }
    }
}
