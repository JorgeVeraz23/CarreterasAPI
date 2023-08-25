using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class CunetaCreateDto
    {
        
        public int IdCuneta { get; set; }

        
        public int? IdTramo { get; set; }

        
        public string? TipoCuneta { get; set; }

        public string? PosicionCuneta { get; set; }


        public string? Descripcion { get; set; }


        public decimal? Latitud { get; set; }

        public decimal? Longitud { get; set; }

        public string? Estado { get; set; }
    }
}
