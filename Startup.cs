using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CarvedRock.Data;
using Npgsql;
using CarvedRock.Repositories;
using CarvedRock.GraphQL;
using Microsoft.Extensions.Hosting;

namespace CarvedRock
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
            services.AddControllers();
            
            //Configuring db and dbcontext
            var connectionString = Configuration["PostgreSql:ConnectionString"];
            var dbPassword = Configuration["PostgreSql:DbPassword"];
            var builder = new NpgsqlConnectionStringBuilder(connectionString){
                Password = dbPassword
            };

            services.AddDbContext<CarvedRockDbContext>(options => 
                options.UseNpgsql(builder.ConnectionString)
            );
            services.AddScoped<IDependencyResolver>(s=> new FuncDependencyResolver(
                                                    s.GetRequiredService));

            services.AddScoped<ProductRepository>();
            services.AddScoped<ProductReviewRepository>();
          
            services.AddScoped<CarvedRockSchema>();
            services.AddGraphQL(o => {o.ExposeExceptions=true;})
                    .AddGraphTypes(ServiceLifetime.Scoped)
                    .AddDataLoader();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CarvedRockDbContext dbContext)
        {
            app.UseGraphQL<CarvedRockSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            dbContext.Seed();

            // app.UseHttpsRedirection();

            // app.UseRouting();

            // app.UseAuthorization();

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllers();
            // });
        }
    }
}
