using CoreApp.Authorization;
using CoreApp.Entities;
using CoreApp.Repositories;
using CoreApp.Services;
using CoreApp.UnitOfWork;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.Entities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.UnitOfWork;
using Infrastructure.Memory;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class UniversityInfrastructureModule
{
    public static IServiceCollection AddUniversityEfModule(
        this IServiceCollection services,
        IConfiguration configuration)
    
    {
        services.AddDbContext<UniversityDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("UniversityDb"))
            .ConfigureWarnings(w => w.Ignore(
                Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));

        services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength         = 8;
                options.Password.RequireUppercase       = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail         = true;
                options.SignIn.RequireConfirmedEmail    = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(15);
            })
            .AddEntityFrameworkStores<UniversityDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IStudentRepository,  EfStudentRepository>();
        services.AddScoped<ILecturerRepository, EfLecturerRepository>();
        services.AddScoped<IGradeRepository,    EfGradeRepository>();
        services.AddScoped<IGenericRepositoryAsync<Course>>(sp =>
            new EfGenericRepository<Course>(sp.GetRequiredService<UniversityDbContext>().Courses));
        services.AddScoped<IGenericRepositoryAsync<AcademicYear>>(sp =>
            new EfGenericRepository<AcademicYear>(sp.GetRequiredService<UniversityDbContext>().AcademicYears));
        services.AddScoped<IGenericRepositoryAsync<DegreeProgram>>(sp =>
            new EfGenericRepository<DegreeProgram>(sp.GetRequiredService<UniversityDbContext>().DegreePrograms));
        services.AddScoped<IUniversityUnitOfWork, EfUniversityUnitOfWork>();
        services.AddScoped<IStudentService,      MemoryStudentService>();
        services.AddScoped<ILecturerService,     MemoryLecturerService>();
        services.AddScoped<IAuthService,         AuthService>();
        services.AddScoped<IDataSeeder,      IdentityDbSeeder>();

        return services;
    }

    public static IServiceCollection AddJwt(
        this IServiceCollection services,
        JwtSettings jwtOptions)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer           = true,
                    ValidateAudience         = true,
                    ValidateLifetime         = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer              = jwtOptions.Issuer,
                    ValidAudience            = jwtOptions.Audience,
                    IssuerSigningKey         = jwtOptions.GetSymmetricKey(),
                    ClockSkew                = TimeSpan.Zero
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AppPolicies.AdminOnly.ToString(), policy =>
                policy.RequireRole(UserRole.Administrator.ToString()));

            options.AddPolicy(AppPolicies.DeanOfficeWorkerOnly.ToString(), policy =>
                policy.RequireRole(UserRole.DeanOfficeWorker.ToString()));

            options.AddPolicy(AppPolicies.LecturerOnly.ToString(), policy =>
                policy.RequireRole(UserRole.Lecturer.ToString()));

            options.AddPolicy(AppPolicies.StudentOnly.ToString(), policy =>
                policy.RequireRole(UserRole.Student.ToString()));

            options.AddPolicy(AppPolicies.ActiveUser.ToString(), policy =>
                policy
                    .RequireAuthenticatedUser()
                    .RequireClaim("status", SystemUserStatus.Active.ToString()));

            options.DefaultPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        return services;
    }

    public static IServiceCollection AddUniversityMemoryModule(
        this IServiceCollection services)
    {
        services.AddSingleton<IStudentRepository,  MemoryStudentRepository>();
        services.AddSingleton<ILecturerRepository, MemoryLecturerRepository>();
        services.AddSingleton<IGradeRepository,    MemoryGradeRepository>();
        services.AddSingleton<IGenericRepositoryAsync<Course>,        MemoryCourseRepository>();
        services.AddSingleton<IGenericRepositoryAsync<AcademicYear>,  MemoryAcademicYearRepository>();
        services.AddSingleton<IGenericRepositoryAsync<DegreeProgram>, MemoryDegreeProgramRepository>();
        services.AddSingleton<IUniversityUnitOfWork, MemoryUniversityUnitOfWork>();
        services.AddSingleton<IStudentService,       MemoryStudentService>();
        services.AddSingleton<ILecturerService,      MemoryLecturerService>();

        return services;
    }
}