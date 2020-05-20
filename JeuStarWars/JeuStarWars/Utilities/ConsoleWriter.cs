using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace JeuStarWars.Utilities
{
    public static class ConsoleWriter
    {
        public static string border = "═";
        public static string topLeftBorder = "╔";
        public static string topRightBorder = "╗";
        public static string bottomLeftBorder = "╚";
        public static string bottomRightBorder = "╝";
        public static string middelBorder = "║";
        public static string heroChar = "☻";
        public static string ennemiChar = "☻";
        public static string specialHeroChar = "☺";
        public static string attaqueChar = "▓";
        public static string adversaireMort = "Ǿ";
        public static string lanceAttaqueChar = "▒";



        public static void SetFrame(List<string> listContent, int width, int threadSleepValue = 0, ConsoleColor color= ConsoleColor.Green)
        {
            string topFrame = string.Empty;
            string bottomFrame = string.Empty;
            StringBuilder frame = new StringBuilder(topLeftBorder);
            Console.ForegroundColor = color;
            ClearCurrentConsoleLine();
            for (int i = 0; i < width; i++)
                frame.Append(border);
            frame.Append(topRightBorder);
            topFrame = frame.ToString();
            int windowWidth = topFrame.Length - 2;
            Console.Write(new string(' ', ((Console.WindowWidth - topFrame.Length) < 0 ? 0 : (Console.WindowWidth - topFrame.Length)) / 2));
            Console.WriteLine(topFrame);

            foreach (string content in listContent)
            {
                ClearCurrentConsoleLine();
                Console.Write(new string(' ', ((Console.WindowWidth - topFrame.Length) < 0 ? 0 : (Console.WindowWidth - topFrame.Length)) / 2));
                Console.WriteLine(String.Format("║{0," + ((windowWidth / 2) + (content.Length / 2)) + "}{1," + (windowWidth - (windowWidth / 2) - (content.Length / 2) + 1) + "}", content, "║"));
                Thread.Sleep(threadSleepValue);
            }

            frame = new StringBuilder(bottomLeftBorder);
            for (int i = 0; i < width; i++)
                frame.Append(border);
            frame.Append(bottomRightBorder);
            bottomFrame = frame.ToString();
            ClearCurrentConsoleLine();
            Console.Write(new string(' ', ((Console.WindowWidth - bottomFrame.Length) < 0 ? 0 : Console.WindowWidth - bottomFrame.Length) / 2));
            Console.WriteLine(bottomFrame);

        }
        public static void SetConsoleCursorPosition(int posLeft, int posTop)
        {
            Console.SetCursorPosition(posLeft, posTop);
        }
        public static Grille SetGrille(int width, int height, IEnumerable<Position> listInitialPositions)
        {

            Grille grille = new Grille();
            bool isFirstIteration = true;
            string topFrame = string.Empty;
            string bottomFrame = string.Empty;
            StringBuilder frame;
            grille.TopBorder = Console.CursorTop;
            grille.LeftBorder = Console.CursorLeft;
            for (int row = 0; row < height; row++)
            {
                ClearCurrentConsoleLine();
                frame = new StringBuilder();
                for (int i = 0; i < width; i++)
                {
                    frame.Append(topLeftBorder);
                    frame.Append(border);
                    frame.Append(border);
                    frame.Append(border);
                    frame.Append(topRightBorder);
                }

                topFrame = frame.ToString();
                Console.Write(new string(' ', (Console.WindowWidth - topFrame.Length) / 2));
                if (isFirstIteration)
                    grille.LeftBorder = Console.CursorLeft;
                Console.Write(topFrame);
                if (isFirstIteration)
                    grille.RightBorder = Console.CursorLeft;
                Console.WriteLine();
                Console.Write(new string(' ', (Console.WindowWidth - topFrame.Length) / 2));
                int col = 0;
                while (col < width)
                {
                    if (listInitialPositions.Where(p => p.X == col && p.Y == row).Any())
                    {
                        var pos = listInitialPositions.Where(p => p.X == col && p.Y == row).FirstOrDefault();
                        Console.Write(middelBorder);
                        pos.TopCursorPosition = Console.CursorTop;
                        pos.LeftCursorPosition = Console.CursorLeft;
                        switch (pos.Joueur.TypeJoueur)
                        {
                            case TypeJoueur.Joueur:
                                if (pos.Joueur.PersonnageJoueur.TypePersonnage == TypePersonnage.NonLanceurDeSort)
                                    SetJoueurInCase(heroChar, ConsoleColor.Blue);
                                else
                                    SetJoueurInCase(specialHeroChar, ConsoleColor.DarkYellow);
                                break;
                            case TypeJoueur.Adversaire:
                                SetJoueurInCase(ennemiChar, ConsoleColor.Red);
                                break;
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(middelBorder);
                    }
                    else
                        Console.Write($"{middelBorder}   {middelBorder}");

                    col++;
                }
                Console.WriteLine();
                Console.Write(new string(' ', (Console.WindowWidth - topFrame.Length) / 2));
                grille.BottomBorder = Console.CursorTop;
                frame = new StringBuilder();
                for (int j = 0; j < width; j++)
                {
                    frame.Append(bottomLeftBorder);
                    frame.Append(border);
                    frame.Append(border);
                    frame.Append(border);
                    frame.Append(bottomRightBorder);
                }
                bottomFrame = frame.ToString();
                Console.Write(bottomFrame);
                grille.RightBorder = Console.CursorLeft;
                Console.WriteLine();
                Thread.Sleep(100);
                isFirstIteration = false;
            }

            return grille;

        }
        private static void DrawJoueur(TypeJoueur typeJoueur, TypePersonnage typePersonnage)
        {
            switch (typeJoueur)
            {
                case TypeJoueur.Joueur:
                    if (typePersonnage == TypePersonnage.NonLanceurDeSort)
                        SetJoueurInCase(heroChar, ConsoleColor.Blue);
                    else
                        SetJoueurInCase(specialHeroChar, ConsoleColor.DarkYellow);
                    break;

                case TypeJoueur.Adversaire:
                    SetJoueurInCase(ennemiChar, ConsoleColor.Red);
                    break;


            }
        }
        private static void SetJoueurInCase(string personnageChar, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write($" {personnageChar} ");
        }
        private static void SetJoueurInCaseByPosition(string chaine, int leftPos, int topPos, ConsoleColor color, int effetRalenti = 0)
        {
            Console.SetCursorPosition(leftPos, topPos);
            Console.ForegroundColor = color;
            Console.Write(chaine);
            Thread.Sleep(effetRalenti);
        }
        public static void SetEmptyLine(int nbrEmptyLine)
        {
            int a = 0;
            while (a < nbrEmptyLine + 1)
            {
                ClearCurrentConsoleLine();
                Console.WriteLine(" ");
                a++;
            }
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");
            Console.SetCursorPosition(0, currentLineCursor);
        }


        #region Deplacement
        public static void Up(Position positionJoueur, TypeJoueur typeJoueur = TypeJoueur.Joueur)
        {
            Console.SetCursorPosition(positionJoueur.LeftCursorPosition + 1, positionJoueur.TopCursorPosition);
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop - 3);
            if (typeJoueur != TypeJoueur.Adversaire)
                DrawJoueur(typeJoueur, positionJoueur.Joueur.PersonnageJoueur.TypePersonnage);
            else
                DrawJoueur(typeJoueur, TypePersonnage.NonLanceurDeSort);

        }
        public static void Down(Position positionJoueur, TypeJoueur typeJoueur = TypeJoueur.Joueur)
        {
            Console.SetCursorPosition(positionJoueur.LeftCursorPosition + 1, positionJoueur.TopCursorPosition);
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop + 3);
            if (typeJoueur != TypeJoueur.Adversaire)
                DrawJoueur(typeJoueur, positionJoueur.Joueur.PersonnageJoueur.TypePersonnage);
            else
                DrawJoueur(typeJoueur, TypePersonnage.NonLanceurDeSort);
        }
        public static void Left(Position positionJoueur, TypeJoueur typeJoueur = TypeJoueur.Joueur)
        {
            Console.SetCursorPosition(positionJoueur.LeftCursorPosition + 1, positionJoueur.TopCursorPosition);
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 2 - 5, Console.CursorTop);
            if (typeJoueur != TypeJoueur.Adversaire)
                DrawJoueur(typeJoueur, positionJoueur.Joueur.PersonnageJoueur.TypePersonnage);
            else
                DrawJoueur(typeJoueur, TypePersonnage.NonLanceurDeSort);
        }
        public static void Right(Position positionJoueur, TypeJoueur typeJoueur = TypeJoueur.Joueur)
        {
            Console.SetCursorPosition(positionJoueur.LeftCursorPosition + 1, positionJoueur.TopCursorPosition);
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 2 + 5, Console.CursorTop);
            if (typeJoueur != TypeJoueur.Adversaire)
                DrawJoueur(typeJoueur, positionJoueur.Joueur.PersonnageJoueur.TypePersonnage);
            else
                DrawJoueur(typeJoueur, TypePersonnage.NonLanceurDeSort);

        }



        #endregion
        #region Attaque


        public static void Attaquer(Position currentJourPosition, Position adversairAattaquerPos, bool isDead = true)
        {
            ConsoleColor colorJoueur = currentJourPosition.Joueur.TypeJoueur == TypeJoueur.Adversaire ? ConsoleColor.Red : (currentJourPosition.Joueur.PersonnageJoueur.TypePersonnage == TypePersonnage.LanceurDeSort ? ConsoleColor.DarkYellow : ConsoleColor.Blue);
            ConsoleColor colorAdversaire = adversairAattaquerPos.Joueur.TypeJoueur == TypeJoueur.Adversaire ? ConsoleColor.Red : (adversairAattaquerPos.Joueur.PersonnageJoueur.TypePersonnage == TypePersonnage.LanceurDeSort ? ConsoleColor.DarkYellow : ConsoleColor.Blue);
            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(currentJourPosition.LeftCursorPosition, currentJourPosition.TopCursorPosition);
                SetJoueurInCaseByPosition(lanceAttaqueChar, currentJourPosition.LeftCursorPosition, currentJourPosition.TopCursorPosition, colorJoueur, 0);
                Console.SetCursorPosition(currentJourPosition.LeftCursorPosition + 2, currentJourPosition.TopCursorPosition);
                SetJoueurInCaseByPosition(lanceAttaqueChar, currentJourPosition.LeftCursorPosition + 2, currentJourPosition.TopCursorPosition, colorJoueur, 200);
                Console.SetCursorPosition(currentJourPosition.LeftCursorPosition + 2, currentJourPosition.TopCursorPosition);

                Console.SetCursorPosition(currentJourPosition.LeftCursorPosition, currentJourPosition.TopCursorPosition);
                SetJoueurInCaseByPosition(" ", currentJourPosition.LeftCursorPosition, currentJourPosition.TopCursorPosition, colorJoueur, 0);
                Console.SetCursorPosition(currentJourPosition.LeftCursorPosition + 2, currentJourPosition.TopCursorPosition);
                SetJoueurInCaseByPosition(" ", currentJourPosition.LeftCursorPosition + 2, currentJourPosition.TopCursorPosition, colorJoueur, 200);

            }
            Console.SetCursorPosition(currentJourPosition.LeftCursorPosition + 1, currentJourPosition.TopCursorPosition);
             SetJoueurInCaseByPosition(specialHeroChar, currentJourPosition.LeftCursorPosition + 1, currentJourPosition.TopCursorPosition, colorJoueur, 100);
        



            for (int i = 0; i <10; i++)
                {
                   
                    Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition , adversairAattaquerPos.TopCursorPosition);
                    SetJoueurInCaseByPosition(attaqueChar, adversairAattaquerPos.LeftCursorPosition , adversairAattaquerPos.TopCursorPosition, colorJoueur, 50);

                    Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition, adversairAattaquerPos.TopCursorPosition);
                    SetJoueurInCaseByPosition(attaqueChar, adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition, colorJoueur, 50);

                    Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition, adversairAattaquerPos.TopCursorPosition);
                    SetJoueurInCaseByPosition(attaqueChar, adversairAattaquerPos.LeftCursorPosition + 2, adversairAattaquerPos.TopCursorPosition, colorJoueur, 50);

                   Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition);
                   SetJoueurInCaseByPosition(" ", adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition, colorJoueur, 50);
            }
                if(isDead)
            {
                
                Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition, adversairAattaquerPos.TopCursorPosition);
                SetJoueurInCaseByPosition(" ", adversairAattaquerPos.LeftCursorPosition, adversairAattaquerPos.TopCursorPosition, colorAdversaire, 0);
                Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition, adversairAattaquerPos.TopCursorPosition);
                SetJoueurInCaseByPosition(" ", adversairAattaquerPos.LeftCursorPosition + 2, adversairAattaquerPos.TopCursorPosition, colorAdversaire, 100);
                Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition, adversairAattaquerPos.TopCursorPosition);
                SetJoueurInCaseByPosition(adversaireMort, adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition, colorAdversaire, 100);
            }
               else
            {
                for (int i = 0; i <3; i++)
                {
                    Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition, adversairAattaquerPos.TopCursorPosition);
                    SetJoueurInCaseByPosition("[", adversairAattaquerPos.LeftCursorPosition, adversairAattaquerPos.TopCursorPosition, colorAdversaire, 0);
                    Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition, adversairAattaquerPos.TopCursorPosition);
                    SetJoueurInCaseByPosition("]", adversairAattaquerPos.LeftCursorPosition + 2, adversairAattaquerPos.TopCursorPosition, colorAdversaire, 100);
                    Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition, adversairAattaquerPos.TopCursorPosition);
                    SetJoueurInCaseByPosition(ennemiChar, adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition, colorAdversaire, 100);
                   
                }

            }



        }

        public static void AttaquerJoueur(Position pos, Position adversairAattaquerPos, bool isDead = false)
        {
            for (int i = 0; i <5; i++)
            {
                Console.SetCursorPosition(pos.LeftCursorPosition , pos.TopCursorPosition);
                SetJoueurInCaseByPosition(lanceAttaqueChar, pos.LeftCursorPosition , pos.TopCursorPosition, ConsoleColor.Red, 0);
                Console.SetCursorPosition(pos.LeftCursorPosition + 2, pos.TopCursorPosition);
                SetJoueurInCaseByPosition(lanceAttaqueChar, pos.LeftCursorPosition + 2, pos.TopCursorPosition, ConsoleColor.Red, 200);
                Console.SetCursorPosition(pos.LeftCursorPosition + 2, pos.TopCursorPosition);

                Console.SetCursorPosition(pos.LeftCursorPosition, pos.TopCursorPosition);
                SetJoueurInCaseByPosition(" ", pos.LeftCursorPosition , pos.TopCursorPosition, ConsoleColor.Red, 0);
                Console.SetCursorPosition(pos.LeftCursorPosition + 2, pos.TopCursorPosition);
                SetJoueurInCaseByPosition(" ", pos.LeftCursorPosition + 2, pos.TopCursorPosition, ConsoleColor.Red, 200);

            }
            Console.SetCursorPosition(pos.LeftCursorPosition + 1, pos.TopCursorPosition);
            SetJoueurInCaseByPosition(ennemiChar, pos.LeftCursorPosition + 1, pos.TopCursorPosition, ConsoleColor.Red, 100);
            if (isDead)
            {
                for (int i = 0; i < 6; i++)
                {
                    Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition);
                    SetJoueurInCaseByPosition(" ", adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition, ConsoleColor.Red, 100);
                    Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition);
                    SetJoueurInCaseByPosition(attaqueChar, adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition, ConsoleColor.Red, 100);

                }
                Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition);
                SetJoueurInCaseByPosition(adversaireMort, adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition, ConsoleColor.Red, 0);
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition);
                    SetJoueurInCaseByPosition(" ", adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition, ConsoleColor.Red, 100);
                    Console.SetCursorPosition(adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition);
                    SetJoueurInCaseByPosition(specialHeroChar, adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition, ConsoleColor.Red, 100);

                }
                if (adversairAattaquerPos.Joueur.PersonnageJoueur.TypePersonnage == TypePersonnage.LanceurDeSort)
                    SetJoueurInCaseByPosition(specialHeroChar, adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition, ConsoleColor.DarkYellow, 100);
                else
                    SetJoueurInCaseByPosition(specialHeroChar, adversairAattaquerPos.LeftCursorPosition + 1, adversairAattaquerPos.TopCursorPosition, ConsoleColor.Blue, 100);

            }
        }


        #endregion
    }
}
