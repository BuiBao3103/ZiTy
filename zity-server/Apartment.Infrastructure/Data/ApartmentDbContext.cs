using System;
using System.Collections.Generic;
using Apartment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;


namespace Apartment.Infrastructure.Data;

public partial class ApartmentDbContext : DbContext
{
    public ApartmentDbContext(DbContextOptions<ApartmentDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Domain.Entities.Apartment> Apartments { get; set; }



    public virtual DbSet<Item> Items { get; set; }



    public virtual DbSet<Relationship> Relationships { get; set; }



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



        modelBuilder.Entity<Domain.Entities.Apartment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("apartments");

            entity.Property(e => e.Id)
                .HasMaxLength(5)
                .HasColumnName("id");
            entity.Property(e => e.ApartmentNumber).HasColumnName("apartment_number");
            entity.Property(e => e.Area).HasColumnName("area");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.FloorNumber).HasColumnName("floor_number");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'EMPTY'")
                .HasColumnType("enum('IN_USE','EMPTY','DISRUPTION')")
                .HasColumnName("status");
            entity.Property(e => e.CurrentWaterNumber)
                .HasColumnName("current_water_number");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });



        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("items");

            entity.HasIndex(e => e.UserId, "user_id");

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
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.IsReceive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_receive");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");


        });





        modelBuilder.Entity<Relationship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("relationships");

            entity.HasIndex(e => e.ApartmentId, "apartment_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApartmentId)
                .HasMaxLength(5)
                .HasColumnName("apartment_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Role)
                .HasColumnType("enum('OWNER','USER')")
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Apartment).WithMany(p => p.Relationships)
                .HasForeignKey(d => d.ApartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("relationships_ibfk_2");

        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
