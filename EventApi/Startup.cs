using DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddSwaggerGen();
        }

        private static void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

    }
}
