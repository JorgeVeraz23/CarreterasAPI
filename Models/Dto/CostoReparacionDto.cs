using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class CostoReparacionDto
    {
        [Required]
        public int IdCostoReparacion { get; set; }

        [Required]
        public int? IdDanos { get; set; }

        public string? ReparacionNombre { get; set; }

     
        public string? Unidad { get; set; }

        public double? CostoFinan { get; set; }

 
        public double? Rpc { get; set; }

        public double? CostoEcon { get; set; }
    }
}
