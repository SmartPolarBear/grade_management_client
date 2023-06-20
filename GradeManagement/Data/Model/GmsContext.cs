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

    public virtual DbSet<AdminViewForStudent> AdminViewForStudents { get; set; }

    public virtual DbSet<AdminViewForTeacher> AdminViewForTeachers { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Sc> Scs { get; set; }

    public virtual DbSet<Scaudit> Scaudits { get; set; }

    public virtual DbSet<Stc> Stcs { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentViewForTeacher> StudentViewForTeachers { get; set; }

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
        });

        modelBuilder.Entity<Sc>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.CourseId }).HasName("PK__SC__5E57FD61587EA494");

            entity.Property(e => e.StudentId).IsFixedLength();
            entity.Property(e => e.CourseId).IsFixedLength();

            entity.HasOne(d => d.Course).WithMany(p => p.Scs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SC__CourseID__03F0984C");

            entity.HasOne(d => d.Student).WithMany(p => p.Scs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SC__StudentID__02FC7413");
        });

        modelBuilder.Entity<Scaudit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SCAudit__3214EC27450AE732");

            entity.Property(e => e.CourseId).IsFixedLength();
            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.StudentId).IsFixedLength();

            entity.HasOne(d => d.Course).WithMany(p => p.Scaudits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SCAudit__CourseI__6754599E");

            entity.HasOne(d => d.Student).WithMany(p => p.Scaudits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SCAudit__Student__66603565");
        });

        modelBuilder.Entity<Stc>(entity =>
        {
            entity.HasKey(e => new { e.TeacherId, e.StudentId, e.CourseId }).HasName("PK__STC__E8172692224D076E");

            entity.Property(e => e.TeacherId).IsFixedLength();
            entity.Property(e => e.StudentId).IsFixedLength();
            entity.Property(e => e.CourseId).IsFixedLength();

            entity.HasOne(d => d.Course).WithMany(p => p.Stcs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__STC__CourseID__797309D9");

            entity.HasOne(d => d.Student).WithMany(p => p.Stcs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__STC__StudentID__787EE5A0");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Stcs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__STC__TeacherID__778AC167");
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
