using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("calendario_de_actuaciones")]
public partial class CalendarioDeActuacione
{
    [Key]
    [Column("id_calendario")]
    public int IdCalendario { get; set; }

    [Column("id_tramo")]
    public int IdTramo { get; set; }

    [Column("año")]
    public short? Año { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [Column("codigo")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Codigo { get; set; }

    [Column("id_costo_reparacion")]
    public int IdCostoReparacion { get; set; }

    [Column("cantidad_de_trabajo")]
    public double? CantidadDeTrabajo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [ForeignKey("IdCostoReparacion")]
    [InverseProperty("CalendarioDeActuaciones")]
    public virtual CostoReparacion IdCostoReparacionNavigation { get; set; } = null!;

    [ForeignKey("IdTramo")]
    [InverseProperty("CalendarioDeActuaciones")]
    public virtual Tramo IdTramoNavigation { get; set; } = null!;
}
