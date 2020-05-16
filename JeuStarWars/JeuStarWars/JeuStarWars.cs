using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using JeuStarWars.Utilities;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using ApplicationCore.services;
using TypePersonnage = Entities.TypePersonnage;
using System.Linq;

namespace JeuStarWars
{
    class JeuStarWars
    {
        public static ServiceProvider serviceProvider;
        public static int nombreEnnemie = 10;
        public static Personnage currentPersonnage;
        static void Main(string[] args)
        {
            ConfigureDependencies();
            WelcomeScreen();
            

        }
        private static void ConfigureDependencies()
        {
            serviceProvider = new ServiceCollection()
           .AddScoped<IPersonnageService, PersonnageService>()
           .AddScoped<IPositionService, PositionService>()
           .BuildServiceProvider();
        }


        private static void WelcomeScreen()
        {
            Console.Clear();
            SetConsoleEntete();
            var mode = ChoixModePartie();
            switch (mode)
            {
                case "n":
                case "N":
                    NouvellePartie();
                    break;
                case "c":
                case "C":
                   
                    break;
                case "q":
                case "Q":
                    break;
              
            }

           
        }

        private static void NouvellePartie()
        {
            Console.Clear();
            SetConsoleEntete();
            ChoisirPersonnage();
            List<string> listContent = new List<string>()
            {
               currentPersonnage.ToString()
            };
            ConsoleWriter.SetFrame(listContent, 90);
           
            var positionService = serviceProvider.GetService<IPositionService>();
            IEnumerable<Position> listPosition = positionService.GetInitialPosition(currentPersonnage, nombreEnnemie);
            ConsoleWriter.SetGrille(18, 10, listPosition) ;
            var pos = listPosition.Where(p => p.Personnage.TypePersonnage != TypePersonnage.Ennemie).FirstOrDefault();
            if(pos!=null)
            {
                Console.SetCursorPosition(pos.LeftCursorPosition+1, pos.TopCursorPosition);
                Console.Write(" ");
                Console.ReadLine();
            }
          
        }

       

        private static void ChoisirPersonnage()
        {
            var personnageService = serviceProvider.GetService<IPersonnageService>();
            currentPersonnage = new Personnage("Obiwan", pointsVie: 250, pointsMagie: 150, typePersonnage: TypePersonnage.Hero) ;
        }

        public static void SetConsoleEntete()
        {
            List<string> listContent = new List<string>()
            {
                "Star Wars",
                "L'attaque des clones"
            };
            ConsoleWriter.SetFrame(listContent,90);
        }
        public static string ChoixModePartie()
        {
            ConsoleWriter.SetEmptyLine(2);
            List<string> listContent = new List<string>()
            {
                "Choisissez une option parmi les options suivantes:",
                "N - Pour commencer une nouvelle partie" ,
                "C - Pour reperendre une partie sauvgarder",
                "Q - Pour quitter"
            };
            ConsoleWriter.SetFrame(listContent, 50);
            ConsoleWriter.SetEmptyLine(4);
            Console.Write("Tapez votre choix, puis appuyez sur entrée  ");
            return Console.ReadLine();
        }
    }
}
