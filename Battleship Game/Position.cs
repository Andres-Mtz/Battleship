using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Position
    {
        public int x { get; set; }
        public int y { get; set; }

        public void coordinates(int row, int column)
        {
            x = row;
            y = column;
        }
    }
}
