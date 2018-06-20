using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EventFlow;
using EventFlow.Aspnetcore.Middlewares;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Autofac.Extensions;
using EventFlow.Extensions;
using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using EventFlow.MsSql.Extensions;
using EventFlow.MsSql.SnapshotStores;
using EventSourcing.Domain.Orders;
using EventSourcing.Domain.Orders.Projections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace EventSourcing
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Event Sourcing api",
                    Description = "This public facing api for access to Event Sourcing api",
                    TermsOfService = "None"
                });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            EventFlowOptions.New
                .UseAutofacContainerBuilder(builder)
                .UseMssqlEventStore()
                .UseMsSqlSnapshotStore()
                .ConfigureMsSql(MsSqlConfiguration.New.SetConnectionString(Configuration.GetConnectionString("EventFlow")))
                .AddDefaults(typeof(Startup).Assembly)
                .AddDefaults(typeof(OrderAggregate).Assembly)
                .UseMssqlReadModel<OrderReadModel>()
                .UseMssqlReadModel<OrderLineReadModel>()
                .AddAspNetCoreMetadataProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<CommandPublishMiddleware>();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Sourcing api V1");
            });

            var msSqlDatabaseMigrator = app.ApplicationServices.GetService<IMsSqlDatabaseMigrator>();
            EventFlowEventStoresMsSql.MigrateDatabase(msSqlDatabaseMigrator);
            EventFlowSnapshotStoresMsSql.MigrateDatabase(msSqlDatabaseMigrator);
        }
    }
}
