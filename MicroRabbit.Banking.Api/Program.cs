
using MediatR;
using MicroRabbit.Banking.Data.Context;
using Microsoft.EntityFrameworkCore;
using Rabbit.Infra.IoC;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MicroRabbit.Banking.Api
{
    public class Program
    {
 

 //       public IConfiguration Configuration { get; }

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            
            // Add services to the container.

            var connectionString =
                builder.Configuration.GetConnectionString("BankingDbConnection");


            services.AddDbContext<BankingDbContext>(options =>
            {
                options.UseSqlServer(//configuration.GetSection("ConnectionStrings:BankingDbConnection").Value);
                    connectionString);
            }
            );

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Banking Microservice", Version = "v1" });
            }
            );


            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            RegisterServices(services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","Banking Microservice v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }
    }
}