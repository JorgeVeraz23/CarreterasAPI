using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class CurvaDto
    {
        [Required]
        public int IdCurvas { get; set; }

        [Required]
        public int? IdTramo { get; set; }

        public string? TipoCurvas { get; set; }

        
        public string? EstadoCurvas { get; set; }

     
        public decimal? Latitud { get; set; }

    }
}
