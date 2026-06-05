using Infrastructure.EntityFramework.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest;

public class UniversityAppTestFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<UniversityDbContext>));
            if (dbContextDescriptor != null)
                services.Remove(dbContextDescriptor);

            services
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<UniversityDbContext>((container, options) =>
                {
                    options.UseInMemoryDatabase("UniversityTest")
                        .UseInternalServiceProvider(container);
                });
        });

        builder.UseEnvironment("Development");
    }
}