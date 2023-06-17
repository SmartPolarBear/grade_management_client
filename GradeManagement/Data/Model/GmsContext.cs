using System;
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

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Sc> Scs { get; set; }

    public virtual DbSet<St> Sts { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=gms;User ID=SA;Password=<YourStrong@Passw0rd>;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admin__3214EC27CBF5BD14");

            entity.ToTable("Admin");

            entity.Property(e => e.Id)
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Course__3214EC2734ABCF1C");

            entity.ToTable("Course");

            entity.Property(e => e.Id)
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Teacher)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sc>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.CourseId }).HasName("PK__SC__5E57FD614A559E46");

            entity.ToTable("SC");

            entity.Property(e => e.StudentId)
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("StudentID");
            entity.Property(e => e.CourseId)
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CourseID");

            entity.HasOne(d => d.Course).WithMany(p => p.Scs)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SC__CourseID__4316F928");

            entity.HasOne(d => d.Student).WithMany(p => p.Scs)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SC__StudentID__4222D4EF");
        });

        modelBuilder.Entity<St>(entity =>
        {
            entity.HasKey(e => new { e.TeacherId, e.StudentId }).HasName("PK__ST__6EDE0BE3B3237FA8");

            entity.ToTable("ST");

            entity.Property(e => e.TeacherId)
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TeacherID");
            entity.Property(e => e.StudentId)
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("StudentID");

            entity.HasOne(d => d.Student).WithMany(p => p.Sts)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ST__StudentID__52593CB8");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Sts)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ST__TeacherID__5165187F");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC27084EFF5D");

            entity.ToTable("Student");

            entity.Property(e => e.Id)
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Teacher__3214EC2778D9F0DE");

            entity.ToTable("Teacher");

            entity.Property(e => e.Id)
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
