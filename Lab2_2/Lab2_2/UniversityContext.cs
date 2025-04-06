using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lab2_2;

public partial class UniversityContext : DbContext
{
    public UniversityContext()
    {
    }

    public UniversityContext(DbContextOptions<UniversityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<EfmigrationsLock> EfmigrationsLocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=D:\\\\\\\\C# Projects\\\\\\\\AcademyIT\\\\\\\\Lab2_2\\\\\\\\Lab2_2\\\\\\\\university.db")
        .UseLazyLoadingProxies()
        .LogTo(Console.WriteLine, LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EfmigrationsLock>(entity =>
        {
            entity.ToTable("__EFMigrationsLock");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Course>()
        .HasMany(c => c.Teachers)
        .WithMany(t => t.Courses);

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Students)
            .WithMany(s => s.Courses);


        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Title = "Математика", Duration = 70, Description = "Введение в математику" },
            new Course { Id = 2, Title = "История", Duration = 70, Description = "Мировая история" }
        );

        modelBuilder.Entity<Teacher>().HasData(
            new Teacher { Id = 1, Name = "Александр Петров" },
            new Teacher { Id = 2, Name = "Дмитрий Брюханов" }
        );

        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, Name = "Геннадий Полетаев" },
            new Student { Id = 2, Name = "Максим Марков" }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
