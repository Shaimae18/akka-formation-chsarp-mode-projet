using ApplicationCore.services;
using Entities;

using JeuStarWars.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JeuStarWars
{
    public class Startup: BackgroundService
    {

        #region Services
        public IConfiguration Configuration { get; }
        private readonly IDeplacementService _deplacementService;
        private readonly IPartieService _partieService;
        private readonly IPersonnageJoueurService _personnageService;
        private readonly IAttaqueService _attaqueService;
        private readonly IParametrageService _parametrageService;
        private readonly IPositionService _positionService;
        private readonly IJoueurService _joueurService;
        private readonly IPNJService _pNJService;
        private readonly ITourService _tourService;

        #endregion

        public  Joueur currentJoueur;
        public  Partie partie;
        public  Tour tour;
        public  Grille grille;

        private List<Parametrage> _lstParametrages;
        private IEnumerable<Position> _listPosition;
        private  dynamic _headerCursorPosition;
        private  dynamic _barreEtatCrsorPosition;
        private  dynamic _barreTourPosition;
        private  bool _isMonTour;
        private  bool _isGameOver;
        private  bool _isPartieGagner;
        private int _nbr;

        public Startup
            (
            IConfiguration configuration,
            IDeplacementService DeplacementService,
            IPartieService PartieService,
            IPersonnageJoueurService PersonnageJoueurService,
            IAttaqueService AttaqueService,
            IParametrageService ParametrageService,
            IPositionService PositionService,
            IJoueurService JoueurService,
            IPNJService   PNJService,
            ITourService TourService
            )
        {
            Configuration = configuration;
            _deplacementService = DeplacementService;
            _partieService = PartieService;
            _personnageService = PersonnageJoueurService;
            _attaqueService = AttaqueService;
            _parametrageService = ParametrageService;
            _positionService = PositionService;
            _joueurService = JoueurService;
            _pNJService = PNJService;
            _tourService = TourService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                StartNewGame();
                await Task.Delay(100, stoppingToken);
            }
        }

        private void StartNewGame()
        {
         
            _lstParametrages = new List<Parametrage>();
            ConsolePrametrage();
            WelcomeScreen();
            Console.WriteLine();
        }

        private  void ConsolePrametrage()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.ForegroundColor = ConsoleColor.Green;

        }
        private void WelcomeScreen()
        {
            Console.Clear();
            SetConsoleEntete();
            ChoixModePartie();
        }
        public   void SetConsoleEntete()
        {
            var text = new WenceyWang.FIGlet.AsciiArt("Star Wars");
            List<string> listContent = text.Result.ToList();
            text = new WenceyWang.FIGlet.AsciiArt("L'attaque des clones");
            listContent.AddRange(text.Result.ToList());
            ConsoleWriter.SetFrame(listContent, 120, 100);
            _headerCursorPosition = new { posLeft = Console.CursorLeft, posTop = Console.CursorTop };

        }
        private   void ReintialiseCursorPos()
        {
            ConsoleWriter.SetConsoleCursorPosition(_headerCursorPosition.posLeft, _headerCursorPosition.posTop);


        }

        public   void ChoixModePartie()
        {
            ConsoleWriter.SetEmptyLine(2);
            List<string> listContent = new List<string>()
            {
                "    Choisissez une option parmi les options suivantes:",
                "<<<<    N: Pour commencer une nouvelle partie    >>>>" ,
                "<<<<   C: Pour reperendre une partie sauvgarder  >>>>",
                  "<<<     S: Pour afficher les statistiques      >>>>",
                "<<<<               Q: Pour quitter               >>>>"
            };
            ConsoleWriter.SetFrame(listContent, 70, 50);
            ConsoleWriter.SetEmptyLine(4);
            GetMode();
        }
        private   void GetMode()
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
                    ChargePartie();
                    break;
                case "s":
                case "S":
                  AfficherStatistique();
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

      

        private void NouvellePartie()
        {
         
            _isMonTour = true;
            _isGameOver = false;
            _isPartieGagner = false;
            Console.SetCursorPosition(_headerCursorPosition.posLeft, _headerCursorPosition.posTop);
            ReintialiseCursorPos();
            Prologue();
            ReintialiseCursorPos();
            ChoisirPersonnage();
            ReinsialiserDonnee();
            SauvegarderParametrage();
            AjouterNouvellePartie();
            InitializeGrille();
          
        }

        private void AjouterNouvellePartie()
        {
            partie = new Partie();
            _partieService.Insert(partie);
            InitializerNouveauTour(ActionTour.Deplacement);
            _partieService.Update(partie);
            tour.ListPositionEnCours = GetInitialPosition();
            _tourService.Update(tour);

           
        }

        private void ChargePartie()
        {
            partie = _partieService.FindAll().FirstOrDefault();
            if(partie != null)
            {
                //_isMonTour = partie.DernierTour.isMonTour;
                _isGameOver = false;
                _isPartieGagner = false;
                tour = partie.ListTours.Where(t=>t.ListPositionEnCours != null).MaxBy(t => t.NumeroDuTour).First();
                _nbr = tour.NumeroDuTour;
                Console.SetCursorPosition(_headerCursorPosition.posLeft, _headerCursorPosition.posTop);
                ReintialiseCursorPos();
               
                GetPosition();
                currentJoueur = _listPosition.FirstOrDefault(j => j.Joueur.TypeJoueur == TypeJoueur.Joueur)?.Joueur;
               InitializeGrille();
            }
            
            // GetLastSauvegard();
        }

      
        private IEnumerable<Position> GetInitialPosition()
        {
            //revoir
            _listPosition = _positionService.GetInitialPosition(_pNJService.FindAll().ToList());
            tour.ListPositionEnCours = _listPosition;
            var lst = _listPosition.ToList();
            lst.Add(new Position(2, 9, currentJoueur));
            _listPosition = lst;
            return _listPosition;
        }
        private IEnumerable<Position> GetPositionsActuelle()
        {
            List<Position> positions = new List<Position>();
            _listPosition.ForEach(pos =>
            {
                positions.Add(
                 new Position()
                 {
                   
                     X = pos.X,
                     Y = pos.Y,
                     LeftCursorPosition = pos.LeftCursorPosition,
                     TopCursorPosition = pos.TopCursorPosition
                 }
                );
            });
            positions.ForEach(pos =>
            {
                _positionService.Insert(pos);
                pos.Joueur = _listPosition.FirstOrDefault(p => p.LeftCursorPosition == pos.LeftCursorPosition && p.TopCursorPosition == pos.TopCursorPosition)?.Joueur;
                _positionService.Update(pos);
            });
           
            return positions; 
        }
        private void GetLastSauvegard()
        {
           
        }

       

        private void InitializeGrille()
        {
           
            
            ReintialiseCursorPos();
            SetInstruction();
            _barreEtatCrsorPosition = new { posLeft = Console.CursorLeft, posTop = Console.CursorTop };
            SetJoueurInfo(currentJoueur);
            grille = ConsoleWriter.SetGrille(18, 10, _listPosition);
            _barreTourPosition = new { posLeft = Console.CursorLeft, posTop = Console.CursorTop };
            if(tour != null )
                SetTourInfo();
            Console.Write("Cliquez sur entrée pour commencer: ");
            Console.ReadLine();
            RunTour();
        }

        private void GetPosition()
        {
            _listPosition =tour.ListPositionEnCours;
       
        }

        private void GetParametrageInitial()
        {
            _lstParametrages = _parametrageService.FindAll().ToList();
            if(_lstParametrages.Count>0)
            {
              var param =   _lstParametrages.FirstOrDefault(p => p.NomParametre.Equals("PersonnageChoisie"));
                if(param != null)
                    InitializeJoueur(_personnageService.FindById(int.Parse(param.Valeur)));
                else
                    ChoisirPersonnage();
              
            }

        }

        private   void Prologue()
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
        private   Cote GetCote()
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
        private  void ChoisirPersonnage()
        {
            ConsoleWriter.SetEmptyLine(4);
            List<string> listContent = new List<string>()
            {
                "    Choisissez une option parmi les options suivantes:",
                "<<<<   L: Pour jouer du côté Lumineux   >>>>" ,
                "<<<<    O: Pour jouer du côté Obscur    >>>>"
            };
            ConsoleWriter.SetFrame(listContent, 90, 50);
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
            _lstParametrages.Add(new Parametrage("PersonnageChoisie", personnage.Id.ToString()));
            InitializeJoueur(personnage);


        }
       public void InitializeJoueur(PersonnageJoueur personnage)
        {
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

        private   int GetPersonnageChoisi()
        {
            int res = 0;
            Console.Write("Tapez votre choix, puis appuyez sur entrée  ");
            string choix = Console.ReadLine();
            if (int.TryParse(choix, out res))
                return int.Parse(choix);
            else
                return GetPersonnageChoisi();
        }

        private   void SetInstruction()
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
        private   void SetJoueurInfo(Joueur joueur, ConsoleColor color = ConsoleColor.Green)
        {
            List<string> listContent = new List<string>()
            {
               joueur.ToString()
            };
            ConsoleWriter.SetFrame(listContent, 90, 30, color: color);
        }


        private   void RunTour()
        {
            var currentPositionJoueur = _listPosition.Where(p => p.Joueur.TypeJoueur == TypeJoueur.Joueur).FirstOrDefault();
            while ( !_isGameOver && !_isPartieGagner)
            {
                bool isMoved = true;
                Joueur joueurMort = RunAttack(currentPositionJoueur);
                if (joueurMort != null && joueurMort.TypeJoueur == TypeJoueur.Joueur)
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
                    tour.ListPositionEnCours = GetPositionsActuelle();
                    _tourService.Update(tour);
                    partie.DateDernierSauvgarde = DateTime.Now;
                    _partieService.Update(partie);
                }
               // SauvegardeAutomatique(partie);
            }
           
            if (_isGameOver)
            {
                partie.Resultat = Resultat.Perdu;
                _partieService.Update(partie);
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
            else if (_isPartieGagner)
            {
                partie.Resultat = Resultat.Gagne;
                _partieService.Update(partie);
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
            else
                TourAdversaire(currentPositionJoueur);
        }

       

        private void InitializerNouveauTour(ActionTour mode)
        {
            tour = new Tour();
            _nbr++;
            tour.NumeroDuTour= _nbr;
            tour.ActionTour = mode;
            tour.isMonTour = _isMonTour;
            _tourService.Insert(tour);
            partie.ListTours.Add(tour);
            
           
        }

        private   bool CanMove(TypeDeplacement typeDeplacement, Position currentPositionJoueur)
        {

            return _deplacementService.CheckMoveValidity(typeDeplacement, currentPositionJoueur, grille)
                && _deplacementService.CheckIfCaseIsEmpty(typeDeplacement, currentPositionJoueur, _listPosition);
        }
        private   void TourAdversaire(Position currentPositionJoueur)
        {
            if (_listPosition.Where(p => p.Joueur.TypeJoueur == TypeJoueur.Adversaire && p.Joueur.Etat != Etat.Mort).Any())
            {
                List<Dictionary<TypeDeplacement, Position>> listDeplacementAdv = _deplacementService.DeplacerTousEnnemie(currentPositionJoueur, _listPosition, grille);
                InitializerNouveauTour(ActionTour.Deplacement);
                tour.JoueurEnAttaque = _listPosition.FirstOrDefault(p => p.Joueur.TypeJoueur == TypeJoueur.Adversaire && p.Joueur.Etat != Etat.Mort).Joueur;
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
                            currentPosition.LeftCursorPosition = Console.CursorLeft - 3;
                            currentPosition.TopCursorPosition = Console.CursorTop;

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
        private   Joueur RunAttack(Position currentJoueurPosition)
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


                    } while (!isJoueurDead && !isAdversaireDead && !Console.KeyAvailable);
                    adversairAattaquerPos.Joueur.OnAttack = false;
                }
                if (isJoueurDead)
                    return currentJoueur;
                else if (isAdversaireDead)
                    return adversairAattaquerPos.Joueur;
                else return null;
            }
            else
                return null;
        }

        private   void ActualiserBarEtat(Joueur joueur)
        {
            ConsoleWriter.SetConsoleCursorPosition(_barreEtatCrsorPosition.posLeft, _barreEtatCrsorPosition.posTop);
            SetJoueurInfo(joueur);
        }
        private   void SetTourInfo()
        {
            ConsoleWriter.SetConsoleCursorPosition(_barreTourPosition.posLeft, _barreTourPosition.posTop);
            List<string> listContent = new List<string>()
            {
               tour.ToString()
            };
            ConsoleWriter.SetFrame(listContent, 90, 30, ConsoleColor.Green);
        }

        private void ReinsialiserDonnee()
        {
            _parametrageService.DeleteAll();
      
        }

        #region Sauvgard
        private void SauvegarderParametrage()
        {
            _lstParametrages.ForEach(parametre => _parametrageService.Insert(parametre));
        }

        #endregion


        private void AfficherStatistique()
        {


            ReintialiseCursorPos();
         
            ConsoleWriter.SetEmptyLine(4);

            var parties = _partieService.FindAll();
            int nbrPnjTueDernPartie = GetNbrAdvTueByPartie(_partieService.FindAll().MaxBy(p => p.DateCreation).FirstOrDefault());
            int nbrFoisMort = _partieService.FindAll().Where(p => p.Resultat == Resultat.Perdu).Count();
            int nbrPnjTuPartie = 0;
            parties.ForEach(p => nbrPnjTuPartie += GetNbrAdvTueByPartie(p));
            List<string> listContent = new List<string>()
            {

                $"<<<<   Nombre de PNJs tués dans la dernière partie: {nbrPnjTueDernPartie} >>>>" ,
                $"<<<<                 Nombre de PNJs tués: {nbrPnjTuPartie}                 >>>>" ,
                $"<<<<               Nombre de parties perdues: {nbrFoisMort}                >>>>" ,
            };
            ConsoleWriter.SetFrame(listContent, 90, 50);
            ConsoleWriter.SetEmptyLine(15);
            Console.Read();
        }
       
        public int GetNbrAdvTueByPartie(Partie partie)
        {
               return partie.ListTours.Where(t => t.ListPositionEnCours != null).MaxBy(t => t.NumeroDuTour).First()
                                     .ListPositionEnCours.Count(p => p.Joueur.Etat == Etat.Mort);
        }

    }

}