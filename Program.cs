using System;
using System.Threading;

// The Rules

namespace hack_gol
{
    class Game
    {
        private int oLeft = 2;
        private int oTop = 2;

        private bool[,] world;

        public void Start()
        {
            Console.ResetColor();
            world = new bool[96, 32];

            Console.Clear();
            Console.WriteLine("Game, started!");

            int x = 96/2 - 5, y = 32/2 - 2;
            Add(x, y); Add(x+2, y); Add(x+4, y);
            Add(x, y+1);            Add(x+4, y+1);
            Add(x, y+2);            Add(x+4, y+2);
            Add(x, y+3);            Add(x+4, y+3);
            Add(x, y+4); Add(x+2, y+4); Add(x+4, y+4);
            
            Draw();

            for(int i=0; true; i++){
                Tick();
            }
        }

        public void Draw()
        {
            
            for(int i=0; i < 96; i++)
            {
                for(int j=0; j < 32; j++)
                {
                    // Console.BackgroundColor = world[i, j] ? ConsoleColor.Blue : ConsoleColor.Yellow;

                    Console.SetCursorPosition(oLeft+i, oTop+j);
                    Console.Write(world[i, j] ? " " : " ");

                    // Console.ResetColor();
                }
            }
        }

        public void Tick()
        {
            //Thread.Sleep(100);

            // For a space that is 'populated':
            // Each cell with one or no neighbors dies, as if by solitude.
            // Each cell with four or more neighbors dies, as if by overpopulation.
            // Each cell with two or three neighbors survives.
            // For a space that is 'empty' or 'unpopulated'
            // Each cell with three neighbors becomes populated.

           

            var worldCopy = (bool[,])world.Clone();
            for(int i=0; i < 96; i++)
            {
                for(int j=0; j < 32; j++)
                {   
                    if(worldCopy[i, j])
                    {
                        int c = CountNeighbors(i, j, false);
                        if(c <= 1 || c >= 4){
                            worldCopy[i, j] = false;
                        }
                        else {
                            worldCopy[i, j] = true;
                        }
                    }
                    else
                    {
                        int c = CountNeighbors(i, j, true);
                        if(c == 3){
                            worldCopy[i, j] = true;
                        }
                    }
                }
            }

            world = worldCopy;

            Draw();

            Console.WriteLine();
        }

        public void AddRandom()
        {
            Random rand = new Random();
            Add(rand.Next(96), rand.Next(32));
        }

        public void Add(int x, int y){
            world[x, y] = true;
        }

        public int CountNeighbors(int px, int py, bool countEmpty)
        {
            int count = 0;
            for(int i=-1; i <= 1; i++){
                for(int j=-1; j <= 1; j++)
                {
                    int x = px - i, y = py - j;
                    if((x == px && y == py) || x < 0 || y < 0 || x >= 96 || y >= 32) 
                        continue;

                    if((!countEmpty && world[x, y]) || (countEmpty && world[x, y])){
                        count++;
                    }
                }
            }

            return count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.Start();
        }
    }
}
