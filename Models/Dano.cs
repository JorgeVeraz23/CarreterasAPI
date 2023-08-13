using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("danos")]
public partial class Dano
{
    [Key]
    [Column("ID_DANOS")]
    public int IdDanos { get; set; }

    [Column("ID_TRAMO")]
    public int? IdTramo { get; set; }

    [Column("TIPO_DANOS")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TipoDanos { get; set; }

    [Column("DESCRIPCION")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [Column("LATITUD", TypeName = "decimal(10, 6)")]
    public decimal? Latitud { get; set; }

    [Column("LONGITUD", TypeName = "decimal(10, 6)")]
    public decimal? Longitud { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [InverseProperty("IdDanosNavigation")]
    public virtual ICollection<CostoReparacion> CostoReparacions { get; set; } = new List<CostoReparacion>();

    [ForeignKey("IdTramo")]
    [InverseProperty("Danos")]
    public virtual Tramo? IdTramoNavigation { get; set; }
}
