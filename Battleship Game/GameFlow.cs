using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class GameFlow
    {
        Random random = new Random();
        private const int CARRIER = 5;
        private const int BATTLESHIP = 4;
        private const int DESTROYER = 3;
        private const int SUBMARINE = 3;
        private const int PATROLBOAT = 2;

        public GameFlow()
        {

            Carrier = generatePosistion(CARRIER, AllShipsPosition);
            Battleship = generatePosistion(BATTLESHIP, AllShipsPosition);
            Destroyer = generatePosistion(DESTROYER, AllShipsPosition);
            Submarine = generatePosistion(SUBMARINE, AllShipsPosition);
            PatrolBoat = generatePosistion(PATROLBOAT, AllShipsPosition);
        }

        public int StepsTaken { get; set; } = 0;

        public List<Position> Carrier { get; set; }
        public List<Position> Battleship { get; set; }
        public List<Position> Destroyer { get; set; }
        public List<Position> Submarine { get; set; }
        public List<Position> PatrolBoat { get; set; }
        public List<Position> AllShipsPosition { get; set; } = new List<Position>();
        public List<Position> FirePositions { get; set; } = new List<Position>();


        public bool carrierSunk { get; set; } = false;
        public bool battleshipSunk { get; set; } = false;
        public bool destroyerSunk { get; set; } = false;
        public bool submarineSunk { get; set; } = false;
        public bool patrolBoatSunk { get; set; } = false;
        public bool obliteratedAll { get; set; } = false;


        public bool checkCarrier { get; set; } = true;
        public bool checkBattleship { get; set; } = true;
        public bool checkDestroyer { get; set; } = true;
        public bool checkSubmarine { get; set; } = true;
        public bool checkPatrolBoat { get; set; } = true;

        public GameFlow CheckShipStatus(List<Position> hitPositions)
        {

            carrierSunk = Carrier.Where(C => !hitPositions.Any(H => C.x == H.x && C.y == H.y)).ToList().Count == 0;
            battleshipSunk = Battleship.Where(B => !hitPositions.Any(H => B.x == H.x && B.y == H.y)).ToList().Count == 0;
            destroyerSunk = Destroyer.Where(D => !hitPositions.Any(H => D.x == H.x && D.y == H.y)).ToList().Count == 0;
            submarineSunk = Submarine.Where(S => !hitPositions.Any(H => S.x == H.x && S.y == H.y)).ToList().Count == 0;
            patrolBoatSunk = PatrolBoat.Where(P => !hitPositions.Any(H => P.x == H.x && P.y == H.y)).ToList().Count == 0;


            obliteratedAll = carrierSunk && battleshipSunk && destroyerSunk && submarineSunk && patrolBoatSunk;
            return this;
        }


        public List<Position> generatePosistion(int size, List<Position> allPosition)
        {
            List<Position> positions = new List<Position>();

            bool exists = false;

            do
            {
                positions = generatePositionRandomly(size);
                exists = positions.Where(AP => allPosition.Exists(ShipPos => ShipPos.x == AP.x && ShipPos.y == AP.y)).Any();
            }
            while (exists);

            allPosition.AddRange(positions);


            return positions;
        }

        public List<Position> generatePositionRandomly(int size)
        {
            List<Position> positions = new List<Position>();

            int direction = random.Next(1, size);
            int row = random.Next(1, 11);
            int col = random.Next(1, 11);

            if (direction % 2 != 0)
            {
                if (row - size > 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row - i;
                        pos.y = col;
                        positions.Add(pos);
                    }
                }
                else
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row + i;
                        pos.y = col;
                        positions.Add(pos);
                    }
                }
            }
            else
            {
                if (col - size > 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row;
                        pos.y = col - i;
                        positions.Add(pos);
                    }
                }
                else
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row;
                        pos.y = col + i;
                        positions.Add(pos);
                    }
                }
            }
            return positions;
        }

        public GameFlow fire()
        {
            Position enemyShotPos = new Position();
            bool alreadyShot = false;
            do
            {
                enemyShotPos.x = random.Next(1, 11);
                enemyShotPos.y = random.Next(1, 11);
                alreadyShot = FirePositions.Any(EFP => EFP.x == enemyShotPos.x && EFP.y == enemyShotPos.y);
            }
            while (alreadyShot);

            FirePositions.Add(enemyShotPos);
            return this;
        }
    }
}
