using Microsoft.EntityFrameworkCore;
using Parking.Model.Models;

namespace Parking.Data.Context;

public partial class ApplicationDbContext : DbContext
{
    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerVehicle> CustomerVehicles { get; set; }

    public virtual DbSet<Stay> Stays { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    private string _connectionString = "Server=.\\SQLEXPRESS;Database=Parking;User Id=sa;Password=********;TrustServerCertificate=True";

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Addresse__3214EC07CB7D83E4");

            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Complement)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FederativeUnit)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Neighborhood)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Number)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ZipCode)
                .IsRequired()
                .HasMaxLength(9)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC0794F4CDEF");

            entity.HasIndex(e => e.Cpf, "UQ__Customer__C1FF9309D04BA2C4").IsUnique();

            entity.Property(e => e.Cpf)
                .IsRequired()
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Address).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__Customers__Addre__3A81B327");
        });

        modelBuilder.Entity<CustomerVehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC0773697324");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerVehicles)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__CustomerV__Custo__412EB0B6");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.CustomerVehicles)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__CustomerV__Vehic__4222D4EF");
        });

        modelBuilder.Entity<Stay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stays__3214EC078CDF02D7");

            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.ExitDate).HasColumnType("datetime");
            entity.Property(e => e.HourlyRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LicensePlate)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StayStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CustomerVehicle).WithMany(p => p.Stays)
                .HasForeignKey(d => d.CustomerVehicleId)
                .HasConstraintName("FK__Stays__CustomerV__45F365D3");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehicles__3214EC07D56A8AC5");

            entity.Property(e => e.Brand)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Color)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.VehicleType)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
