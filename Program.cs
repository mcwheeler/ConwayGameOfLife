using System;
using System.Threading;

namespace hack_gol
{
    class Game
    {
        private int offsetLeft = 2;
        private int offsetTop = 2;
        private int worldWidth = 96;
        private int worldHeight = 32;

        private bool[,] world;
        private bool musicThread;

        public void Start()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.Clear();
            Console.WriteLine("Game, started!");
            
            PlayCoolMusic();
            CreateInfinty();
            Draw();

            while(true)
            {
                Tick();

                if(Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(true);
                    if(keyInfo.Key == ConsoleKey.Spacebar)    { AddRandom(); }
                    else if(keyInfo.Key == ConsoleKey.R)      { Start(); break; }
                    else if(keyInfo.Key == ConsoleKey.Q)      { CreateInfinty(); }
                    else if(keyInfo.Key == ConsoleKey.W)      { CreateRandom(); }
                    else if(keyInfo.Key == ConsoleKey.Escape) { break; }
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(1, offsetTop+worldHeight+3);
        }

        public void Draw()
        {
            for(int j=0; j < worldHeight; j++) {
                for(int i=0; i < worldWidth; i++) {
                    DrawUpdate(i, j);
                }
            }
        }

        public void DrawUpdate(int x, int y) {
            DrawUpdate(x, y, world[x, y]);
        }

        public void clearWorld() 
        {
            world = new bool[worldWidth, worldHeight];
            Draw();
        }

        public void CreateInfinty() 
        {
            clearWorld();
            
            int x = worldWidth/2 - 5;
            int y = worldHeight/2 - 2;

            Add(x, y);    Add(x+2, y);  Add(x+4, y);
            Add(x, y+1);                Add(x+4, y+1);
            Add(x, y+2);                Add(x+4, y+2);
            Add(x, y+3);                Add(x+4, y+3);
            Add(x, y+4); Add(x+2, y+4); Add(x+4, y+4);
        }

        public void CreateRandom()
        {
            clearWorld();
            for(int i=0; i < worldWidth; i++) {
                AddRandom();
            }
        }
        
        public void DrawUpdate(int x, int y, bool colored)
        {
            Console.BackgroundColor = colored ? ConsoleColor.Blue : ConsoleColor.Yellow;
            Console.SetCursorPosition(offsetLeft+x, offsetTop+y);
            Console.Write(" ");
            Console.ResetColor();
        }

        public void Tick()
        {
            // five frames per second.. mind blow..
            Thread.Sleep(1000/5);

            var worldCopy = (bool[,])world.Clone();
            for(int i=0; i < worldWidth; i++)
            {
                for(int j=0; j < worldHeight; j++)
                {   
                    if(worldCopy[i, j])
                    {
                        int c = CountNeighbors(i, j, false);
                        if(c <= 1 || c >= 4){
                            worldCopy[i, j] = false;
                            DrawUpdate(i, j, false);
                        }
                        else {
                            worldCopy[i, j] = true;
                            DrawUpdate(i, j, true);
                        }
                    }
                    else
                    {
                        int c = CountNeighbors(i, j, true);
                        if(c == 3){
                            worldCopy[i, j] = true;
                            DrawUpdate(i, j, true);
                        }
                    }
                }
            }

            world = worldCopy;
        }

        public void AddRandom()
        {
            Random rand = new Random();
            Add(rand.Next(worldWidth), rand.Next(worldHeight));
        }

        public void Add(int x, int y){
            world[x, y] = true;
            DrawUpdate(x, y, true);
        }

        public int CountNeighbors(int px, int py, bool countEmpty)
        {
            int count = 0;
            for(int i=-1; i <= 1; i++){
                for(int j=-1; j <= 1; j++)
                {
                    int x = px - i, y = py - j;
                    if((x == px && y == py) || x < 0 || y < 0 || x >= worldWidth || y >= worldHeight){ 
                        continue;
                    }

                    if((!countEmpty && world[x, y]) || (countEmpty && world[x, y])){
                        count++;
                    }
                }
            }

            return count;
        }

        /* Created by TwoTinyTrees
         * October 7, 2015
         * https://www.reddit.com/r/PowerShell/comments/3o13fq/cool_song_in_system_beeps_using_powershell/
         */
        public void PlayCoolMusic()
        {
            new Thread(() => {
                for (int i=0; i < 100; i++) 
                {
                    Console.Beep(450, 110);
                    Console.Beep(500, 110);
                    Console.Beep(550, 110);
                    Console.Beep(450, 110);
                    Console.Beep(675, 200);
                    Console.Beep(675, 200);
                    Console.Beep(600, 300);
                    Console.Beep(450, 110);
                    Console.Beep(500, 110);
                    Console.Beep(550, 110);
                    Console.Beep(450, 110);
                    Console.Beep(600, 200);
                    Console.Beep(600, 200);
                    Console.Beep(550, 300);
                    Console.Beep(525, 110);
                    Console.Beep(450, 300);
                    Console.Beep(450, 110);
                    Console.Beep(500, 110);
                    Console.Beep(550, 110);
                    Console.Beep(450, 110);
                    Console.Beep(500, 400);
                    Console.Beep(600, 300);
                    Console.Beep(500, 400);
                    Console.Beep(475, 200);
                    Console.Beep(450, 200);
                    Console.Beep(400, 200);
                    Console.Beep(600, 500);
                    Console.Beep(525, 500);
                    Console.Beep(450, 110);
                    Console.Beep(500, 110);
                    Console.Beep(550, 110);
                    Console.Beep(450, 110);
                    Console.Beep(675, 200);
                    Console.Beep(675, 200);
                    Console.Beep(600, 300);
                    Console.Beep(450, 110);
                    Console.Beep(500, 110);
                    Console.Beep(550, 110);
                    Console.Beep(450, 110);
                    Console.Beep(800, 200);
                    Console.Beep(500, 200);
                    Console.Beep(550, 300);
                    Console.Beep(525, 110);
                    Console.Beep(450, 300);
                    Console.Beep(450, 110);
                    Console.Beep(500, 110);
                    Console.Beep(550, 110);
                    Console.Beep(450, 110);
                    Console.Beep(500, 400);
                    Console.Beep(600, 300);
                    Console.Beep(500, 400);
                    Console.Beep(475, 200);
                    Console.Beep(450, 200);
                    Console.Beep(400, 200);
                    Console.Beep(600, 500);
                    Console.Beep(525, 500);
            }
            }).Start();
        }

        public void pauseMusic() {
            // Well shit.
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
