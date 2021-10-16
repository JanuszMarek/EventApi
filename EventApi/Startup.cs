using BusinessLogic.Modules.EventModule.Services;
using BusinessLogic.Modules.EventParticipantModule.Services;
using BusinessLogic.Modules.EventTicketModule.Services;
using DataLayer;
using DataLayer.Repositories;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Interfaces.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace EventApi
{
    public class Startup
    {
        private IConfigurationRoot Configuration { get; set; }

        private IWebHostEnvironment CurrentEnvironment { get; set; }

        public Startup(IWebHostEnvironment env)
        {
            CurrentEnvironment = env;
            Configuration = BuildConfiguration();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSwagger(services);
            ConfigureDatabase(services);
            RegisterService(services);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                UseSwagger(app);
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(CurrentEnvironment.ContentRootPath);

            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (CurrentEnvironment.IsDevelopment())
            {
                builder.AddJsonFile($"appsettings.{CurrentEnvironment.EnvironmentName}.json", optional: true);
            }

            return builder.Build();
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            string connectionString;

            if (CurrentEnvironment.IsDevelopment())
            {
                connectionString = $"Server={Configuration["SQL:Server"]};Database={Configuration["SQL:Database"]};Trusted_Connection=True;MultipleActiveResultSets=True;ConnectRetryCount=0;";
            }
            else
            {
                connectionString = $"Server={Configuration["SQL:Server"]};Database={Configuration["SQL:Database"]};User ID={Configuration["SQL:LTCPSqlUserID"]};Password={Configuration["SQL:LTCPSqlUserPassword"]};";
            }

            int.TryParse(Configuration["SQL:CommandTimeout"], out int commandTimeout);

            services.AddDbContext<EventContext>(option =>
            {
                option.UseSqlServer(connectionString, b => b.MigrationsAssembly(nameof(EventApi)).CommandTimeout(commandTimeout));
            });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
            });
        }

        private static void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        private static void RegisterService(IServiceCollection services)
        {
            RegisterRepositories(services);
            RegisterBusinessServices(services);

            static void RegisterRepositories(IServiceCollection services)
            {
                services.AddTransient<IEventRepository, EventRepository>();
                services.AddTransient<IEventTicketRepository, EventTicketRepository>();
                services.AddTransient<IEventParticipantRepository, EventParticipantRepository>();
            }

            static void RegisterBusinessServices(IServiceCollection services)
            {
                services.AddTransient<IEventService, EventService>();
                services.AddTransient<IEventTicketService, EventTicketService>();
                services.AddTransient<IEventParticipantService, EventParticipantService>();
            }
        }
    }
}
