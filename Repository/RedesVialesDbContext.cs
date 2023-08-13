using System;
using System.Collections.Generic;
using APICarreteras.Models;
using Microsoft.EntityFrameworkCore;

namespace APICarreteras.Repository;

public partial class RedesVialesDbContext : DbContext
{
    public RedesVialesDbContext()
    {
    }

    public RedesVialesDbContext(DbContextOptions<RedesVialesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accesorio> Accesorios { get; set; }

    public virtual DbSet<Alcantarillado> Alcantarillados { get; set; }

    public virtual DbSet<CalendarioDeActuacione> CalendarioDeActuaciones { get; set; }

    public virtual DbSet<CamarasDeSeguridad> CamarasDeSeguridads { get; set; }

    public virtual DbSet<Canton> Cantons { get; set; }

    public virtual DbSet<Carretera> Carreteras { get; set; }

    public virtual DbSet<CarreteraDetalle> CarreteraDetalles { get; set; }

    public virtual DbSet<CostoReparacion> CostoReparacions { get; set; }

    public virtual DbSet<Cunetum> Cuneta { get; set; }

    public virtual DbSet<Curva> Curvas { get; set; }

    public virtual DbSet<Dano> Danos { get; set; }

    public virtual DbSet<Iluminacion> Iluminacions { get; set; }

    public virtual DbSet<Interseccione> Intersecciones { get; set; }

    public virtual DbSet<Puente> Puentes { get; set; }

    public virtual DbSet<Senalizacion> Senalizacions { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Talud> Taluds { get; set; }

    public virtual DbSet<TipoCarril> TipoCarrils { get; set; }

    public virtual DbSet<TipoDeVium> TipoDeVia { get; set; }

    public virtual DbSet<TipoRodadura> TipoRodaduras { get; set; }

    public virtual DbSet<Tramo> Tramos { get; set; }

    public virtual DbSet<Tunele> Tuneles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-K3LB2V2;Initial Catalog=redes_viales_guayas;Integrated Security=True; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accesorio>(entity =>
        {
            entity.HasKey(e => e.IdAccesorios).HasName("PK__accesori__1D8249C91A0A60B3");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.Accesorios).HasConstraintName("accesorios_ibfk_1");
        });

        modelBuilder.Entity<Alcantarillado>(entity =>
        {
            entity.HasKey(e => e.IdAlcantarillado).HasName("PK__alcantar__6C9E7DABEA8F9332");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.Alcantarillados).HasConstraintName("alcantarillado_ibfk_1");
        });

