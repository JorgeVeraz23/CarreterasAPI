using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("carretera_detalle")]
public partial class CarreteraDetalle
{
    [Key]
    [Column("id_carretera_detalle")]
    public int IdCarreteraDetalle { get; set; }

    [Column("id_carretera")]
    public int IdCarretera { get; set; }

    [Column("id_tramo")]
    public int IdTramo { get; set; }

    [Column("id_tipo_rodadura")]
    public int IdTipoRodadura { get; set; }

    [Column("clase_de_carretera")]
    [StringLength(255)]
    [Unicode(false)]
    public string? ClaseDeCarretera { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    public double? Ancho { get; set; }

    public double? AnchoEnMetros { get; set; }

    public double? Curvatura { get; set; }

    public double? RampaMasPendiente { get; set; }

    [ForeignKey("IdCarretera")]
    [InverseProperty("CarreteraDetalles")]
    public virtual Carretera IdCarreteraNavigation { get; set; } = null!;

    [ForeignKey("IdTipoRodadura")]
    [InverseProperty("CarreteraDetalles")]
    public virtual TipoRodadura IdTipoRodaduraNavigation { get; set; } = null!;

    [ForeignKey("IdTramo")]
    [InverseProperty("CarreteraDetalles")]
    public virtual Tramo IdTramoNavigation { get; set; } = null!;
}
