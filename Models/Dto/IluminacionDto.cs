using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class IluminacionDto
    {
        [Required]
        public int IdIluminacion { get; set; }

        [Required]
        public int? IdTramo { get; set; }


        public string? TipoIluminacion { get; set; }

        public decimal? Latitud { get; set; }
    }
}
