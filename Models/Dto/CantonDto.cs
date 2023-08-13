using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICarreteras.Models.Dto
{
    public class CantonDto
    {
        [Required]
        public int IdCanton { get; set; }

        public string? Nombre { get; set; }

       
    }

}
