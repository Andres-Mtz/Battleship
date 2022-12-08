using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_Game
{
    class Board
    {
        private int row;
        private int col;
        private Type[,] board;

        public Board()
        {
            row = 10;
            col = 10;
            board = new Type[row, col];
        }

        private void init()
        {
            for (int r = 0; r < row; row++)
            {
                for (int c = 0; c < col; col++)
                    board[r, c] = new Type();
            }
        }

        //private void setColor(Type type)
        private void setColor(Type type)
        {
            switch (type.type)
            {
                case 'n':
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;

                case 'b':
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
            }
        }

        public void print()
        {
            Console.Clear();
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    setColor(board[r, c]);
                    Console.Write(" ");
                    Console.Write(board[r, c].type);
                    Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }

        public void place(int r, int c, char or, int length)
        {

            switch (or)
            {
                case 'h':
                    for (int i = c; i < length + c; i++)
                    {
                        if (board[r, i].type == 'n')
                        {
                            board[r, i].type = 'b';
                        }
                    }
                    print();
                    break;
                case 'v':
                    for (int i = r; i < length + r; i++)
                    {
                        if (board[i, c].type == 'n')
                        {
                            board[i, c].type = 'b';
                        }
                    }
                    print();
                    break;
            }
        }

        public bool checkBoard()
        {
            int count = 0;
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    if (board[r, c].type == 'd')
                    {
                        count++;
                    }
                }
            }

            if (count == 30) return false;
            else return true;
        }
    }

    class Type
    {
        public char type { get; set; } = 'n';
    }
}
