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
        private static dynamic sauvCursorPosition;
        private static dynamic headerCursorPosition;
        private static IEnumerable<Position> listPosition;
        private static bool gameIsOn;
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
            ChoixModePartie();
        }

        private static void NouvellePartie()
        {
            
            Console.SetCursorPosition(headerCursorPosition.posLeft,headerCursorPosition.posTop);
            ReintialiseCursorPos();
            ChoisirPersonnage();
            Prologue();
            ReintialiseCursorPos();
            SetInstruction();
            SetJoueurInfo();
            gameIsOn = true;
            var positionService = serviceProvider.GetService<IPositionService>();
             listPosition = positionService.GetInitialPosition(currentPersonnage, nombreEnnemie);
            ConsoleWriter.SetGrille(18, 10, listPosition);
            RunTour();

            sauvCursorPosition = new { left = Console.CursorLeft, top = Console.CursorTop };
           
          
        }

        private static void RunTour()
        {
          
            while (gameIsOn)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                var pos = listPosition.Where(p => p.Personnage.TypePersonnage != TypePersonnage.Ennemie).FirstOrDefault();
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                       
                        if (pos != null)
                        {
                            Console.SetCursorPosition(pos.LeftCursorPosition + 1, pos.TopCursorPosition);
                            Console.Write(" ");
                            Console.SetCursorPosition(pos.LeftCursorPosition + 1-5, pos.TopCursorPosition );
                            Console.Write("☺");
     
                        };
                        break;
                    case ConsoleKey.RightArrow:
                       
                        if (pos != null)
                        {
                            Console.SetCursorPosition(pos.LeftCursorPosition + 1, pos.TopCursorPosition);
                            Console.Write(" ");
                            Console.SetCursorPosition(pos.LeftCursorPosition + 1 + 5, pos.TopCursorPosition);
                            Console.Write("☺");

                        };
                        break;
                    case ConsoleKey.UpArrow:
                        if (pos != null)
                        {
                            Console.SetCursorPosition(pos.LeftCursorPosition + 1, pos.TopCursorPosition);
                            Console.Write(" ");
                            Console.SetCursorPosition(pos.LeftCursorPosition + 1, pos.TopCursorPosition-3);
                            Console.Write("☺");

                        };
                        break;
                    case ConsoleKey.DownArrow:
                       
                        if (pos != null)
                        {
                            Console.SetCursorPosition(pos.LeftCursorPosition + 1, pos.TopCursorPosition);
                            Console.Write(" ");
                            Console.SetCursorPosition(pos.LeftCursorPosition + 1, pos.TopCursorPosition + 3);
                            Console.Write("☺");

                        };
                        break;
                    case ConsoleKey.Escape:
                        // GameIsOn = false;
                        break;
                    default:
                        break;

                }
                listPosition.Where(p => p.Personnage.TypePersonnage != TypePersonnage.Ennemie).FirstOrDefault().TopCursorPosition = Console.CursorTop;
                listPosition.Where(p => p.Personnage.TypePersonnage != TypePersonnage.Ennemie).FirstOrDefault().LeftCursorPosition = Console.CursorLeft-2;
            }






        }

        private static void SetInstruction()
        {
            List<string> listContent = new List<string>()
            {
                 "Le but du jeu est de tuer tous les joueurs adverses.",
                 "Chaque joueur peut se déplacer d'une case vers l'avant, l'arrière, gauche " ,
                 "et à droite en utilisant les touches " ,
                 "directionnelles(← ↓ → ↑).",
                 "Si un joueur n'a plus de points de vie, il perd la partie.",
                
            };
            ConsoleWriter.SetFrame(listContent, 90, 30);
        }

        private static void ReintialiseCursorPos()
        {
            ConsoleWriter.SetConsoleCursorPosition(headerCursorPosition.posLeft, headerCursorPosition.posTop);
           

        }

        private static void Prologue()
        {
          
            List<string> listContent = new List<string>()
            {
              "L'agitation règne au Sénat",
              "Galactique. Des milliers de",
              "systèmes solaires ont annoncé",
              "leur intention de quitter la",
              "République.",
              "Confrontés à ce mouvement",
              "",
              "séparatiste mené par le",
              "mystérieux Comte Dooku, les",
              "chevaliers Jedi, en nombre",
              "limité, ont du mal à maintenir",
              "la paix et l'ordre dans la",
              "galaxie.",
              "La sénatrice Amidala,",
              "ancienne reine de Naboo,",
              "revient au Sénat Galactique",
              "participer à un vote crucial",
              "sur la création d'une ARMÉE",
              "DE LA RÉPUBLIQUE pour",
              "aider les Jedi débordés..."

            };
            ConsoleWriter.SetFrame(listContent, 50,50) ;
            Console.WriteLine("Cliquer sur une touche pour continuer");
            Console.ReadLine();
        
        }
        private static void SetJoueurInfo()
        {
            List<string> listContent = new List<string>()
            {
               currentPersonnage.ToString()
            };
            ConsoleWriter.SetFrame(listContent, 90,30) ;
        }

        private static void ChoisirPersonnage()
        {
            var personnageService = serviceProvider.GetService<IPersonnageService>();
            currentPersonnage = new Personnage("Obiwan", pointsVie: 250, pointsMagie: 150, typePersonnage: TypePersonnage.Hero) ;
        }

        public static void SetConsoleEntete()
        {
            var text = new WenceyWang.FIGlet.AsciiArt("Star Wars");
            List<string> listContent = text.Result.ToList();
            text = new WenceyWang.FIGlet.AsciiArt("L'attaque des clones");
            listContent.AddRange(text.Result.ToList());
            ConsoleWriter.SetFrame(listContent, 150,100);
            headerCursorPosition = new { posLeft = Console.CursorLeft, posTop = Console.CursorTop };

        }
        public static void ChoixModePartie()
        {
            ConsoleWriter.SetEmptyLine(2);
            List<string> listContent = new List<string>()
            {
                "    Choisissez une option parmi les options suivantes:",
                "<<<<    N: Pour commencer une nouvelle partie    >>>>" ,
                "<<<<   C: Pour reperendre une partie sauvgarder  >>>>",
                "<<<<               Q: Pour quitter               >>>>"
            };
            ConsoleWriter.SetFrame(listContent, 70,50);
            ConsoleWriter.SetEmptyLine(4);
            GetMode();
        }

        private static void GetMode()
        {
            Console.Write("Tapez votre choix, puis appuyez sur entrée  ");
            switch (Console.ReadLine())
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
                default:
                    GetMode();
                    break;

            }
        }
    }
}
