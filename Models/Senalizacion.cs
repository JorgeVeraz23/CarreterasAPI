using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("senalizacion")]
public partial class Senalizacion
{
    [Key]
    [Column("ID_SENALIZACION")]
    public int IdSenalizacion { get; set; }

    [Column("ID_TRAMO")]
    public int? IdTramo { get; set; }

    [Column("LATITUD", TypeName = "decimal(10, 6)")]
    public decimal? Latitud { get; set; }

    [Column("TIPO_SENALIZACION")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TipoSenalizacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [ForeignKey("IdTramo")]
    [InverseProperty("Senalizacions")]
    public virtual Tramo? IdTramoNavigation { get; set; }
}
