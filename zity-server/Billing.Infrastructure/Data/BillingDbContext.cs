using System;
using System.Collections.Generic;
using Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;


namespace Billing.Infrastructure.Data;

public partial class BillingDbContext : DbContext
{
    public BillingDbContext(DbContextOptions<BillingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillDetail> BillDetails { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)
        //{
        //    optionsBuilder.LogTo(Console.WriteLine, LogLevel.None);
        //}
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");



        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("bills");

            entity.HasIndex(e => e.RelationshipId, "relationship_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Monthly)
                .HasMaxLength(7)
                .HasColumnName("monthly");
            entity.Property(e => e.NewWater).HasColumnName("new_water");
            entity.Property(e => e.OldWater).HasColumnName("old_water");
            entity.Property(e => e.RelationshipId).HasColumnName("relationship_id");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'UNPAID'")
                .HasColumnType("enum('UNPAID','PAID','OVERDUE')")
                .HasColumnName("status");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WaterReadingDate)
                .HasColumnType("datetime")
                .HasColumnName("water_reading_date");


        });

        modelBuilder.Entity<BillDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("bill_details");

            entity.HasIndex(e => e.BillId, "bill_id");

            entity.HasIndex(e => e.ServiceId, "service_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BillId).HasColumnName("bill_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Bill).WithMany(p => p.BillDetails)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bill_details_ibfk_1");

            entity.HasOne(d => d.Service).WithMany(p => p.BillDetails)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bill_details_ibfk_2");
        });



        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("services");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("settings");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CurrentMonthly)
                .HasMaxLength(7)
                .HasColumnName("current_monthly");
            entity.Property(e => e.EnvProtectionTax).HasColumnName("env_protection_tax");
            entity.Property(e => e.RoomPricePerM2).HasColumnName("room_price_per_m2");
            entity.Property(e => e.RoomVat).HasColumnName("room_vat");
            entity.Property(e => e.SystemStatus)
                .HasColumnType("enum('PREPAYMENT','PAYMENT','OVERDUE','DELINQUENT')")
                .HasColumnName("system_status");
            entity.Property(e => e.WaterPricePerM3).HasColumnName("water_price_per_m3");
            entity.Property(e => e.WaterVat).HasColumnName("water_vat");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
