using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("puentes")]
public partial class Puente
{
    [Key]
    [Column("ID_PUENTE")]
    public int IdPuente { get; set; }

    [Column("ID_TRAMO")]
    public int? IdTramo { get; set; }

    [Column("NOMBRE")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [Column("MATERIAL")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Material { get; set; }

    [Column("LATITUD", TypeName = "decimal(10, 6)")]
    public decimal? Latitud { get; set; }

    [Column("LONGITUD", TypeName = "decimal(10, 6)")]
    public decimal? Longitud { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [ForeignKey("IdTramo")]
    [InverseProperty("Puentes")]
    public virtual Tramo? IdTramoNavigation { get; set; }
}
