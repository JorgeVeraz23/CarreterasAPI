using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Models;

[Table("tramos")]
public partial class Tramo
{
    [Key]
    [Column("ID_TRAMO")]
    public int IdTramo { get; set; }

    [Column("ID_CARRETERA")]
    public int? IdCarretera { get; set; }

    [Column("NOMBRE")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [Column("TIPO_TRAMOS")]
    [StringLength(255)]
    [Unicode(false)]
    public string? TipoTramos { get; set; }

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

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<Accesorio> Accesorios { get; set; } = new List<Accesorio>();

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<Alcantarillado> Alcantarillados { get; set; } = new List<Alcantarillado>();

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<CalendarioDeActuacione> CalendarioDeActuaciones { get; set; } = new List<CalendarioDeActuacione>();

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<CamarasDeSeguridad> CamarasDeSeguridads { get; set; } = new List<CamarasDeSeguridad>();

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<CarreteraDetalle> CarreteraDetalles { get; set; } = new List<CarreteraDetalle>();

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<Cunetum> Cuneta { get; set; } = new List<Cunetum>();

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<Curva> Curvas { get; set; } = new List<Curva>();

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<Dano> Danos { get; set; } = new List<Dano>();

    [ForeignKey("IdCarretera")]
    [InverseProperty("Tramos")]
    public virtual Carretera? IdCarreteraNavigation { get; set; }

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<Iluminacion> Iluminacions { get; set; } = new List<Iluminacion>();

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<Interseccione> Intersecciones { get; set; } = new List<Interseccione>();

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<Puente> Puentes { get; set; } = new List<Puente>();

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<Senalizacion> Senalizacions { get; set; } = new List<Senalizacion>();

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<Talud> Taluds { get; set; } = new List<Talud>();

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<TipoRodadura> TipoRodaduras { get; set; } = new List<TipoRodadura>();

    [InverseProperty("IdTramoNavigation")]
    public virtual ICollection<Tunele> Tuneles { get; set; } = new List<Tunele>();
}
