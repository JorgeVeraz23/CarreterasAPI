using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("tipo_rodadura")]
public partial class TipoRodadura
{
    [Key]
    [Column("ID_TIPO_RODADURA")]
    public int IdTipoRodadura { get; set; }

    [Column("ID_TRAMO")]
    public int? IdTramo { get; set; }

    [Column("TIPO_RODADURA")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TipoRodadura1 { get; set; }

    [Column("LATITUD", TypeName = "decimal(10, 6)")]
    public decimal? Latitud { get; set; }

    [Column("LONGITUD", TypeName = "decimal(10, 6)")]
    public decimal? Longitud { get; set; }

    [Column("ESTADO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Estado { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [InverseProperty("IdTipoRodaduraNavigation")]
    public virtual ICollection<CarreteraDetalle> CarreteraDetalles { get; set; } = new List<CarreteraDetalle>();

    [ForeignKey("IdTramo")]
    [InverseProperty("TipoRodaduras")]
    public virtual Tramo? IdTramoNavigation { get; set; }
}
