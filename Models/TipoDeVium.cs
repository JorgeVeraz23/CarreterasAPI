using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("tipo_de_via")]
public partial class TipoDeVium
{
    [Key]
    [Column("ID_TIPO_VIA")]
    public int IdTipoVia { get; set; }

    [Column("TIPO_DE_VIA")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TipoDeVia { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    [InverseProperty("IdTipoViaNavigation")]
    public virtual ICollection<Carretera> Carreteras { get; set; } = new List<Carretera>();
}
