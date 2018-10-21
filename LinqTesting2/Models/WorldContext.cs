using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LinqTesting2.Models
{
    public partial class WorldContext : DbContext
    {
        public WorldContext()
        {
        }

        public WorldContext(DbContextOptions<WorldContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Countrylanguage> Countrylanguage { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=world;user=root;pwd=senhagithub1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(e => e.CountryCode)
                    .HasName("CountryCode");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasColumnType("char(3)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasColumnType("char(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("char(35)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Population)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("City_ibfk_1");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasColumnType("char(3)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Capital).HasColumnType("int(11)");

                entity.Property(e => e.Code2)
                    .IsRequired()
                    .HasColumnType("char(2)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Gnp)
                    .HasColumnName("GNP")
                    .HasColumnType("float(10,2)");

                entity.Property(e => e.Gnpold)
                    .HasColumnName("GNPOld")
                    .HasColumnType("float(10,2)");

                entity.Property(e => e.GovernmentForm)
                    .IsRequired()
                    .HasColumnType("char(45)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.HeadOfState).HasColumnType("char(60)");

                entity.Property(e => e.IndepYear).HasColumnType("smallint(6)");

                entity.Property(e => e.LifeExpectancy).HasColumnType("float(3,1)");

                entity.Property(e => e.LocalName)
                    .IsRequired()
                    .HasColumnType("char(45)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("char(52)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Population)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasColumnType("char(26)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.SurfaceArea)
                    .HasColumnType("float(10,2)")
                    .HasDefaultValueSql("'0.00'");
            });

            modelBuilder.Entity<Countrylanguage>(entity =>
            {
                entity.HasKey(e => new { e.CountryCode, e.Language });

                entity.ToTable("countrylanguage");

                entity.HasIndex(e => e.CountryCode)
                    .HasName("CountryCode");

                entity.Property(e => e.CountryCode)
                    .HasColumnType("char(3)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Language)
                    .HasColumnType("char(30)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Percentage)
                    .HasColumnType("float(4,1)")
                    .HasDefaultValueSql("'0.0'");

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Countrylanguage)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countryLanguage_ibfk_1");
            });
        }
    }
}
