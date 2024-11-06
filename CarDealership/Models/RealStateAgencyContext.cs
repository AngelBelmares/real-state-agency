using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RealStateAgency.Models;

public partial class RealStateAgencyContext : DbContext
{
    public RealStateAgencyContext()
    {
    }

    public RealStateAgencyContext(DbContextOptions<RealStateAgencyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<House> Houses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.AgentId).HasName("agents_pkey");

            entity.ToTable("agents");

            entity.Property(e => e.AgentId)
                .ValueGeneratedNever()
                .HasColumnName("agent_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
            entity.Property(e => e.Mail)
                .HasColumnType("character varying")
                .HasColumnName("mail");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Telephone)
                .HasColumnType("character varying")
                .HasColumnName("telephone");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("appointments_pkey");

            entity.ToTable("appointments");

            entity.Property(e => e.AppointmentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("appointment_id");
            entity.Property(e => e.AgentId).HasColumnName("agent_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.HouseId).HasColumnName("house_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Agent).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.AgentId)
                .HasConstraintName("appointments_agent_id_fkey");

            entity.HasOne(d => d.House).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("appointments_house_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("appointments_user_id_fkey");
        });

        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.HouseId).HasName("houses_pkey");

            entity.ToTable("houses");

            entity.Property(e => e.HouseId)
                .ValueGeneratedNever()
                .HasColumnName("house_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasColumnType("character varying")
                .HasColumnName("image");
            entity.Property(e => e.Location)
                .HasColumnType("character varying")
                .HasColumnName("location");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Mail, "users_mail_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Mail)
                .HasMaxLength(100)
                .HasColumnName("mail");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
