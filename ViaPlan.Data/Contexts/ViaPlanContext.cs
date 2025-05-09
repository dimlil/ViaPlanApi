using Microsoft.EntityFrameworkCore;
using ViaPlan.Entities;

namespace ViaPlan.Data;

public partial class ViaPlanContext : DbContext
{
    public ViaPlanContext()
    {
    }

    public ViaPlanContext(DbContextOptions<ViaPlanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WeatherCache> WeatherCaches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ViaPlan;User Id=sa;Password=yourStrongPassword1;TrustServerCertificate=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_CI_AI");

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hotels__3214EC071FBA0A71");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CachedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.PricePerNight).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trips__3214EC07C21A15A0");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Destination).HasMaxLength(100);
            entity.Property(e => e.HotelRecommendation).HasMaxLength(255);
            entity.Property(e => e.WeatherSummary).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Trips)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Trips__UserId__3D5E1FD2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC070DB6CABB");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4ED5313F9").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105346EF5466A").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("User");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<WeatherCache>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WeatherC__3214EC07173B8C5E");

            entity.ToTable("WeatherCache");

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.RetrievedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Summary).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
