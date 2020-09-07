[assembly: Xunit.CollectionBehavior(DisableTestParallelization = true)]

namespace Repositive.EntityFrameworkCore.Tests
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Repositive.Abstractions;
    using Repositive.EntityFrameworkCore.Tests.Utilities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Context;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Standard;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.UnitOfWork;

    /// <summary>
    ///     Provides an entry point for the program and methods for configuring it.
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     The name of the database used for the tests.
        /// </summary>
        private static readonly string DatabaseName = Guid.NewGuid().ToString();

        /// <summary>
        ///     Configures the container services for the application.
        /// </summary>
        /// <param name="services">The collection of service descriptors.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Standard
            services
                .AddDbContext<RepositiveContext>(t => t.UseInMemoryDatabase(DatabaseName))
                .AddScoped<IPersonRepository, PersonRepository>()
                .AddScoped<IVehicleRepository, VehicleRepository>();

            // Unit of Work
            services
                .AddDbContext<RepositiveUoWContext>(t => t.UseInMemoryDatabase(DatabaseName))
                .AddScoped<IUnitOfWork, UnitOfWork<RepositiveUoWContext>>()
                .AddScoped<IPersonUoWRepository, PersonUoWRepository>()
                .AddScoped<IVehicleUoWRepository, VehicleUoWRepository>()
                .AddScoped<IManufacturerUoWRepository, ManufacturerUoWRepository>();

            services.AddScoped<DatabaseHelper>();
        }

        /// <summary>
        ///     Configures the application.
        /// </summary>
        /// <param name="provider">
        ///     The service provider.
        /// </param>
        public void Configure(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var databaseHelper = services.GetRequiredService<DatabaseHelper>();

                databaseHelper.InitDatabaseWithData();
            }
        }
    }
}