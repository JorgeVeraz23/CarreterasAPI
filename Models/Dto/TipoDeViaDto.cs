using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class TipoDeViaDto
    {
        [Required]
        public int IdTipoVia { get; set; }
        public string? TipoDeVia { get; set; }

    }
}
