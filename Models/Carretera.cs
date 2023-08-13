using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("carreteras")]
public partial class Carretera
{
    [Key]
    [Column("ID_CARRETERA")]
    public int IdCarretera { get; set; }

    [Column("ID_CANTON")]
    public int? IdCanton { get; set; }

    [Column("NOMBRE")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [Column("LATITUD", TypeName = "decimal(10, 6)")]
    public decimal? Latitud { get; set; }

    [Column("LONGITUD", TypeName = "decimal(10, 6)")]
    public decimal? Longitud { get; set; }

    [Column("ESTADO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Estado { get; set; }

    [Column("ID_TIPO_VIA")]
    public int? IdTipoVia { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [InverseProperty("IdCarreteraNavigation")]
    public virtual ICollection<CarreteraDetalle> CarreteraDetalles { get; set; } = new List<CarreteraDetalle>();

    [ForeignKey("IdCanton")]
    [InverseProperty("Carreteras")]
    public virtual Canton? IdCantonNavigation { get; set; }

    [ForeignKey("IdTipoVia")]
    [InverseProperty("Carreteras")]
    public virtual TipoDeVium? IdTipoViaNavigation { get; set; }

    [InverseProperty("IdCarreteraNavigation")]
    public virtual ICollection<Tramo> Tramos { get; set; } = new List<Tramo>();
}
