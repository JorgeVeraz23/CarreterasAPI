using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("talud")]
public partial class Talud
{
    [Key]
    [Column("ID_TALUD")]
    public int IdTalud { get; set; }

    [Column("CANTON")]
    public int? Canton { get; set; }

    [Column("CANTIDAD")]
    public int? Cantidad { get; set; }

    [Column("ID_TRAMO")]
    public int? IdTramo { get; set; }

    [Column("LATITUD", TypeName = "decimal(10, 6)")]
    public decimal? Latitud { get; set; }

    [Column("LONGITUD", TypeName = "decimal(10, 6)")]
    public decimal? Longitud { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [ForeignKey("Canton")]
    [InverseProperty("Taluds")]
    public virtual Canton? CantonNavigation { get; set; }

    [ForeignKey("IdTramo")]
    [InverseProperty("Taluds")]
    public virtual Tramo? IdTramoNavigation { get; set; }
}
