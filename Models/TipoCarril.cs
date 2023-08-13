using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Keyless]
[Table("tipo_carril")]
public partial class TipoCarril
{
    [Column("ID_TRAMO")]
    public int? IdTramo { get; set; }

    [Column("TIPO_CARRIL")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TipoCarril1 { get; set; }

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

    [ForeignKey("IdTramo")]
    public virtual Tramo? IdTramoNavigation { get; set; }
}
