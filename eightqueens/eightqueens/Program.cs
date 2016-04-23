using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace eightqueens
{
    class EightQueens : IEquatable<EightQueens>
    {
        public int x, y;

        public EightQueens(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(EightQueens other)
        {
            if (other == null)
                return false;
            if (other.x == this.x && other.y == this.y)
                return true;
            return false;
        }
    }
    class Program
    {
        static List<EightQueens> FinalList = new List<EightQueens>();

        static bool Check8Queens(int newx, int newy)
        {
            bool isValid = true;
            FinalList.ForEach(e =>
            {
                int diffx = e.x > newx ? e.x - newx : newx - e.x;
                int diffy = e.y > newy ? e.y - newy : newy - e.y;

                if ((e.x == newx) || (e.y == newy) || (diffx == diffy))
                {
                    isValid = false;
                }
            });
            return isValid;
        }

        static void Print8Queens()
        {
            Console.WriteLine("EightQueen Coordinates are : ");
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine(" - - - - - - - - ");
                for (int j = 0; j < 9; j++)
                {
                    if (FinalList.Contains(new EightQueens(i, j)))
                    {
                        Console.Write("|*");
                    }
                    else
                    {
                        Console.Write("| ");
                    }
                }
                Console.WriteLine("");
            }
            Console.WriteLine(" - - - - - - - - ");
            Console.WriteLine("Press Enter to Continue");
            Console.ReadLine();
        }

        static bool CheckBoundaries(int x, int y)
        {
            bool isValid = true;
                FinalList.ForEach(e =>
                {
                    if (e.x == x || e.y == y || e.x - x == e.y - y)
                        isValid = false;
                });
            return isValid;
        }

        // Calculates all possible positions and stores them in the global array.
        static bool Solve8Queens(int startx, int starty)
        {
            // check if the new position has any clash. if so return false.
            if (Check8Queens(startx, starty) != true)
            {
                return false;
            }

            FinalList.Add(new EightQueens(startx, starty));

            if (FinalList.Count() == 8)
            {
                return true;
            }
            // Find and explore new possibilities in all eight directions.
            List<EightQueens> possibilitiesList = new List<EightQueens>();
            for (int i = 0; i < 8; i++)
            {
                if (i == startx)
                    continue;

                for (int j = 0; j < 8; j++)
                {
                    if (j == starty)
                        continue;

                    int diffx = startx > i ? startx - i : i - startx;
                    int diffy = starty > j ? starty - j : j - starty;

                    if (diffx == diffy)
                        continue;

                    possibilitiesList.Add(new EightQueens(i, j));
                }
            }
            possibilitiesList.ForEach(e =>
            {
                Solve8Queens(e.x, e.y);
            });

            if (FinalList.Count() != 8)
            {
                Console.WriteLine("Current EightQueen Coordinates are : ");
                FinalList.ForEach(e => Console.WriteLine("x:{0},y:{1}", e.x, e.y));
                FinalList.Remove(new EightQueens(startx, starty));
                return false;
            }
            else
            {
                return true;
            }
        }
        static void Main(string[] args)
        {
            int startx = 0;
            int starty = 0;

            FinalList.ForEach(e => 
            { 
                e.x = -1;
                e.y = -1;
            });

            Console.WriteLine("Enter starting coordinates:");
            char key = Console.ReadKey().KeyChar;
            startx = (int)char.GetNumericValue(key);
            key = Console.ReadKey().KeyChar;
            starty = (int)char.GetNumericValue(key);

            if (Solve8Queens(startx, starty))
            {
                Print8Queens();
            }
            else
            {
                Console.WriteLine("No solution exists");
            }
        }
    }
}
