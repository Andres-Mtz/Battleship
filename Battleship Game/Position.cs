using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_Game
{
    public class Position
    {
        public int x { get; set; } = -1;
        public int y { get; set; } = -1;

        public void Coordinates(int row, int column)
        {
            x = row;
            y = column;
        }
    }
}
