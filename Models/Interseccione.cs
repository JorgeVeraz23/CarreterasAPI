using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("intersecciones")]
public partial class Interseccione
{
    [Key]
    [Column("ID_INTERSECCIONES")]
    public int IdIntersecciones { get; set; }

    [Column("ID_TRAMO")]
    public int? IdTramo { get; set; }

    [Column("LATITUD", TypeName = "decimal(10, 6)")]
    public decimal? Latitud { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [ForeignKey("IdTramo")]
    [InverseProperty("Intersecciones")]
    public virtual Tramo? IdTramoNavigation { get; set; }
}
