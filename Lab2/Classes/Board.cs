using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Classes
{
    public class Board
    {
        public string[,] PlayArea { get; set; }

        public bool InsertMove(int row, int column, string move)
        {
            if ((row < 0 || row > 2) && (column < 0 || column > 2))
            {
                return false;
            }
            else if (PlayArea[row, column] != null)
            {
                return false;
            }
            else
            {
                PlayArea[row, column] = move;
                return true;
            }
        }

        public bool RandomMove(string move)
        {
            Random random = new Random();
            return InsertMove(random.Next(3), random.Next(3), move);
        }
    }
}
