using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("tuneles")]
public partial class Tunele
{
    [Key]
    [Column("ID_TUNEL")]
    public int IdTunel { get; set; }

    [Column("NOMBRE")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [Column("ID_TRAMO")]
    public int IdTramo { get; set; }

    [Column("LATITUD", TypeName = "decimal(10, 6)")]
    public decimal? Latitud { get; set; }

    [Column("LONGITUD", TypeName = "decimal(10, 6)")]
    public decimal? Longitud { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [ForeignKey("IdTramo")]
    [InverseProperty("Tuneles")]
    public virtual Tramo IdTramoNavigation { get; set; } = null!;
}
