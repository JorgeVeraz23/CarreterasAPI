using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("camaras_de_seguridad")]
public partial class CamarasDeSeguridad
{
    [Key]
    [Column("ID_CAMARA")]
    public int IdCamara { get; set; }

    [Column("ID_TRAMO")]
    public int? IdTramo { get; set; }

    [Column("TIPO_CAMARA")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TipoCamara { get; set; }

    [Column("LATITUD", TypeName = "decimal(10, 6)")]
    public decimal? Latitud { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [ForeignKey("IdTramo")]
    [InverseProperty("CamarasDeSeguridads")]
    public virtual Tramo? IdTramoNavigation { get; set; }
}
