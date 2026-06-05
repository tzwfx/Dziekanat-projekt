using CoreApp.Entities;
using Infrastructure.EntityFramework.Entities;
using Infrastructure.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Context;

public class UniversityDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Lecturer> Lecturers { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<DegreeProgram> DegreePrograms { get; set; }
    public DbSet<AcademicYear> AcademicYears { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public UniversityDbContext() { }

    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlite("Data Source=university.db");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>(entity =>
        {
            entity.Property(u => u.FirstName).HasMaxLength(100);
            entity.Property(u => u.LastName).HasMaxLength(100);
            entity.Property(u => u.Department).HasMaxLength(100);
            entity.HasIndex(u => u.Email).IsUnique();
        });

        builder.Entity<AppRole>(entity =>
        {
            entity.Property(r => r.Name).HasMaxLength(20);
        });

        builder.Entity<Person>()
            .HasDiscriminator<string>("PersonType")
            .HasValue<Student>("Student")
            .HasValue<Lecturer>("Lecturer");

        builder.Entity<Person>(entity =>
        {
            entity.Property(p => p.Email).HasMaxLength(200);
            entity.Property(p => p.FirstName).HasMaxLength(100);
            entity.Property(p => p.LastName).HasMaxLength(100);
            entity.Property(p => p.NationalId).HasMaxLength(11);
        });

        builder.Entity<Student>(entity =>
        {
            entity.Property(s => s.Status).HasConversion<string>();
            entity.Property(s => s.ProgramName).HasMaxLength(200);
        });

        builder.Entity<Lecturer>(entity =>
        {
            entity.Property(l => l.Title).HasMaxLength(50);
            entity.Property(l => l.Faculty).HasMaxLength(200);
        });

        builder.Entity<Grade>(entity =>
        {
            entity.Property(g => g.GradeValue).HasConversion<int>();
            entity.Property(g => g.GradeType).HasConversion<string>();
        });

        builder.Entity<DegreeProgram>(entity =>
        {
            entity.Property(d => d.Code).HasMaxLength(20);
            entity.Property(d => d.Name).HasMaxLength(200);
            entity.Property(d => d.Faculty).HasMaxLength(200);
            entity.Property(d => d.DegreeType).HasConversion<string>();
        });

        // Dane początkowe - role
        builder.Entity<AppRole>().HasData(
            new AppRole { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
            new AppRole { Id = "2", Name = "DeanOfficeWorker", NormalizedName = "DEANOFFICEWOKER" },
            new AppRole { Id = "3", Name = "Lecturer", NormalizedName = "LECTURER" },
            new AppRole { Id = "4", Name = "Student", NormalizedName = "STUDENT" }
        );
    }
}