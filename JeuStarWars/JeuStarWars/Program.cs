using ApplicationCore.Repository;
using ApplicationCore.services;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
                    services.AddScoped<IAttaqueService, AttaqueService>();
                    services.AddScoped<IDeplacementService, DeplacementService>();
                    services.AddScoped<IGrilleService, GrilleService>();
                    services.AddScoped<IJoueurService, JoueurService>();
                    services.AddScoped<IParametrageService, ParametrageService>();
                    services.AddScoped<IPartieService, PartieService>();
                    services.AddScoped<IPersonnageJoueurService, PersonnageJoueurService>();
                    services.AddScoped<IPNJService, PNJService>();
                    services.AddScoped<IPositionService, PositionService>();
                    services.AddScoped<ITourService, TourService>();
                    services.AddDbContext<DataContext>(options => options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));
                    services.AddHostedService<Startup>();
                });
            //.ConfigureLogging((context, logging) => {
            //    var env = context.HostingEnvironment;
            //    var config = context.Configuration.GetSection("Logging");
            //    logging.AddConfiguration(config);
            //    logging.AddConsole();
            //    logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            //});
    }
}
