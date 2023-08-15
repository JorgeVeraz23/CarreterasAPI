using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class AccesorioDto
    {
        [Required]
        public int? IdTramo { get; set; }

        [Required]
        public int IdAccesorios { get; set; }

        
        public string? TipoAccesorios { get; set; }

        public string? Estado { get; set; }

    }
}
