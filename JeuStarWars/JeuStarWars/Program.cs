using ApplicationCore.Repository;
using ApplicationCore.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace JeuStarWars
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .ConfigureAppConfiguration((hostingContext, config) =>
             {
                 config.AddJsonFile("appsettings.json", optional: true);
                 config.AddEnvironmentVariables();

                 if (args != null)
                 {
                     config.AddCommandLine(args);
                 }
             })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IPersonnageJoueurService, PersonnageJoueurService>()
                    .AddScoped<IDeplacementService, DeplacementService>()
                    .AddScoped<IAttaqueService, AttaqueService>()
                    .AddScoped<IGrilleService, GrilleService>();
                    services.AddDbContext<DataContext>(options => options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));
                    services.AddHostedService<Startup>();
                });
    }
}
