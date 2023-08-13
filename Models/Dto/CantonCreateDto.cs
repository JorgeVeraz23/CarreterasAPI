using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class CantonCreateDto
    {
        [Required]
        public int IdCanton { get; set; }

        public string? Nombre { get; set; }

    }
}
