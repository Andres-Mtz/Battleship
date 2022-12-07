using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_Game
{
    class GameFlow
    {
        Random random = new Random();
       
        // Ship sizes
        private const int carrier = 5;
        private const int battleship = 4;
        private const int destroyer = 3;
        private const int submarine = 2;
        private const int patrolBoat= 1;

        public List<Position> carrierList { get; set; }
        public List<Position> battleshipList { get; set; }
        public List<Position> destroyerList { get; set; }
        public List<Position> submarineList { get; set; }
        public List<Position> patrolBoatList { get; set; }
        public List<Position> allPositions { get; set; } = new List<Position>();
        public List<Position> firePositions { get; set; } = new List<Position>();

        public bool carrierIsSunk { get; set; } = false;
        public bool battleshipIsSunk { get; set; } = false;
        public bool destroyerIsSunk { get; set; } = false;
        public bool submarineIsSunk { get; set; } = false;
        public bool patrolBoatIsSunk { get; set; } = false;
        public bool allShipsSunk { get; set; } = false;

        // Methods
        public GameFlow CheckShipStatus(List<Position> hitPositions)
        {
            carrierIsSunk = carrierList.Where(C => !hitPositions.Any(H => C.x == H.x && C.y == H.y)).Count() == 0;
            battleshipIsSunk = battleshipList.Where(C => !hitPositions.Any(H => C.x == H.x && C.y == H.y)).Count() == 0;
            destroyerIsSunk = destroyerList.Where(C => !hitPositions.Any(H => C.x == H.x && C.y == H.y)).Count() == 0;
            submarineIsSunk = submarineList.Where(C => !hitPositions.Any(H => C.x == H.x && C.y == H.y)).Count() == 0;
            patrolBoatIsSunk = patrolBoatList.Where(C => !hitPositions.Any(H => C.x == H.x && C.y == H.y)).Count() == 0;
            allShipsSunk = carrierIsSunk && battleshipIsSunk && destroyerIsSunk && submarineIsSunk && patrolBoatIsSunk;
            return this;
        }

        public List<Position> generatePositions(int size, List<Position> AllPositions)
        {
            List<Position> positions = new List<Position>();
        }

        // Const
        public GameFlow()
        {
            carrierList = generatePositions(carrier, allPositions);

        }




        public void Assets()
        {

        }
    }
}
