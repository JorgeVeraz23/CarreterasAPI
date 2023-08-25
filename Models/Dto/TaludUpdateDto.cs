using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class TaludUpdateDto
    {

        [Required]
        public int IdTalud { get; set; }

        [Required]
        public int? Canton { get; set; }

       
        public int? Cantidad { get; set; }

        [Required]
        public int? IdTramo { get; set; }

        
        public decimal? Latitud { get; set; }

        
        public decimal? Longitud { get; set; }
    }
}
