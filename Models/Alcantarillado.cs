using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("alcantarillado")]
public partial class Alcantarillado
{
    [Key]
    [Column("ID_ALCANTARILLADO")]
    public int IdAlcantarillado { get; set; }

    [Column("ID_TRAMO")]
    public int? IdTramo { get; set; }

    [Column("TIPO_ALCANTARILLADO")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TipoAlcantarillado { get; set; }

    [Column("LONGITUD_DUCTO_KM")]
    public double? LongitudDuctoKm { get; set; }

    [Column("LATITUD", TypeName = "decimal(10, 6)")]
    public decimal? Latitud { get; set; }

    [Column("ESTADO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Estado { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [ForeignKey("IdTramo")]
    [InverseProperty("Alcantarillados")]
    public virtual Tramo? IdTramoNavigation { get; set; }
}
