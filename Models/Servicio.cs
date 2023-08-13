using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("servicios")]
public partial class Servicio
{
    [Key]
    [Column("ID_UBICACION")]
    public int IdUbicacion { get; set; }

    [Column("TIPO_SERVICIOS")]
    [StringLength(50)]
    [Unicode(false)]
    public string TipoServicios { get; set; } = null!;

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
}
