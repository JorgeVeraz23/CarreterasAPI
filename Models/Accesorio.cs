using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("accesorios")]
public partial class Accesorio
{
    [Column("ID_TRAMO")]
    public int? IdTramo { get; set; }

    [Key]
    [Column("ID_ACCESORIOS")]
    public int IdAccesorios { get; set; }

    [Column("TIPO_ACCESORIOS")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TipoAccesorios { get; set; }

    [Column("ESTADO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Estado { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [ForeignKey("IdTramo")]
    [InverseProperty("Accesorios")]
    public virtual Tramo? IdTramoNavigation { get; set; }
}
