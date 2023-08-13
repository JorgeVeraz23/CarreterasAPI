using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("curvas")]
public partial class Curva
{
    [Key]
    [Column("ID_CURVAS")]
    public int IdCurvas { get; set; }

    [Column("ID_TRAMO")]
    public int? IdTramo { get; set; }

    [Column("TIPO_CURVAS")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TipoCurvas { get; set; }

    [Column("ESTADO_CURVAS")]
    [StringLength(20)]
    [Unicode(false)]
    public string? EstadoCurvas { get; set; }

    [Column("LATITUD", TypeName = "decimal(10, 6)")]
    public decimal? Latitud { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [ForeignKey("IdTramo")]
    [InverseProperty("Curvas")]
    public virtual Tramo? IdTramoNavigation { get; set; }
}
