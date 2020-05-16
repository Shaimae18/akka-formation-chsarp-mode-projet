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
        static string heroChar = "☺";
        static string ennemiChar = "☻";
        static string specialHeroChar = "[☺]";



        public static void SetFrame(List<string> listContent, int width)
        {
            string topFrame = string.Empty;
            string bottomFrame = string.Empty;
            StringBuilder frame = new StringBuilder(topLeftBorder);
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < width; i++)
                frame.Append(border);
            frame.Append(topRightBorder);
            topFrame = frame.ToString();
            int windowWidth = topFrame.Length-2;
            Console.Write(new string(' ', (Console.WindowWidth - topFrame.Length) / 2));
            Console.WriteLine(topFrame);

            foreach (string content in listContent)
            {
                Console.Write(new string(' ', (Console.WindowWidth - topFrame.Length) / 2));
                Console.WriteLine(String.Format("║{0," + ((windowWidth / 2) + (content.Length / 2)) + "}{1," + (windowWidth - (windowWidth / 2) - (content.Length / 2) + 1) + "}", content, "║"));
            }
            frame = new StringBuilder(bottomLeftBorder);
            for (int i = 0; i < width; i++)
                frame.Append(border);
            frame.Append(bottomRightBorder);
            bottomFrame = frame.ToString();
            Console.Write(new string(' ', (Console.WindowWidth - bottomFrame.Length) / 2));
            Console.WriteLine(bottomFrame);
            
        }

        public static void SetGrille(int width, int height, IEnumerable<Position> listInitialPositions)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.ForegroundColor = ConsoleColor.Green;
            string topFrame = string.Empty;
            string bottomFrame = string.Empty;
            StringBuilder frame;
          
            for (int row = 0; row < height; row++)
            { 
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
                Console.WriteLine(topFrame);
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
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write($" {heroChar} ");
                              
                                break;
                            case Entities.TypePersonnage.SpecialHero:
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write(specialHeroChar);
                                break;
                            case Entities.TypePersonnage.Ennemie:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write($" {ennemiChar} ");
                               
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
                Console.WriteLine(bottomFrame);
                Thread.Sleep(100);
            }
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
    }
}
