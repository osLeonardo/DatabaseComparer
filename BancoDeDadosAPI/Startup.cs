using BancoDeDadosAPI.Interfaces;
using BancoDeDadosAPI.Services;
using Microsoft.OpenApi.Models;
using Neo4j.Driver;

namespace BancoDeDadosAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services for Cassandra and Neo4j
            services.AddSingleton(GraphDatabase.Driver("neo4j://localhost:7687", AuthTokens.Basic("neo4j", "neoadmin")));
            services.AddSingleton<ICassandraService, CassandraService>();
            services.AddSingleton<INeo4jService, Neo4jService>();

            services.AddControllers();

            // Add Swagger services
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}