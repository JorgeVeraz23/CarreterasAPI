using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("canton")]
public partial class Canton
{
    [Key]
    [Column("ID_CANTON")]
    public int IdCanton { get; set; }

    [Column("NOMBRE")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [InverseProperty("IdCantonNavigation")]
    public virtual ICollection<Carretera> Carreteras { get; set; } = new List<Carretera>();

    [InverseProperty("CantonNavigation")]
    public virtual ICollection<Talud> Taluds { get; set; } = new List<Talud>();
}
