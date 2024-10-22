﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GradeManagement.Data.Model;

public partial class GmsContext : DbContext
{
    public GmsContext()
    {
    }

    public GmsContext(DbContextOptions<GmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminViewForStudent> AdminViewForStudents { get; set; }

    public virtual DbSet<AdminViewForTeacher> AdminViewForTeachers { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<GradeComposition> GradeCompositions { get; set; }

    public virtual DbSet<Sc> Scs { get; set; }

    public virtual DbSet<Scaudit> Scaudits { get; set; }

    public virtual DbSet<Stc> Stcs { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentViewForTeacher> StudentViewForTeachers { get; set; }

    public virtual DbSet<Tcgc> Tcgcs { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeacherViewForStudent> TeacherViewForStudents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=gms;User ID=SA;Password=<YourStrong@Passw0rd>;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admin__3214EC27CBF5BD14");

            entity.Property(e => e.Id).IsFixedLength();
        });

        modelBuilder.Entity<AdminViewForStudent>(entity =>
        {
            entity.ToView("AdminViewForStudent");
        });

        modelBuilder.Entity<AdminViewForTeacher>(entity =>
        {
            entity.ToView("AdminViewForTeacher");

            entity.Property(e => e.Id).IsFixedLength();
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Course__3214EC2734ABCF1C");

            entity.Property(e => e.Id).IsFixedLength();
            entity.Property(e => e.Term).HasDefaultValueSql("('2018-2019-1')");
        });

        modelBuilder.Entity<GradeComposition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GradeCom__3214EC27D3274C21");
        });

        modelBuilder.Entity<Sc>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.CourseId }).HasName("PK__SC__5E57FD6101C1275D");

            entity.Property(e => e.StudentId).IsFixedLength();
            entity.Property(e => e.CourseId).IsFixedLength();

            entity.HasOne(d => d.Course).WithMany(p => p.Scs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SC__CourseID__27F8EE98");

            entity.HasOne(d => d.Student).WithMany(p => p.Scs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SC__StudentID__2704CA5F");
        });

        modelBuilder.Entity<Scaudit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SCAudit__3214EC27F8B0D7AC");

            entity.Property(e => e.CourseId).IsFixedLength();
            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.StudentId).IsFixedLength();

            entity.HasOne(d => d.Course).WithMany(p => p.Scaudits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SCAudit__CourseI__1CBC4616");

            entity.HasOne(d => d.Student).WithMany(p => p.Scaudits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SCAudit__Student__1BC821DD");
        });

        modelBuilder.Entity<Stc>(entity =>
        {
            entity.HasKey(e => new { e.TeacherId, e.StudentId, e.CourseId }).HasName("PK__STC__E81726923AEDB599");

            entity.Property(e => e.TeacherId).IsFixedLength();
            entity.Property(e => e.StudentId).IsFixedLength();
            entity.Property(e => e.CourseId).IsFixedLength();

            entity.HasOne(d => d.Course).WithMany(p => p.Stcs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__STC__CourseID__0A9D95DB");

            entity.HasOne(d => d.Student).WithMany(p => p.Stcs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__STC__StudentID__09A971A2");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Stcs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__STC__TeacherID__08B54D69");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC27084EFF5D");

            entity.Property(e => e.Id).IsFixedLength();
        });

        modelBuilder.Entity<StudentViewForTeacher>(entity =>
        {
            entity.ToView("StudentViewForTeacher");

            entity.Property(e => e.Id).IsFixedLength();
        });

        modelBuilder.Entity<Tcgc>(entity =>
        {
            entity.HasKey(e => new { e.TeacherId, e.CourseId, e.GradeCompositionId }).HasName("PK__TCGC__2CD69DC11CACC9C4");

            entity.ToTable("TCGC", tb => tb.HasTrigger("TCGCInsertTrigger"));

            entity.Property(e => e.TeacherId).IsFixedLength();
            entity.Property(e => e.CourseId).IsFixedLength();

            entity.HasOne(d => d.Course).WithMany(p => p.Tcgcs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TCGC__CourseID__15DA3E5D");

            entity.HasOne(d => d.GradeComposition).WithMany(p => p.Tcgcs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TCGC__GradeCompo__16CE6296");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Tcgcs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TCGC__TeacherID__14E61A24");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Teacher__3214EC2778D9F0DE");

            entity.Property(e => e.Id).IsFixedLength();

            entity.HasMany(d => d.Courses).WithMany(p => p.Teachers)
                .UsingEntity<Dictionary<string, object>>(
                    "Tc",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TC__CourseID__4E88ABD4"),
                    l => l.HasOne<Teacher>().WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TC__TeacherID__4D94879B"),
                    j =>
                    {
                        j.HasKey("TeacherId", "CourseId").HasName("PK__TC__81608E5C073E1BAD");
                        j.ToTable("TC");
                        j.IndexerProperty<string>("TeacherId")
                            .HasMaxLength(14)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("TeacherID");
                        j.IndexerProperty<string>("CourseId")
                            .HasMaxLength(14)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("CourseID");
                    });
        });

        modelBuilder.Entity<TeacherViewForStudent>(entity =>
        {
            entity.ToView("TeacherViewForStudent");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
