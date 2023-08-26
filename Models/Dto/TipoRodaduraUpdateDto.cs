using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class TipoRodaduraUpdateDto {

    [Required]
    public int IdTipoRodadura { get; set; }

    [Required]
    public int? IdTramo { get; set; }

    
    public string? TipoRodadura1 { get; set; }


    public decimal? Latitud { get; set; }


    public decimal? Longitud { get; set; }

    
    public string? Estado { get; set; }
}
}
