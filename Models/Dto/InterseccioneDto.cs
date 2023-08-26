using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class InterseccioneDto
    {
        [Required]   
        public int IdIntersecciones { get; set; }

        [Required]
        public int? IdTramo { get; set; }


        public decimal? Latitud { get; set; }
    }
}
