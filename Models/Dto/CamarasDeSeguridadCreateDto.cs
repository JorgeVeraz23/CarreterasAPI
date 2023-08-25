using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class CamarasDeSeguridadCreateDto
    {
        [Required]
        public int IdCamara { get; set; }

        [Required]
        public int? IdTramo { get; set; }

        public string? TipoCamara { get; set; }

        public decimal? Latitud { get; set; }
    }
}
