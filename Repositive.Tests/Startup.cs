[assembly: Xunit.TestFramework("Repositive.Tests.Startup", "Repositive.Tests")]
[assembly: Xunit.CollectionBehavior(DisableTestParallelization = true)]

//// ReSharper disable StyleCop.SA1126

namespace Repositive.Tests
{
    using System;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Repositive.Tests.Utilities;
    using Repositive.Tests.Utilities.Context;
    using Repositive.Tests.Utilities.Repositories;
    using Repositive.Tests.Utilities.Repositories.Contracts;
    using Xunit.Abstractions;
    using Xunit.DependencyInjection;

    /// <summary>
    ///     Provides an entry point for the program and methods for configuring it.
    /// </summary>
    public class Startup : DependencyInjectionTestFramework
    {
        /// <summary>
        ///     The name of the database used for the tests.
        /// </summary>
        private static readonly string DatabaseName = Guid.NewGuid().ToString();

        /// <summary>
        ///     Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="messageSink">
        ///     The endpoint for the reception of test messages.
        /// </param>
        public Startup(IMessageSink messageSink) : base(messageSink)
        {
        }

        /// <summary>
        ///     Configures the container services for the application.
        /// </summary>
        /// <param name="services">The collection of service descriptors.</param>
        protected void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RepositiveContext>(t => t.UseInMemoryDatabase(DatabaseName));
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleManufacturerRepository, VehicleManufacturerRepository>();
            services.AddScoped<DatabaseHelper>();
        }

        /// <summary>
        ///     Configures the application.
        /// </summary>
        /// <param name="provider">
        ///     The service provider.
        /// </param>
        protected override void Configure(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var databaseHelper = services.GetRequiredService<DatabaseHelper>();

                databaseHelper.InitDatabaseWithData();
            }
        }

        /// <summary>Creates the host builder, responsible for configuring the application.</summary>
        /// <param name="assemblyName">The assembly that is being executed.</param>
        /// <returns>Returns the host builder.</returns>
        protected override IHostBuilder CreateHostBuilder(AssemblyName assemblyName) =>
            base.CreateHostBuilder(assemblyName)
                .ConfigureServices(ConfigureServices);
    }
}
