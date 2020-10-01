using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PoliceAndThief
{
    public class Gameboard
    {
        private int xSize;
        private int ySize;
        private GameboardCell[,] grid;


        public Gameboard(int ys, int xs)
        {
            xSize = xs;
            ySize = ys;
            grid = new GameboardCell[ySize, xSize];
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    grid[y, x] = new GameboardCell();
                }
            }
        }

        public void CheckForCollitions()
        {
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    if (grid[y, x].People.Count > 1)
                    {
                        GameboardCell cell = GetCell(y, x);
                        foreach (Person person in cell.People)
                        {
                            // If CheckAction() returns true that means an action was executed and we should go out of this loop
                            if (person.CheckAction(cell)) break;
                        }

                    }
                }

            }

        }

        public GameboardCell GetCell(int y, int x)
        {
            if (x > xSize) throw new Exception("Gameboard.GetCell(): x out of range!");
            if (y > ySize) throw new Exception("Gameboard.GetCell(): y out of range!");
            return grid[y, x];
        }
        public void Clear()
        {
// Clear out each cell for a fresh update of the board
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    grid[y, x].People.Clear();
                }
            }

        }
        public void Draw()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    if (grid[y, x].People.Count == 0) sb.Append(' ');
                    if (grid[y, x].People.Count == 1)
                    {
                        sb.Append(grid[y, x].People.First().Display());
                        
                    }
                    if (grid[y, x].People.Count >1) sb.Append('X');
                }
                sb.Append('\n');
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(sb.ToString());
        }

    }
}
