using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("cuneta")]
public partial class Cunetum
{
    [Key]
    [Column("ID_CUNETA")]
    public int IdCuneta { get; set; }

    [Column("ID_TRAMO")]
    public int? IdTramo { get; set; }

    [Column("TIPO_CUNETA")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TipoCuneta { get; set; }

    [Column("POSICION_CUNETA")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PosicionCuneta { get; set; }

    [Column("DESCRIPCION")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

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
    [InverseProperty("Cuneta")]
    public virtual Tramo? IdTramoNavigation { get; set; }
}
