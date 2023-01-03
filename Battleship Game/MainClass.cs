using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Collections.Generic;


namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showShips = false;

            GameFlow myBoard = new GameFlow();
            GameFlow enemyBoard = new GameFlow();

            Dictionary<char, int> coordinates = PopulateDictionary();
            PrintHeader();
            for (int h = 0; h < 19; h++)
            {
                Write(" ");
            }

            PrintMap(myBoard.FirePositions, myBoard, enemyBoard, showShips);

            int Game;
            for (Game = 1; Game < 101; Game++)
            {
                myBoard.StepsTaken++;
                Position position = new Position();

                ForegroundColor = ConsoleColor.White;
                WriteLine("Enter firing position (e.g. A3).");
                string input = ReadLine();
                position = AnalyzeInput(input, coordinates);

                if (position.x == -1 || position.y == -1)
                {
                    WriteLine("Invalid coordinates!");
                    Game--;
                    continue;
                }

                if (myBoard.FirePositions.Any(EFP => EFP.x == position.x && EFP.y == position.y))
                {
                    WriteLine("This coordinate already being shot.");
                    Game--;
                    continue;
                }

                enemyBoard.fire();

                var index = myBoard.FirePositions.FindIndex(p => p.x == position.x && p.y == position.y);

                if (index == -1)
                    myBoard.FirePositions.Add(position);

                Clear();

                myBoard.AllShipsPosition.OrderBy(o => o.x).ThenBy(n => n.y).ToList();
                myBoard.CheckShipStatus(enemyBoard.FirePositions);

                enemyBoard.AllShipsPosition.OrderBy(o => o.x).ThenBy(n => n.y).ToList();
                enemyBoard.CheckShipStatus(myBoard.FirePositions);

                PrintHeader();
                for (int h = 0; h < 19; h++)
                {
                    Write(" ");
                }

                PrintMap(myBoard.FirePositions, myBoard, enemyBoard, showShips);

                Commentator(myBoard, true);
                Commentator(enemyBoard, false);
                if (enemyBoard.obliteratedAll || myBoard.obliteratedAll) { break; }
            }

            ForegroundColor = ConsoleColor.White;

            if (enemyBoard.obliteratedAll && !myBoard.obliteratedAll)
            {
                WriteLine("Game Ended, you win.");
            }

            else if (!enemyBoard.obliteratedAll && myBoard.obliteratedAll)
            {
                WriteLine("Game Ended, you lose.");
            }

            else
            {
                WriteLine("Game Ended, draw.");
            }

            WriteLine("Total steps taken:{0} ", Game);
            ReadLine();
        }

        static void PrintStatistic(int x, int y, GameFlow game)
        {
            if (x == 1 && y == 10)
            {
                ForegroundColor = ConsoleColor.White;
                Write("Indicator:    ");
            }

            if (x == 2 && y == 10)
            {
                if (game.carrierSunk)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("Carrier [5]   ");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("Carrier [5]   ");
                }
            }

            if (x == 3 && y == 10)
            {
                if (game.battleshipSunk)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("Battleship [4]");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("Battleship [4]");
                }
            }

            if (x == 4 && y == 10)
            {

                if (game.destroyerSunk)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("Destroyer [3] ");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("Destroyer [3] ");
                }
            }

            if (x == 5 && y == 10)
            {

                if (game.submarineSunk)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("Submarine [3] ");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("Submarine [3] ");
                }
            }

            if (x == 6 && y == 10)
            {

                if (game.patrolBoatSunk)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("PatrolBoat [2]");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("PatrolBoat [2]");
                }
            }

            if (x > 6 && y == 10)
            {
                for (int i = 0; i < 14; i++)
                {
                    Write(" ");
                }
            }
        }

        static void PrintMap(List<Position> positions, GameFlow myBoard, GameFlow enemyGame, bool showEnemyShips)
        {
            PrintHeader();
            WriteLine();
            if (!showEnemyShips)
                showEnemyShips = myBoard.obliteratedAll;

            List<Position> SortedLFirePositions = positions.OrderBy(o => o.x).ThenBy(n => n.y).ToList();
            List<Position> SortedShipsPositions = enemyGame.AllShipsPosition.OrderBy(o => o.x).ThenBy(n => n.y).ToList();

            SortedShipsPositions = SortedShipsPositions.Where(FP => !SortedLFirePositions.Exists(ShipPos => ShipPos.x == FP.x && ShipPos.y == FP.y)).ToList();

            int hitCounter = 0;
            int EnemyshipCounter = 0;
            int myShipCounter = 0;
            int enemyHitCounter = 0;

            char row = 'A';
            try
            {
                for (int x = 1; x < 11; x++)
                {
                    for (int y = 1; y < 11; y++)
                    {
                        bool keepGoing = true;

                        if (y == 1)
                        {
                            ForegroundColor = ConsoleColor.DarkYellow;
                            Write("[" + row + "]");
                            row++;
                        }

                        if (SortedLFirePositions.Count != 0 && SortedLFirePositions[hitCounter].x == x && SortedLFirePositions[hitCounter].y == y)
                        {

                            if (SortedLFirePositions.Count - 1 > hitCounter)
                                hitCounter++;

                            if (enemyGame.AllShipsPosition.Exists(ShipPos => ShipPos.x == x && ShipPos.y == y))
                            {

                                ForegroundColor = ConsoleColor.Red;
                                Write("[*]");

                                keepGoing = false;
                            }
                            else
                            {
                                ForegroundColor = ConsoleColor.Black;
                                Write("[X]");

                                keepGoing = false;
                            }
                        }

                        if (keepGoing && showEnemyShips && SortedShipsPositions.Count != 0 && SortedShipsPositions[EnemyshipCounter].x == x && SortedShipsPositions[EnemyshipCounter].y == y)
                        {
                            if (SortedShipsPositions.Count - 1 > EnemyshipCounter)
                                EnemyshipCounter++;

                            ForegroundColor = ConsoleColor.DarkGreen;
                            Write("[O]");
                            keepGoing = false;
                        }

                        if (keepGoing)
                        {
                            ForegroundColor = ConsoleColor.Blue;
                            Write("[~]");
                        }

                        PrintStatistic(x, y, myBoard);

                        if (y == 10)
                        {
                            Write("      ");

                            PrintMapOfEnemy(x, row, myBoard, enemyGame, ref myShipCounter, ref enemyHitCounter);
                        }
                    }
                    WriteLine();
                }
            }
            catch (Exception e)
            {
                string error = e.Message.ToString();
            }
        }

        static void PrintMapOfEnemy(int x, char row, GameFlow myBoard, GameFlow enemyBoard, ref int MyshipCounter, ref int EnemyHitCounter)
        {
            List<Position> EnemyFirePositions = new List<Position>();
            row--;
            Random random = new Random();
            List<Position> SortedFirePositions = enemyBoard.FirePositions.OrderBy(o => o.x).ThenBy(n => n.y).ToList();
            List<Position> SortedShipsPositions = myBoard.AllShipsPosition.OrderBy(o => o.x).ThenBy(n => n.y).ToList();

            SortedShipsPositions = SortedShipsPositions.Where(FP => !SortedFirePositions.Exists(ShipPos => ShipPos.x == FP.x && ShipPos.y == FP.y)).ToList();

            try
            {
                for (int y = 1; y < 11; y++)
                {
                    bool keepGoing = true;

                    if (y == 1)
                    {
                        ForegroundColor = ConsoleColor.DarkYellow;
                        Write("[" + row + "]");
                        row++;
                    }

                    if (SortedFirePositions.Count != 0 && SortedFirePositions[EnemyHitCounter].x == x && SortedFirePositions[EnemyHitCounter].y == y)
                    {

                        if (SortedFirePositions.Count - 1 > EnemyHitCounter)
                            EnemyHitCounter++;

                        if (myBoard.AllShipsPosition.Exists(ShipPos => ShipPos.x == x && ShipPos.y == y))
                        {

                            ForegroundColor = ConsoleColor.Red;
                            Write("[*]");

                            keepGoing = false;
                        }
                        else
                        {
                            ForegroundColor = ConsoleColor.Black;
                            Write("[X]");

                            keepGoing = false;
                        }
                    }

                    if (keepGoing && SortedShipsPositions.Count != 0 && SortedShipsPositions[MyshipCounter].x == x && SortedShipsPositions[MyshipCounter].y == y)
                    {
                        if (SortedShipsPositions.Count - 1 > MyshipCounter)
                            MyshipCounter++;

                        ForegroundColor = ConsoleColor.DarkGreen;
                        Write("[O]");

                        keepGoing = false;
                    }

                    if (keepGoing)
                    {
                        ForegroundColor = ConsoleColor.Blue;
                        Write("[~]");
                    }
                    PrintStatistic(x, y, enemyBoard);
                }
            }
            catch (Exception e)
            {
                string error = e.Message.ToString();
            }
        }

        static Position AnalyzeInput(string input, Dictionary<char, int> coordinates)
        {
            Position pos = new Position();

            char[] inputSplit = input.ToUpper().ToCharArray();

            if (inputSplit.Length < 2 || inputSplit.Length > 4)
            {
                return pos;
            }

            if (coordinates.TryGetValue(inputSplit[0], out int value))
            {
                pos.x = value;
            }

            else
            {
                return pos;
            }

            if (inputSplit.Length == 3)
            {
                if (inputSplit[1] == '1' && inputSplit[2] == '0')
                {
                    pos.y = 10;
                    return pos;
                }
                else
                {
                    return pos;
                }
            }

            if (inputSplit[1] - '0' > 9)
            {
                return pos;
            }
            else
            {
                pos.y = inputSplit[1] - '0';
            }
            return pos;
        }

        static void PrintHeader()
        {
            ForegroundColor = ConsoleColor.DarkYellow;
            Write("[ ]");
            for (int i = 1; i < 11; i++)
                Write("[" + i + "]");
        }

        static Dictionary<char, int> PopulateDictionary()
        {
            Dictionary<char, int> Coordinate =
                     new Dictionary<char, int>
                     {
                         { 'A', 1 },
                         { 'B', 2 },
                         { 'C', 3 },
                         { 'D', 4 },
                         { 'E', 5 },
                         { 'F', 6 },
                         { 'G', 7 },
                         { 'H', 8 },
                         { 'I', 9 },
                         { 'J', 10 }
                     };
            return Coordinate;
        }

        static void Commentator(GameFlow game, bool isMyShip)
        {
            string title = isMyShip ? "Your" : "Enemy";

            if (game.checkBattleship && game.battleshipSunk)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sink", title, nameof(game.Battleship));
                game.checkBattleship = false;
            }

            if (game.checkCarrier && game.carrierSunk)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sink", title, nameof(game.Carrier));
                game.checkCarrier = false;
            }

            if (game.checkDestroyer && game.destroyerSunk)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sink", title, nameof(game.Destroyer));
                game.checkDestroyer = false;
            }

            if (game.checkPatrolBoat && game.patrolBoatSunk)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sink", title, nameof(game.PatrolBoat));
                game.checkPatrolBoat = false;
            }

            if (game.checkSubmarine && game.submarineSunk)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sink", title, nameof(game.Submarine));
                game.checkSubmarine = false;
            }
        }
    }
}
