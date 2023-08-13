using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("iluminacion")]
public partial class Iluminacion
{
    [Key]
    [Column("ID_ILUMINACION")]
    public int IdIluminacion { get; set; }

    [Column("ID_TRAMO")]
    public int? IdTramo { get; set; }

    [Column("TIPO_ILUMINACION")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TipoIluminacion { get; set; }

    [Column("LATITUD", TypeName = "decimal(10, 6)")]
    public decimal? Latitud { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [ForeignKey("IdTramo")]
    [InverseProperty("Iluminacions")]
    public virtual Tramo? IdTramoNavigation { get; set; }
}
