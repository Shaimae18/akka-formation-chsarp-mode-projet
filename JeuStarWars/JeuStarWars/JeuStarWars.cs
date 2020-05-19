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
        public static Joueur currentJoueur;
        public static Grille grille;
        private static dynamic headerCursorPosition;
        private static IEnumerable<Position> listPosition;
        private static IDeplacementService deplacementService;
        private static IGrilleService grilleService;
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
           .AddScoped<IPersonnageService, PersonnageJoueurService>()
           .AddScoped<IDeplacementService, DeplacementService>()
           .AddScoped<IAttaqueService, AttaqueService>()
           .AddScoped<IGrilleService, GrilleService>()
           .BuildServiceProvider();
        }

      

        private static void WelcomeScreen()
        {
            Console.Clear();
            SetConsoleEntete();
            ChoixModePartie();
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
        private static void ReintialiseCursorPos()
        {
            ConsoleWriter.SetConsoleCursorPosition(headerCursorPosition.posLeft, headerCursorPosition.posTop);


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
            grilleService = serviceProvider.GetService<IGrilleService>();
            listPosition = grilleService.GetInitialPositionInGrille(currentJoueur, nombreEnnemie);
            grille = ConsoleWriter.SetGrille(18, 10, listPosition);
            RunTour();
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

            personnageService = serviceProvider.GetService<IPersonnageService>();
            var listePersonnage = personnageService.GetPersonnagesByCote(GetCote());

            List<string> listCharac = new List<string>()
            {
                "    Choisissez un personnage parmi les suivants:"
            };
            int i = 0;
            foreach (PersonnageJoueur p in listePersonnage)
            {
                listCharac.Add("<<<<   " + i + ": " + p.ToString() + "  >>>>");
                i++;
            }
            ConsoleWriter.SetFrame(listCharac, 90, 20 + i * 10);
            ConsoleWriter.SetEmptyLine(4);

            Console.Write("Tapez votre choix, puis appuyez sur entrée  ");
            i = int.Parse(Console.ReadLine());

            var personnage = new PersonnageJoueur(listePersonnage.ElementAt(i));
            currentJoueur = new Joueur
            {
                PersonnageJoueur = personnage,
                PointsMagie = personnage.PointsMagie,
                Portee = personnage.Portee,
                Degat = personnage.Degat,
                Etat = Etat.Vivant,
                TypeJoueur = TypeJoueur.Joueur

            };

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
        private static void SetJoueurInfo()
        {
            List<string> listContent = new List<string>()
            {
               currentJoueur.ToString()
            };
            ConsoleWriter.SetFrame(listContent, 90, 30);
        }


        private static void RunTour()
        {

            while (isMonTour && !isGameOver)
            {
                bool isMoved = true;
                bool isAttack = false;
                var currentPositionJoueur = listPosition.Where(p => p.Joueur.TypeJoueur == TypeJoueur.Joueur).FirstOrDefault();
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (!key.Modifiers.HasFlag(ConsoleModifiers.Shift))
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.LeftArrow:

                            if (currentPositionJoueur != null && CanMove(TypeDeplacement.Left, currentPositionJoueur))
                                ConsoleWriter.Left(currentPositionJoueur);
                            else
                                isMoved = false;
                            break;
                        case ConsoleKey.RightArrow:

                            if (currentPositionJoueur != null && CanMove(TypeDeplacement.Right, currentPositionJoueur))
                                ConsoleWriter.Right(currentPositionJoueur);
                            else
                                isMoved = false;
                            break;
                        case ConsoleKey.UpArrow:
                            if (currentPositionJoueur != null && CanMove(TypeDeplacement.Up, currentPositionJoueur))
                                ConsoleWriter.Up(currentPositionJoueur);
                            else
                                isMoved = false;
                            break;
                        case ConsoleKey.DownArrow:
                            if (currentPositionJoueur != null && CanMove(TypeDeplacement.Down, currentPositionJoueur))

                                ConsoleWriter.Down(currentPositionJoueur);
                            else
                                isMoved = false;
                            break;

                        default:
                            isMoved = false;

                            break;

                    }
                    if (isMoved)
                    {
                        listPosition.Where(p => p.Joueur.TypeJoueur == TypeJoueur.Joueur).FirstOrDefault().TopCursorPosition = currentPositionJoueur.TopCursorPosition = Console.CursorTop;
                        listPosition.Where(p => p.Joueur.TypeJoueur == TypeJoueur.Joueur).FirstOrDefault().LeftCursorPosition = currentPositionJoueur.LeftCursorPosition = Console.CursorLeft - 3;
                        isMonTour = false;
                        isMoved = false;
                        Thread.Sleep(1000);
                        MoveEnnemie();
                    }
                }
                else
                    isAttack = RunAttack(key, currentPositionJoueur);



            }
        }
        private static bool CanMove(TypeDeplacement typeDeplacement, Position currentPositionJoueur)
        {

            return deplacementService.CheckMoveValidity(typeDeplacement, currentPositionJoueur, grille)
                && deplacementService.CheckIfCaseIsEmpty(typeDeplacement, currentPositionJoueur, listPosition);
        }
        private static void MoveEnnemie()
        {
            var currentJoueurPosition = listPosition.Where(p => p.Joueur.TypeJoueur == TypeJoueur.Joueur).FirstOrDefault();
            Dictionary<TypeDeplacement, Position> dictPos = deplacementService.DeplacerLePlusProcheEnnemie(currentJoueurPosition, listPosition, grille);
            if (dictPos.Any())
                switch (dictPos.First().Key)
                {
                    case TypeDeplacement.Up:
                        ConsoleWriter.Up(dictPos.First().Value, TypeJoueur.Adversaire);
                        break;
                    case TypeDeplacement.Down:
                        ConsoleWriter.Down(dictPos.First().Value, TypeJoueur.Adversaire);
                        break;
                    case TypeDeplacement.Left:
                        ConsoleWriter.Left(dictPos.First().Value, TypeJoueur.Adversaire);
                        break;
                    case TypeDeplacement.Right:
                        ConsoleWriter.Right(dictPos.First().Value, TypeJoueur.Adversaire);
                        break;
                }
            listPosition.Where(p => p.LeftCursorPosition == dictPos.First().Value.LeftCursorPosition && p.TopCursorPosition == dictPos.First().Value.TopCursorPosition).FirstOrDefault().TopCursorPosition = Console.CursorTop;
            listPosition.Where(p => p.LeftCursorPosition == dictPos.First().Value.LeftCursorPosition && p.TopCursorPosition == dictPos.First().Value.TopCursorPosition).FirstOrDefault().LeftCursorPosition = Console.CursorLeft - 3;
            isMonTour = true;
        }
        private static bool RunAttack(ConsoleKeyInfo key, Position currentJoueurPosition)
        {
            attaqueService = serviceProvider.GetService<IAttaqueService>();
            bool isAttack = true;
            switch (key.Key)
            {
                case ConsoleKey.A:
                    if (currentJoueurPosition != null )
                    {
                       // dynamic border = new { LeftBorder = grille.LeftBorder, RightBorder = grille.RightBorder, TopBorder = grille.TopBorder, BottomBorder = grille.BottomBorder };
                        List<Position> listPosAuChampsDatt = attaqueService.GetChampsDattaques(currentJoueurPosition, currentJoueur.Portee,grille);
                        Position adversairAattaquerPos = attaqueService.GetAdversaireAattaquer(listPosition, listPosAuChampsDatt);
                       
                        if (adversairAattaquerPos != null)
                        {
                            if(attaqueService.Attaquer(currentJoueur, adversairAattaquerPos.Joueur)) 
                            ConsoleWriter.Attaquer(currentJoueurPosition, adversairAattaquerPos);

                        }

                    }
                    else
                        isAttack = false;
                    return isAttack;
                case ConsoleKey.S:
                    return true;
                default:
                    return false;
            }

        }
  

      

     

      

       

       

       

       
      
       

       
    }
}