        modelBuilder.Entity<CalendarioDeActuacione>(entity =>
        {
            entity.HasKey(e => e.IdCalendario).HasName("PK__calendar__289F89E0E334499C");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdCostoReparacionNavigation).WithMany(p => p.CalendarioDeActuaciones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calendario_de_actuaciones_ibfk_2");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.CalendarioDeActuaciones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calendario_de_actuaciones_ibfk_1");
        });

        modelBuilder.Entity<CamarasDeSeguridad>(entity =>
        {
            entity.HasKey(e => e.IdCamara).HasName("PK__camaras___473F358EAE84DB8D");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.CamarasDeSeguridads).HasConstraintName("camaras_de_seguridad_ibfk_1");
        });

        modelBuilder.Entity<Canton>(entity =>
        {
            entity.HasKey(e => e.IdCanton).HasName("PK__canton__69745474440EC212");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Carretera>(entity =>
        {
            entity.HasKey(e => e.IdCarretera).HasName("PK__carreter__84054F2D7B5EAF67");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdCantonNavigation).WithMany(p => p.Carreteras).HasConstraintName("carreteras_ibfk_1");

            entity.HasOne(d => d.IdTipoViaNavigation).WithMany(p => p.Carreteras).HasConstraintName("carreteras_fk_tipo_via");
        });

        modelBuilder.Entity<CarreteraDetalle>(entity =>
        {
            entity.HasKey(e => e.IdCarreteraDetalle).HasName("PK__carreter__EE6ECF20064A387A");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdCarreteraNavigation).WithMany(p => p.CarreteraDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("carretera_detalle_ibfk_1");

            entity.HasOne(d => d.IdTipoRodaduraNavigation).WithMany(p => p.CarreteraDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("carretera_detalle_ibfk_3");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.CarreteraDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("carretera_detalle_ibfk_2");
        });

        modelBuilder.Entity<CostoReparacion>(entity =>
        {
            entity.HasKey(e => e.IdCostoReparacion).HasName("PK__costo_re__AACA50B0FD44C725");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdDanosNavigation).WithMany(p => p.CostoReparacions).HasConstraintName("costo_reparacion_ibfk_1");
        });

        modelBuilder.Entity<Cunetum>(entity =>
        {
            entity.HasKey(e => e.IdCuneta).HasName("PK__cuneta__5ED9F0F2586BFF7A");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.Cuneta).HasConstraintName("cuneta_ibfk_1");
        });

        modelBuilder.Entity<Curva>(entity =>
        {
            entity.HasKey(e => e.IdCurvas).HasName("PK__curvas__7E6F9E0B83FFED40");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.Curvas).HasConstraintName("curvas_ibfk_1");
        });

        modelBuilder.Entity<Dano>(entity =>
        {
            entity.HasKey(e => e.IdDanos).HasName("PK__danos__EC006B2737F8A538");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.Danos).HasConstraintName("danos_ibfk_1");
        });

        modelBuilder.Entity<Iluminacion>(entity =>
        {
            entity.HasKey(e => e.IdIluminacion).HasName("PK__iluminac__87650CC96F42C3AC");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.Iluminacions).HasConstraintName("iluminacion_ibfk_1");
        });

        modelBuilder.Entity<Interseccione>(entity =>
        {
            entity.HasKey(e => e.IdIntersecciones).HasName("PK__intersec__4AAEA8DBAB7FCEC9");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.Intersecciones).HasConstraintName("intersecciones_ibfk_1");
        });

        modelBuilder.Entity<Puente>(entity =>
        {
            entity.HasKey(e => e.IdPuente).HasName("PK__puentes__B600EB72466BC5EB");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.Puentes).HasConstraintName("puentes_ibfk_1");
        });

        modelBuilder.Entity<Senalizacion>(entity =>
        {
            entity.HasKey(e => e.IdSenalizacion).HasName("PK__senaliza__FA44F75F59A65A2E");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.Senalizacions).HasConstraintName("senalizacion_ibfk_1");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdUbicacion).HasName("PK__servicio__3C017AB47F1C757A");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Talud>(entity =>
        {
            entity.HasKey(e => e.IdTalud).HasName("PK__talud__6E1A372E5E90ACCD");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CantonNavigation).WithMany(p => p.Taluds).HasConstraintName("talud_ibfk_1");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.Taluds).HasConstraintName("talud_ibfk_2");
        });

        modelBuilder.Entity<TipoCarril>(entity =>
        {
            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTramoNavigation).WithMany().HasConstraintName("tipo_carril_ibfk_1");
        });

        modelBuilder.Entity<TipoDeVium>(entity =>
        {
            entity.HasKey(e => e.IdTipoVia).HasName("PK__tipo_de___98AD968BDE605721");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TipoRodadura>(entity =>
        {
            entity.HasKey(e => e.IdTipoRodadura).HasName("PK__tipo_rod__BF64A4F079781EF0");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.TipoRodaduras).HasConstraintName("tipo_rodadura_ibfk_1");
        });

        modelBuilder.Entity<Tramo>(entity =>
        {
            entity.HasKey(e => e.IdTramo).HasName("PK__tramos__3FC58E2C0A376B36");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdCarreteraNavigation).WithMany(p => p.Tramos).HasConstraintName("tramos_ibfk_1");
        });

        modelBuilder.Entity<Tunele>(entity =>
        {
            entity.HasKey(e => e.IdTunel).HasName("PK__tuneles__F0D82ED04C6B6C82");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTramoNavigation).WithMany(p => p.Tuneles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tuneles_fk_tramos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
