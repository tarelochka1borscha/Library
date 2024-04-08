using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBaseLibrary.Models;

public partial class _43pKnowledgeBaseContext : DbContext
{
    public _43pKnowledgeBaseContext()
    {
    }

    public _43pKnowledgeBaseContext(DbContextOptions<_43pKnowledgeBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Problem> Problems { get; set; }

    public virtual DbSet<Reason> Reasons { get; set; }

    public virtual DbSet<Soft> Softs { get; set; }

    public virtual DbSet<Solution> Solutions { get; set; }

    public virtual DbSet<SolutionStep> SolutionSteps { get; set; }

    public virtual DbSet<Step> Steps { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagProblem> TagProblems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ngknn.ru;Port=5442;Database=43P_KnowledgeBase;Username=43P;Password=444444");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Answers_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.Answer1)
                .HasColumnType("character varying")
                .HasColumnName("Answer");
        });

        modelBuilder.Entity<Problem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Problems_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.Title).HasColumnType("character varying");
        });

        modelBuilder.Entity<Reason>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Reasons_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

            entity.HasOne(d => d.Problem).WithMany(p => p.Reasons)
                .HasForeignKey(d => d.ProblemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Reasons_ProblemID_fkey");
        });

        modelBuilder.Entity<Soft>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Softs_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.Title).HasColumnType("character varying");
        });

        modelBuilder.Entity<Solution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Solutions_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

            entity.HasOne(d => d.Answer).WithMany(p => p.Solutions)
                .HasForeignKey(d => d.AnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Solutions_AnswerID_fkey");

            entity.HasOne(d => d.Problem).WithMany(p => p.Solutions)
                .HasForeignKey(d => d.ProblemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Solutions_ProblemID_fkey");
        });

        modelBuilder.Entity<SolutionStep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Solution_Steps_pkey");

            entity.ToTable("Solution_Steps");

            entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

            entity.HasOne(d => d.Solution).WithMany(p => p.SolutionSteps)
                .HasForeignKey(d => d.SolutionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Solution_Steps_SolutionID_fkey");

            entity.HasOne(d => d.Step).WithMany(p => p.SolutionSteps)
                .HasForeignKey(d => d.StepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Solution_Steps_StepID_fkey");
        });

        modelBuilder.Entity<Step>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Steps_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.Action).HasColumnType("character varying");

            entity.HasOne(d => d.Soft).WithMany(p => p.Steps)
                .HasForeignKey(d => d.SoftId)
                .HasConstraintName("Steps_SoftId_fkey");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tags_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.Title).HasColumnType("character varying");
        });

        modelBuilder.Entity<TagProblem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tag_Problems_pkey");

            entity.ToTable("Tag_Problems");

            entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

            entity.HasOne(d => d.Problem).WithMany(p => p.TagProblems)
                .HasForeignKey(d => d.ProblemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tag_Problems_ProblemId_fkey");

            entity.HasOne(d => d.Tag).WithMany(p => p.TagProblems)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tag_Problems_TagId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
