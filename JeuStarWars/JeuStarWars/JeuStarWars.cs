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
        public static Grille grille;
        private static dynamic headerCursorPosition;
        private static IEnumerable<Position> listPosition;
        private static IDeplacementService deplacementService;
        private static IPersonnageService personnageService;
        private static IAttaqueService attaqueService;
        private static bool isMonTour;
        private static bool isGameOver;
        static void Main(string[] args)
        {
            ConfigureDependencies();
            ConsolePrametrage();
            WelcomeScreen();


        }

        private static void ConsolePrametrage()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.ForegroundColor = ConsoleColor.Green;
        }

        private static void ConfigureDependencies()
        {
            serviceProvider = new ServiceCollection()
           .AddScoped<IPersonnageService, PersonnageService>()
           .AddScoped<IDeplacementService, DeplacementService>()
           .AddScoped<IAttaqueService, AttaqueService>()
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

            Console.SetCursorPosition(headerCursorPosition.posLeft, headerCursorPosition.posTop);
            ReintialiseCursorPos();
            Prologue();
            ChoisirPersonnage();
            ReintialiseCursorPos();
            SetInstruction();
            SetJoueurInfo();
            isMonTour = true;
            isGameOver = false;
            deplacementService = serviceProvider.GetService<IDeplacementService>();
            listPosition = deplacementService.GetInitialPosition(currentPersonnage, nombreEnnemie);
            grille = ConsoleWriter.SetGrille(18, 10, listPosition);
            RunTour();
        }

         private static void RunTour()
        {

            while (isMonTour && !isGameOver)
            {
                bool isMoved=true;
                bool isAttack = false;
                var pos = listPosition.Where(p => p.Personnage.TypePersonnage != TypePersonnage.Ennemie).FirstOrDefault();
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (!key.Modifiers.HasFlag(ConsoleModifiers.Shift))
                {

                    switch (key.Key)
                    {
                        case ConsoleKey.LeftArrow:

                            if (pos != null && CanMove(TypeDeplacement.Left))
                                ConsoleWriter.Left(pos);
                            else
                                isMoved = false;
                            break;
                        case ConsoleKey.RightArrow:

                            if (pos != null && CanMove(TypeDeplacement.Right))
                                ConsoleWriter.Right(pos);
                            else
                                isMoved = false;
                            break;
                        case ConsoleKey.UpArrow:
                            if (pos != null && CanMove(TypeDeplacement.Up))
                                ConsoleWriter.Up(pos);
                            else
                                isMoved = false;
                            break;
                        case ConsoleKey.DownArrow:
                            if (pos != null && CanMove(TypeDeplacement.Down))

                                ConsoleWriter.Down(pos);
                            else
                                isMoved = false;
                            break;
                        
                        default:
                            if ((key.Modifiers & ConsoleModifiers.Control) != 0)
                            {
                                Console.Write("Ctrl + ");
                            }
                            isMoved = false;
                           
                            break;

                    }
                    if (isMoved )
                    {
                        listPosition.Where(p => p.Personnage.TypePersonnage != TypePersonnage.Ennemie).FirstOrDefault().TopCursorPosition = Console.CursorTop;
                        listPosition.Where(p => p.Personnage.TypePersonnage != TypePersonnage.Ennemie).FirstOrDefault().LeftCursorPosition = Console.CursorLeft - 3;
                        isMonTour = false;
                        isMoved = false;
                        Thread.Sleep(1000);
                        MoveEnnemie();
                    }
                }
                else
                    isAttack = RunAttack(key,pos);



            }
        }

        private static bool RunAttack(ConsoleKeyInfo key, Position pos)
        {
            attaqueService = serviceProvider.GetService<IAttaqueService>();
            bool isAttack = true;
                switch (key.Key)
                {
                    case ConsoleKey.L:
                    if (pos != null && CanAttack(TypeDeplacement.Left))
                        ConsoleWriter.LeftAttack(currentPersonnage, pos, listPosition) ;
                    else
                        isAttack = false;
                    return isAttack;
                    break;
                    case ConsoleKey.P:
                    if (pos != null && CanAttack(TypeDeplacement.Left))
                        ConsoleWriter.RigthAttack(currentPersonnage, pos, listPosition);
                    else
                        isAttack = false;
                    return isAttack;
                    break;
                    case ConsoleKey.Q:
                    if (pos != null && CanAttack(TypeDeplacement.Up))
                       ConsoleWriter.UpAttack(currentPersonnage, pos, listPosition);
                    else
                        isAttack = false;
                    return isAttack;
                    break;
                    case ConsoleKey.A:
                    if (pos != null && CanAttack(TypeDeplacement.Down))
                        ConsoleWriter.DownAttack(currentPersonnage, pos, listPosition);
                    else
                            isAttack = false;
                        return isAttack;
                    break;

                    default:
                    return isAttack;
                    break;


                    
               
            }
                
        }

        private static bool CanAttack(TypeDeplacement right)
        {
            return true;
        }

        private static void MoveEnnemie()
        {
            var currentPosition = listPosition.Where(p => p.Personnage.TypePersonnage != TypePersonnage.Ennemie).FirstOrDefault();
            Dictionary<TypeDeplacement, Position> dictPos = deplacementService.DeplacerLePlusProcheEnnemie(currentPosition, listPosition, grille);
            if (dictPos.Any())
                switch (dictPos.First().Key)
                {
                    case TypeDeplacement.Up:
                        ConsoleWriter.Up(dictPos.First().Value, TypePersonnage.Ennemie);
                        break;
                    case TypeDeplacement.Down:
                        ConsoleWriter.Down(dictPos.First().Value, TypePersonnage.Ennemie);
                        break;
                    case TypeDeplacement.Left:
                        ConsoleWriter.Left(dictPos.First().Value, TypePersonnage.Ennemie);
                        break;
                    case TypeDeplacement.Right:
                        ConsoleWriter.Right(dictPos.First().Value, TypePersonnage.Ennemie);
                        break;
                }
            listPosition.Where(p => p.LeftCursorPosition == dictPos.First().Value.LeftCursorPosition && p.TopCursorPosition == dictPos.First().Value.TopCursorPosition).FirstOrDefault().TopCursorPosition = Console.CursorTop;
            listPosition.Where(p => p.LeftCursorPosition == dictPos.First().Value.LeftCursorPosition && p.TopCursorPosition == dictPos.First().Value.TopCursorPosition).FirstOrDefault().LeftCursorPosition = Console.CursorLeft - 3;
            isMonTour = true;
        }

        private static bool CanMove(TypeDeplacement typeDeplacement)
        {
            var currentPosition = listPosition.Where(p => p.Personnage.TypePersonnage != TypePersonnage.Ennemie).FirstOrDefault();
            return deplacementService.CheckMoveValidity(typeDeplacement, currentPosition, grille)
                && deplacementService.CheckIfCaseIsEmpty(typeDeplacement, currentPosition, listPosition);
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
            ConsoleWriter.SetFrame(listContent, 50, 50);
            Console.WriteLine("Cliquer sur entrer pour continuer");
            Console.ReadLine();

        }
        private static void SetJoueurInfo()
        {
            List<string> listContent = new List<string>()
            {
               currentPersonnage.ToString()
            };
            ConsoleWriter.SetFrame(listContent, 90, 30);
        }

        private static void ChoisirPersonnage()

        {

            ConsoleWriter.SetEmptyLine(2);
            List<string> listContent = new List<string>()
            {
                "    Choisissez une option parmi les options suivantes:",
                "<<<<   L: Pour jouer du côté Lumineux   >>>>" ,
                "<<<<    O: Pour jouer du côté Obscur    >>>>"
            };
            ConsoleWriter.SetFrame(listContent, 70, 50);
            ConsoleWriter.SetEmptyLine(4);
            
            var personnageService = serviceProvider.GetService<IPersonnageService>();
            var listePersonnage = personnageService.GetPersonnagesByCote(GetCote());

            List<string> listCharac = new List<string>()
            {
                "    Choisissez un personnage parmi les suivants:"
            };
            int i = 0;
            foreach (Personnage p in listePersonnage)
            {
                listCharac.Add("<<<<   "+i+": "+p.ToString()+"  >>>>");
                i++;
            }
            ConsoleWriter.SetFrame(listCharac, 90, 20+i*10);
            ConsoleWriter.SetEmptyLine(4);

            Console.Write("Tapez votre choix, puis appuyez sur entrée  ");
            i = int.Parse(Console.ReadLine());

            currentPersonnage = new Personnage(listePersonnage.ElementAt(i));

        }

        public static void SetConsoleEntete()
        {
            var text = new WenceyWang.FIGlet.AsciiArt("Star Wars");
            List<string> listContent = text.Result.ToList();
            text = new WenceyWang.FIGlet.AsciiArt("L'attaque des clones");
            listContent.AddRange(text.Result.ToList());
            ConsoleWriter.SetFrame(listContent, 150, 100);
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
            ConsoleWriter.SetFrame(listContent, 70, 50);
            ConsoleWriter.SetEmptyLine(4);
            GetMode();
        }

        private static Cote GetCote()
        {
            Console.Write("Tapez votre choix, puis appuyez sur entrée  ");
            switch (Console.ReadLine())
            {
                case "l":
                case "L":
                    return Cote.Lumineux;
                case "o":
                case "O":
                    return Cote.Obscur;
                default:
                    return GetCote();
            }
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