using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("costo_reparacion")]
public partial class CostoReparacion
{
    [Key]
    [Column("ID_COSTO_REPARACION")]
    public int IdCostoReparacion { get; set; }

    [Column("ID_DANOS")]
    public int? IdDanos { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? ReparacionNombre { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Unidad { get; set; }

    public double? CostoFinan { get; set; }

    [Column("RPC")]
    public double? Rpc { get; set; }

    public double? CostoEcon { get; set; }

    [InverseProperty("IdCostoReparacionNavigation")]
    public virtual ICollection<CalendarioDeActuacione> CalendarioDeActuaciones { get; set; } = new List<CalendarioDeActuacione>();

    [ForeignKey("IdDanos")]
    [InverseProperty("CostoReparacions")]
    public virtual Dano? IdDanosNavigation { get; set; }
}
