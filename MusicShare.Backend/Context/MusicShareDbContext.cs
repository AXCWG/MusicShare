using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MusicShare.Backend.Models;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MusicShare.Backend.Context;

public partial class MusicShareDbContext : DbContext
{
    public MusicShareDbContext()
    {
    }

    public MusicShareDbContext(DbContextOptions<MusicShareDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(new Func<string>(() =>
        {
            var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("MusicShare.Backend.ConStr");
            var mem = new MemoryStream();
            s!.CopyTo(mem);
            return Encoding.UTF8.GetString(mem.ToArray()); 
        })(), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.4.3-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.ToTable("comments");

            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.Master)
                .HasMaxLength(36)
                .HasColumnName("master");
            entity.Property(e => e.Mod)
                .HasColumnType("text")
                .HasColumnName("mod");
            entity.Property(e => e.Pubtime)
                .HasColumnType("datetime")
                .HasColumnName("pubtime");
            entity.Property(e => e.Vote).HasColumnName("vote");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.ToTable("posts");

            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.Mod)
                .HasColumnType("text")
                .HasColumnName("mod");
            entity.Property(e => e.Pubtime)
                .HasColumnType("datetime")
                .HasColumnName("pubtime");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Vote)
                .HasMaxLength(255)
                .HasColumnName("vote");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
