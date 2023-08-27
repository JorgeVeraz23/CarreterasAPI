using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class CarreteraDetalleUpdateDto
    {
        [Required]
        public int IdCarreteraDetalle { get; set; }

        [Required]
        public int IdCarretera { get; set; }

        [Required]
        public int IdTramo { get; set; }


        public int IdTipoRodadura { get; set; }

     
        public string? ClaseDeCarretera { get; set; }
    }
}
