using Microsoft.EntityFrameworkCore;
using Src.api_net8.Application.Context;
using Src.api_net8.Domain.Models;
using System.Data;

namespace Src.api_net8.Infrastructure.context
{
    public class DataContext : DbContext, IDataContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Factor>(entity =>
            {
                entity.ToTable("Factor");

                entity.Property(e => e.Customer).HasMaxLength(50);
                entity.Property(e => e.DelivaryType).HasConversion<byte>();
            });
            modelBuilder.Entity<FactorDetail>(entity =>
            {
                entity.ToTable("FactorDetail");

                entity.Property(e => e.Count).HasColumnType("decimal(18, 3)");
                entity.Property(e => e.ProductDescription).HasMaxLength(50);

                entity.HasOne(d => d.Factor).WithMany(p => p.FactorDetails)
                    .HasForeignKey(d => d.FactorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactorDetail_Factor");

                entity.HasOne(d => d.Product).WithMany(p => p.FactorDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactorDetail_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");
                entity.Property(e => e.ProductCode)
                    .HasMaxLength(13)
                    .IsUnicode(false);
                entity.Property(e => e.ProductName).HasMaxLength(50);
                entity.Property(e => e.Unit).HasMaxLength(20);
            });

        }

        public virtual DbSet<Factor> Factors { get; set; }

        public virtual DbSet<FactorDetail> FactorDetails { get; set; }

        public virtual DbSet<Product> Products { get; set; }
    }
}

