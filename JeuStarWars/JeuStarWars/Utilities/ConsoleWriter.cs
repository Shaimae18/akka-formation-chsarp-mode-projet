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
        static string border = "═";
        static string topLeftBorder = "╔";
        static string topRightBorder = "╗";
        static string bottomLeftBorder = "╚";
        static string bottomRightBorder = "╝";
        static string middelBorder = "║";
        static string heroChar = "☻";
        static string ennemiChar = "☻";
        static string specialHeroChar = "[☺]";
        static string attaqueChar = "▓";




        public static void SetFrame(List<string> listContent, int width, int threadSleepValue=0)
        {
            string topFrame = string.Empty;
            string bottomFrame = string.Empty;
            StringBuilder frame = new StringBuilder(topLeftBorder);
            Console.ForegroundColor = ConsoleColor.Green;
            ClearCurrentConsoleLine();
            for (int i = 0; i < width; i++)
                frame.Append(border);
            frame.Append(topRightBorder);
            topFrame = frame.ToString();
            int windowWidth = topFrame.Length-2;
            Console.Write(new string(' ', ((Console.WindowWidth - topFrame.Length) <0 ? 0 : (Console.WindowWidth - topFrame.Length)) / 2)) ;
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
            Console.Write(new string(' ', ((Console.WindowWidth - bottomFrame.Length)<0 ? 0 : Console.WindowWidth - bottomFrame.Length) / 2));
            Console.WriteLine(bottomFrame);
            
        }

        internal static void SetConsoleCursorPosition(int posLeft, int posTop)
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
                    if(listInitialPositions.Where(p => p.X ==col && p.Y == row).Any()) 
                    {
                        var pos = listInitialPositions.Where(p => p.X == col && p.Y == row).FirstOrDefault();
                        Console.Write(middelBorder);
                        pos.TopCursorPosition = Console.CursorTop;
                        pos.LeftCursorPosition = Console.CursorLeft;
                        switch (pos.Personnage.TypePersonnage)
                        {
                            case Entities.TypePersonnage.Hero:
                                SetCharInCase(heroChar, ConsoleColor.Blue);
                                break;
                            case Entities.TypePersonnage.SpecialHero:
                                SetCharInCase(specialHeroChar, ConsoleColor.DarkYellow);
                                break;
                            case Entities.TypePersonnage.Ennemie:
                                SetCharInCase(ennemiChar, ConsoleColor.Red);
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
                grille.RightBorder =Console.CursorLeft;
                Console.WriteLine();
                Thread.Sleep(100);
                isFirstIteration = false;
            }

            return grille;
           
        }

        

        private static void SetCharInCase(string personnageChar, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write($" {personnageChar} ");
        }
        private static void SetCharInCaseByPosition(string chaine, int leftPos,int topPos, ConsoleColor color, int effetRalenti =0)
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
        public static void Up(Position position, TypePersonnage typePersonnage = TypePersonnage.Hero)
        {
            Console.SetCursorPosition(position.LeftCursorPosition + 1, position.TopCursorPosition);
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop - 3);
            switch (typePersonnage)
            {
                case TypePersonnage.Hero:
                    SetCharInCase(heroChar, ConsoleColor.Blue);
                    break;
                case TypePersonnage.SpecialHero:
                    SetCharInCase(specialHeroChar, ConsoleColor.DarkYellow);
                    break;
                case TypePersonnage.Ennemie:
                    SetCharInCase(ennemiChar, ConsoleColor.Red);
                    break;


            }

        }
        public static void Down(Position position, TypePersonnage typePersonnage = TypePersonnage.Hero)
        {
            Console.SetCursorPosition(position.LeftCursorPosition + 1, position.TopCursorPosition);
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop + 3);
            switch (typePersonnage)
            {
                case TypePersonnage.Hero:
                    SetCharInCase(heroChar, ConsoleColor.Blue);
                    break;
                case TypePersonnage.SpecialHero:
                    SetCharInCase(specialHeroChar, ConsoleColor.DarkYellow);
                    break;
                case TypePersonnage.Ennemie:
                    SetCharInCase(ennemiChar, ConsoleColor.Red);
                    break;


            }
        }
        public static void Left(Position position, TypePersonnage typePersonnage = TypePersonnage.Hero)
        {
            Console.SetCursorPosition(position.LeftCursorPosition + 1, position.TopCursorPosition);
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 2 - 5, Console.CursorTop);
            switch (typePersonnage)
            {
                case TypePersonnage.Hero:
                    SetCharInCase(heroChar, ConsoleColor.Blue);
                    break;
                case TypePersonnage.SpecialHero:
                    SetCharInCase(specialHeroChar, ConsoleColor.DarkYellow);
                    break;
                case TypePersonnage.Ennemie:
                    SetCharInCase(ennemiChar, ConsoleColor.Red);
                    break;


            }
        }
        public static void Right(Position position, TypePersonnage typePersonnage = TypePersonnage.Hero)
        {
            Console.SetCursorPosition(position.LeftCursorPosition + 1, position.TopCursorPosition);
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 2 + 5, Console.CursorTop);
            switch (typePersonnage)
            {
                case TypePersonnage.Hero:
                    SetCharInCase(heroChar, ConsoleColor.Blue);
                    break;
                case TypePersonnage.SpecialHero:
                    SetCharInCase(specialHeroChar, ConsoleColor.DarkYellow);
                    break;
                case TypePersonnage.Ennemie:
                    SetCharInCase(ennemiChar, ConsoleColor.Red);
                    break;


            }
        }

        #endregion
        #region Attaque
        public static void LeftAttack(Personnage personnageJoueur,Position position, IEnumerable<Position> listPos)
        {
            Console.SetCursorPosition(position.LeftCursorPosition + 1, position.TopCursorPosition);
            AnimerAttaqueLeft(1);
            if(listPos.Any(p => p.LeftCursorPosition == position.LeftCursorPosition-5 && p.TopCursorPosition == position.TopCursorPosition ))
            {
                AjouterCadavre(1,0) ;

            }
            SetCharInCaseByPosition(middelBorder, position.LeftCursorPosition - 1, Console.CursorTop, ConsoleColor.Green);
            
        }
        public static void RigthAttack(Personnage personnageJoueur, Position position, IEnumerable<Position> listPos)
        {
            Console.SetCursorPosition(position.LeftCursorPosition+2 , position.TopCursorPosition);
            AnimerAttaqueRight(-1);
            
            if (listPos.Any(p => p.LeftCursorPosition == position.LeftCursorPosition + 5 && p.TopCursorPosition == position.TopCursorPosition))
            {
                Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                AjouterCadavre(1, 0);

            }
            SetCharInCaseByPosition(middelBorder, position.LeftCursorPosition + 3, Console.CursorTop, ConsoleColor.Green);
            SetCharInCaseByPosition(middelBorder, position.LeftCursorPosition + 4, Console.CursorTop, ConsoleColor.Green);

        }
        public static void UpAttack(Personnage personnageJoueur, Position position, IEnumerable<Position> listPos)
        {
            Console.SetCursorPosition(position.LeftCursorPosition + 2, position.TopCursorPosition - 1);
            AnimerAttaqueUp();

            if (listPos.Any(p => p.LeftCursorPosition == position.LeftCursorPosition + 5 && p.TopCursorPosition == position.TopCursorPosition))
            {
                Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop - 1);
                AjouterCadavre(1, 0);

            }
            SetCharInCaseByPosition(border, position.LeftCursorPosition + 1, Console.CursorTop + 1, ConsoleColor.Green);

        }
        public static void DownAttack(Personnage personnageJoueur, Position position, IEnumerable<Position> listPos)
        {
            Console.SetCursorPosition(position.LeftCursorPosition + 2, position.TopCursorPosition + 1);
            AnimerAttaqueDown();
            //
            if (listPos.Any(p => p.LeftCursorPosition == position.LeftCursorPosition + 5 && p.TopCursorPosition == position.TopCursorPosition))
            {
                Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop + 1);
                AjouterCadavre(1, 0);

            }
            SetCharInCaseByPosition(border, position.LeftCursorPosition + 1, Console.CursorTop - 1, ConsoleColor.Green);

        }
        private static void AnimerAttaqueLeft(int j)
        {
            for(int i =1;i<4;i++)
            {
                SetCharInCaseByPosition(attaqueChar, Console.CursorLeft - (i*j), Console.CursorTop, ConsoleColor.Red, 100);
                SetCharInCaseByPosition(" ", Console.CursorLeft - 1, Console.CursorTop, ConsoleColor.Red, 100);
            }
        }
        private static void AnimerAttaqueRight(int j)
        {
            for (int i = 1; i < 6; i++)
            {
                SetCharInCaseByPosition(attaqueChar, Console.CursorLeft , Console.CursorTop, ConsoleColor.Red, 100);
                SetCharInCaseByPosition(" ", Console.CursorLeft - 1, Console.CursorTop, ConsoleColor.Red, 100);
            }
        }
        private static void AnimerAttaqueUp()
        {
            for (int i = 1; i <2; i++)
            {
                SetCharInCaseByPosition(attaqueChar, Console.CursorLeft-1, Console.CursorTop-1, ConsoleColor.Red, 100);
                SetCharInCaseByPosition(" ", Console.CursorLeft -1, Console.CursorTop, ConsoleColor.Red, 100);
            }
        }
        private static void AnimerAttaqueDown()
        {
            for (int i = 1; i < 2; i++)
            {
                SetCharInCaseByPosition(attaqueChar, Console.CursorLeft - 1, Console.CursorTop + 1, ConsoleColor.Red, 100);
                SetCharInCaseByPosition(" ", Console.CursorLeft - 1, Console.CursorTop, ConsoleColor.Red, 100);
            }
        }

        private static void AjouterCadavre(int x, int y)
        {
            Console.SetCursorPosition(Console.CursorLeft -1, Console.CursorTop);
            for (int i = 0; i < 5; i++)
            {
                SetCharInCaseByPosition("X", Console.CursorLeft - x, Console.CursorTop - y, ConsoleColor.Red, 50);
                SetCharInCaseByPosition(" ", Console.CursorLeft - x, Console.CursorTop - y, ConsoleColor.Red, 50);
                SetCharInCaseByPosition("X", Console.CursorLeft - x, Console.CursorTop - y, ConsoleColor.Red, 50);
            }

        }

       
        
#endregion
    }
}
