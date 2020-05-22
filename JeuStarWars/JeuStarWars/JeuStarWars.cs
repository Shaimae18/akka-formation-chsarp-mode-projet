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
        public static Partie partie;
        public static Tour tour;
        public static Grille grille;
        private static dynamic _headerCursorPosition;
        private static dynamic _barreEtatCrsorPosition;
        private static dynamic _barreTourPosition;
        private static IEnumerable<Position> _listPosition;
        private static IDeplacementService _deplacementService;
        private static IGrilleService _grilleService;
        private static IPersonnageService _personnageService;
        private static IAttaqueService _attaqueService;
        private static bool _isMonTour;
        private static bool _isGameOver;
        private static bool _isPartieGagner;
        private static int _nbrTour=0;

        static void Main(string[] args)
        {
            ConfigureDependencies();
            ConsolePrametrage();
            WelcomeScreen();
            Console.WriteLine();

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
            ConsoleWriter.SetFrame(listContent, 120, 100);
            _headerCursorPosition = new { posLeft = Console.CursorLeft, posTop = Console.CursorTop };

        }
        private static void ReintialiseCursorPos()
        {
            ConsoleWriter.SetConsoleCursorPosition(_headerCursorPosition.posLeft, _headerCursorPosition.posTop);


        }

        public static void ChoixModePartie()
        {
            ConsoleWriter.SetEmptyLine(2);
            List<string> listContent = new List<string>()
            {
                "    Choisissez une option parmi les options suivantes:",
                "<<<<    N: Pour commencer une nouvelle partie    >>>>" ,
                "<<<<   C: Pour reperendre une partie sauvgarder  >>>>",
                "                      Prochainement                  ",
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
                    Environment.Exit(0);
                    break;
                default:
                    GetMode();
                    break;

            }
        }
        private static void NouvellePartie()
        {
            partie = new Partie();
            

            Console.SetCursorPosition(_headerCursorPosition.posLeft, _headerCursorPosition.posTop);
            ReintialiseCursorPos();
            Prologue();
            ReintialiseCursorPos();
            ChoisirPersonnage();
            ReintialiseCursorPos();
            SetInstruction();
            _barreEtatCrsorPosition = new { posLeft = Console.CursorLeft, posTop = Console.CursorTop };
            SetJoueurInfo(currentJoueur);
            _isMonTour = true;
            _isGameOver = false;
            _isPartieGagner = false;
            _deplacementService = serviceProvider.GetService<IDeplacementService>();
            _grilleService = serviceProvider.GetService<IGrilleService>();
            _attaqueService = serviceProvider.GetService<IAttaqueService>();
            _listPosition = _grilleService.GetInitialPositionInGrille(currentJoueur, nombreEnnemie);
            grille = ConsoleWriter.SetGrille(18, 10, _listPosition);
            _barreTourPosition = new { posLeft = Console.CursorLeft, posTop = Console.CursorTop };
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
                   
                    ReintialiseCursorPos();
                    ConsoleWriter.SetEmptyLine(15);
                    ReintialiseCursorPos();
                    ConsoleWriter.SetEmptyLine(4);
                    return Cote.Lumineux;
                case "o":
                case "O":

                    ReintialiseCursorPos();
                    ConsoleWriter.SetEmptyLine(15);
                    ReintialiseCursorPos();
                    ConsoleWriter.SetEmptyLine(4);
                    return Cote.Obscur;
                default:
                    return GetCote();
            }
        }
        private static void ChoisirPersonnage()
        {
            ConsoleWriter.SetEmptyLine(4);
            List<string> listContent = new List<string>()
            {
                "    Choisissez une option parmi les options suivantes:",
                "<<<<   L: Pour jouer du côté Lumineux   >>>>" ,
                "<<<<    O: Pour jouer du côté Obscur    >>>>"
            };
            ConsoleWriter.SetFrame(listContent, 90, 50);
           
            _personnageService = serviceProvider.GetService<IPersonnageService>();
            ConsoleWriter.SetEmptyLine(15);
            var listePersonnage = _personnageService.GetPersonnagesByCote(GetCote());
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
            ConsoleWriter.SetEmptyLine(12);
            i = GetPersonnageChoisi();
            ReintialiseCursorPos();
          

            var personnage = new PersonnageJoueur(listePersonnage.ElementAt(i));
            currentJoueur = new Joueur
            {
                Personnage = personnage,
                PointsMagie = personnage.PointsMagie,
                PointsExperiences = 0,
                PointsVie = personnage.PointsVie,
                Portee = personnage.Portee,
                Degat = personnage.Degat,
                Etat = Etat.Vivant,
                TypeJoueur = TypeJoueur.Joueur

            };

        }

        private static int GetPersonnageChoisi()
        {
            int res = 0;
            Console.Write("Tapez votre choix, puis appuyez sur entrée  ");
            string choix = Console.ReadLine();
            if (int.TryParse(choix, out res))
                return int.Parse(choix);
            else
                return GetPersonnageChoisi();
        }

        private static void SetInstruction()
        {
            List<string> listContent = new List<string>()
            {
                 "Le but du jeu est de tuer tous les joueurs adverses.",
                 "Le joueur peut se déplacer d'une case vers l'avant, l'arrière, gauche " ,
                 "et à droite en utilisant les touches " ,
                 "directionnelles(← ↓ → ↑).",
                 "Le joueur peut lancer des attaques vers les adversaires " ,
                 "dans leur champs d'attaque en utilisant la combinaison" ,
                  "de touches (Shift+A)" ,
                 "Si un joueur n'a plus de points de vie, il perd la partie.",

            };
            ConsoleWriter.SetFrame(listContent, 90, 30);
        }
        private static void SetJoueurInfo(Joueur joueur, ConsoleColor color = ConsoleColor.Green)
        {
            List<string> listContent = new List<string>()
            {
               joueur.ToString()
            };
            ConsoleWriter.SetFrame(listContent, 90, 30,color: color);
        }

       
        private static void RunTour()
        {
            while (_isMonTour && !_isGameOver && !_isPartieGagner)
            {
                
                bool isMoved = true;
                var currentPositionJoueur = _listPosition.Where(p => p.Joueur.TypeJoueur == TypeJoueur.Joueur).FirstOrDefault();

                Joueur joueurMort = RunAttack(currentPositionJoueur);

                if (joueurMort!= null && joueurMort.TypeJoueur == TypeJoueur.Joueur)
                    _isGameOver = true;
                InitializerNouveauTour(ActionTour.Deplacement);
                tour.JoueurEnAttaque = currentJoueur;
                tour.message = " déplacez-vous à l'aide des touches directionnelles(← ↓ → ↑).";
                SetTourInfo();

                ConsoleKeyInfo key = Console.ReadKey(true);
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
                 
                    _listPosition.Where(p => p.Joueur.TypeJoueur == TypeJoueur.Joueur).FirstOrDefault().TopCursorPosition = currentPositionJoueur.TopCursorPosition = Console.CursorTop;
                        _listPosition.Where(p => p.Joueur.TypeJoueur == TypeJoueur.Joueur).FirstOrDefault().LeftCursorPosition = currentPositionJoueur.LeftCursorPosition = Console.CursorLeft - 3;
                        _isMonTour = false;
                        isMoved = false;
                        Thread.Sleep(1000);
                        TourAdversaire(currentPositionJoueur);
                        Thread.Sleep(1000);
                }
                    



            }
            if(_isGameOver)
            {
                var text = new WenceyWang.FIGlet.AsciiArt("Game Over");
                List<string> title = text.Result.ToList();
                int lenght = title.First().Length;
                Console.Clear();
                ConsoleWriter.SetEmptyLine(10);
                foreach (string row in title)
                {
                    Console.Write(new string(' ', ((Console.WindowWidth - lenght) < 0 ? 0 : (Console.WindowWidth - lenght)) / 2));
                    Console.WriteLine(row);
                    
                }
                Console.ReadLine();

            }
            else if(_isPartieGagner)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                var text = new WenceyWang.FIGlet.AsciiArt("Vous avez gagner");
                List<string> title = text.Result.ToList();
                int lenght = title.First().Length;
                Console.Clear();
                ConsoleWriter.SetEmptyLine(10);
                foreach (string row in title)
                {
                    Console.Write(new string(' ', ((Console.WindowWidth - lenght) < 0 ? 0 : (Console.WindowWidth - lenght)) / 2));
                    Console.WriteLine(row);
                }
                Console.ReadLine();
            }

        }

        private static void InitializerNouveauTour(ActionTour mode)
        {
            tour = new Tour();
            _nbrTour++;
            tour.NumeroDuTour = _nbrTour;
            tour.ActionTour = mode;
            partie.ListTours.Add(tour);
        }

        private static bool CanMove(TypeDeplacement typeDeplacement, Position currentPositionJoueur)
        {

            return _deplacementService.CheckMoveValidity(typeDeplacement, currentPositionJoueur, grille)
                && _deplacementService.CheckIfCaseIsEmpty(typeDeplacement, currentPositionJoueur, _listPosition);
        }
        private static void TourAdversaire(Position currentPositionJoueur)
        {
            if (_listPosition.Where(p => p.Joueur.TypeJoueur == TypeJoueur.Adversaire && p.Joueur.Etat != Etat.Mort).Any())
            {
                List<Dictionary<TypeDeplacement, Position>> listDeplacementAdv =  _deplacementService.DeplacerTousEnnemie(currentPositionJoueur, _listPosition,grille);
                InitializerNouveauTour(ActionTour.Deplacement);
                tour.JoueurEnAttaque = _listPosition.FirstOrDefault(p => p.Joueur.TypeJoueur == TypeJoueur.Adversaire &&  p.Joueur.Etat != Etat.Mort).Joueur;
                listDeplacementAdv.ForEach(dictPos =>
                {
                   Position currentPosition = _listPosition.Where(p => p.LeftCursorPosition == dictPos.First().Value.LeftCursorPosition && p.TopCursorPosition == dictPos.First().Value.TopCursorPosition).FirstOrDefault();
                   
                    SetTourInfo();
                    
                    switch (dictPos.First().Key)
                    {
                        case TypeDeplacement.Up:
                            ConsoleWriter.Up(dictPos.First().Value, TypeJoueur.Adversaire);
                            currentPosition.LeftCursorPosition = Console.CursorLeft - 3;
                            currentPosition.TopCursorPosition = Console.CursorTop;
                            break;
                        case TypeDeplacement.Down:
                            ConsoleWriter.Down(dictPos.First().Value, TypeJoueur.Adversaire);
                            currentPosition.LeftCursorPosition = Console.CursorLeft-3;
                            currentPosition.TopCursorPosition =  Console.CursorTop;
                    
                            break;
                        case TypeDeplacement.Left:
                            ConsoleWriter.Left(dictPos.First().Value, TypeJoueur.Adversaire);
                            currentPosition.LeftCursorPosition = Console.CursorLeft - 3;
                            currentPosition.TopCursorPosition = Console.CursorTop;
                            break;
                        case TypeDeplacement.Right:
                            ConsoleWriter.Right(dictPos.First().Value, TypeJoueur.Adversaire);
                            currentPosition.LeftCursorPosition = Console.CursorLeft - 3;
                            currentPosition.TopCursorPosition = Console.CursorTop;
                            break;
                    }
                  
                }
                );

                _isMonTour = true;
            }
            else
                _isPartieGagner = true;
           
        }
        private static Joueur RunAttack(Position currentJoueurPosition)
        {
            bool isJoueurDead = false;
            bool isAdversaireDead = false;
           
          
            if (currentJoueurPosition != null)
            {
                List<Position> listPosAuChampsDatt = _attaqueService.GetChampsDattaques(currentJoueurPosition, currentJoueur.Portee, grille);
                Position adversairAattaquerPos = _attaqueService.GetAdversaireAattaquer(_listPosition, listPosAuChampsDatt);
                if (adversairAattaquerPos != null)
                {
                    do
                    {
                        adversairAattaquerPos.Joueur.OnAttack = true;


                        InitializerNouveauTour(ActionTour.Attaque);
                        tour.JoueurEnAttaque = currentJoueur;
                        tour.JoueurEndefense = adversairAattaquerPos.Joueur;
                        SetTourInfo();
                        isAdversaireDead = _attaqueService.Attaquer(currentJoueur, adversairAattaquerPos.Joueur);
                        ConsoleWriter.Attaquer(currentJoueurPosition, adversairAattaquerPos, isAdversaireDead);
                        ActualiserBarEtat(currentJoueur);

                        if (isAdversaireDead)
                            return adversairAattaquerPos.Joueur;
                        if (Console.KeyAvailable)
                            return null;
                        TourAdversaire(currentJoueurPosition);
                        Thread.Sleep(2000);
                        InitializerNouveauTour(ActionTour.Attaque);
                        tour.JoueurEnAttaque = adversairAattaquerPos.Joueur;
                        tour.JoueurEndefense = currentJoueur;
                        SetTourInfo();
                        isJoueurDead = _attaqueService.Attaquer(adversairAattaquerPos.Joueur, currentJoueur);
                        ConsoleWriter.Attaquer(adversairAattaquerPos, currentJoueurPosition, isJoueurDead);
                        ActualiserBarEtat(adversairAattaquerPos.Joueur);

                   
                    } while (!isJoueurDead && !isAdversaireDead && !Console.KeyAvailable) ;
                    adversairAattaquerPos.Joueur.OnAttack = false;
                }
                if (isJoueurDead)
                    return currentJoueur;
                else if (isAdversaireDead)
                    return adversairAattaquerPos.Joueur;
                else return null;
            }
            else
                return  null;
        }

        private static void ActualiserBarEtat(Joueur joueur)
        {
            ConsoleWriter.SetConsoleCursorPosition(_barreEtatCrsorPosition.posLeft, _barreEtatCrsorPosition.posTop);
            SetJoueurInfo(joueur);
        }
        private static void SetTourInfo()
        {
            ConsoleWriter.SetConsoleCursorPosition(_barreTourPosition.posLeft, _barreTourPosition.posTop);
            List<string> listContent = new List<string>()
            {
               tour.ToString()
            };
            ConsoleWriter.SetFrame(listContent, 90, 30,ConsoleColor.Green);
        }
    }
}