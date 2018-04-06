using Models;
using MyPacMan.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyPacMan.BLL.Interfaces;

namespace MyPacMan.Plugin2
{
    class GamePlayingPlugin2:IGamePlaying
    {

        Random random;
        private int sizeField;
        private int amountEnemy;
        private int amountApples;
        private int speedGame;
        private List<Enemey> enemies;
        private List<Apple> apples;
        private List<Wall> walls;
        private Wall wall;
        private int collectedApple;
        private PacMan pacman;
        private int randomParam1 = 6;
        private int randomParam2 = 5;
        private GameStatus status;
        /// <summary>
        /// Returns or sets array of <see cref="Models.Enemey" /> for game
        /// </summary>
        public List<Enemey> Enemies { get => enemies; set => enemies = value; }
        /// <summary>
        /// Returns or sets array of <see cref="Models.Apple" /> for game
        /// </summary>
        public List<Apple> Apples { get => apples; set => apples = value; }
        /// <summary>
        ///  Returns or sets a count of Apples
        /// </summary>
        public int CollectedApple { get => CollectedApple1; set => CollectedApple1 = value; }
        /// <summary>
        /// Returns or sets a  <see cref="Models.PacMan" /> for game
        /// </summary>
        public PacMan Pacman { get => pacman; set => pacman = value; }
        /// <summary>
        /// Returns or sets a size of field for game
        /// </summary>
        public int SizeField { get => sizeField; set => sizeField = value; }

        public int AmountEnemy { get => amountEnemy; set => amountEnemy = value; }
        // <summary>
        /// Returns or sets a amount of apples for game
        /// </summary>

        public int AmountApples { get => amountApples; set => amountApples = value; }
        /// <summary>
        /// Returns or sets a speed for game
        /// </summary>
        public int SpeedGame { get => speedGame; set => speedGame = value; }
        /// <summary>
        /// Returns or sets a  <see cref="Models.Wall" /> for game
        /// </summary>
        public Wall Wall { get => wall; set => wall = value; }
        public int CollectedApple1 { get => collectedApple; set => collectedApple = value; }
        /// <summary>
        /// Returns or sets array of <see cref="GameStatus" /> for game
        /// </summary>
        public GameStatus Status { get => status; set => status = value; }
        public int RandomParam1 { get => randomParam1; set => randomParam1 = value; }
        /// <summary>
        /// Random's parameter for Y coordinate
        /// </summary>
        public int RandomParam2 { get => randomParam2; set => randomParam2 = value; }

        public List<Wall> Walls { get => walls; set => walls = value; }
        public GamePlayingPlugin2()
        {
            int sizeField = 250;
            int amountEnemy = 8;
            int amountApples = 8;
            int speedGame = 30;
            random = new Random();
            this.SizeField = sizeField;
            this.AmountEnemy = amountEnemy;
            this.AmountApples = amountApples;
            this.SpeedGame = speedGame;
            Status = GameStatus.stopping;
            enemies = new List<Enemey>();
            apples = new List<Apple>();
            walls = new List<Wall>();
            Wall = new Wall();
            Pacman = new PacMan(sizeField, Walls);
            CreateWalls();
            CreateApples();
            CreateEnemies();


        }
        /// <summary>
        /// 
        ///  Initialize new instance of the <see cref="MyPacMan.Plugin.GamePlayingPlugin" /> class
        /// </summary>
        /// <param name="sizeField"></param>
        /// <param name="amountEnemy"></param>
        /// <param name="amountApples"></param>
        /// <param name="speedGame"></param>
        public GamePlayingPlugin2(int sizeField=250, int amountEnemy=8, int amountApples=8, int speedGame=30)
        {
            random = new Random();
            this.SizeField = sizeField;
            this.AmountEnemy = amountEnemy;
            this.AmountApples = amountApples;
            this.SpeedGame = speedGame;
            Status = GameStatus.stopping;
            enemies = new List<Enemey>();
            apples = new List<Apple>();
            walls = new List<Wall>();
            Wall = new Wall();
            Pacman = new PacMan(sizeField, Walls);
            CreateWalls();
            CreateApples();
            CreateEnemies();


        }
        /// <summary>
        /// Creates a List of  <see cref="Models.Wall" /> 
        /// </summary>
        public void CreateWalls()
        {
            int x, y;
            bool flag = true;
            while (walls.Count < 20)
            {
                flag = true;
                x = random.Next(1, 10) * 20;
                y = random.Next(1, 10) * 20;
                foreach (Wall en in walls)
                {

                    if (en.X == x && en.Y == y)
                    { flag = false; break; }


                }
                if (flag)
                {
                    walls.Add(new Wall(x, y));
                }
            }
        }
        /// <summary>
        /// Creates a List of  <see cref="Models.Apple" /> class
        /// </summary>
        public void CreateApples()
        {
            int x, y;
            bool flag = true;
            while (apples.Count < AmountApples)
            {
                flag = true;
                x = random.Next(RandomParam1) * 40;
                y = random.Next(RandomParam1) * 40;
                foreach (var wall in walls)
                {
                    if (((Math.Abs(wall.X - x) < 18) && (Math.Abs(wall.Y - y) < 18)))
                        flag = false;
                    foreach (Apple en in apples)
                    {

                        // if ((en.X == x && en.Y == y) || (wall.X == x && wall.Y == y))
                        if ((Math.Abs(en.Y - y) < 18 && Math.Abs(en.X - x) < 18))
                            flag = false;



                    }


                }
                if (flag)
                {
                    apples.Add(new Apple(x, y));

                }
            }

        }
        /// <summary>
        /// Creates a List of  <see cref="Models.Apple" /> 
        /// </summary>
        public void CreateEnemies()
        {
            int x, y;
            bool flag = true;
            while (enemies.Count < AmountEnemy)
            {
                flag = true;
                x = random.Next(RandomParam1) * 40;
                y = random.Next(RandomParam2) * 40;
                foreach (var wall in walls)
                {
                    if (((Math.Abs(wall.X - x) < 18) && (Math.Abs(wall.Y - y) < 18)))
                        flag = false;

                    foreach (Enemey en in enemies)
                    {
                        //if ((en.X == x && en.Y == y) |( wall.X == x && wall.Y == y))
                        if ((Math.Abs(en.Y - y) < 18 && Math.Abs(en.X - x) < 18))
                            flag = false;



                    }


                }
                if (flag)
                {
                    enemies.Add(new Enemey(SizeField, x, y, Walls));

                }

            }
        }
        /// <summary>
        /// Runs the program
        /// </summary>
        public void Play()
        {


            while (Status == GameStatus.playing)
            {
                Thread.Sleep(SpeedGame);
                pacman.Run();
                foreach (var enemey in Enemies)
                {
                    enemey.Run();
                }

                for (int i = 0; i < enemies.Count; i++)
                {
                    if (
                        (Math.Abs(enemies[i].X - pacman.X) <= 10 && (enemies[i].Y == pacman.Y))
                        || (Math.Abs(enemies[i].Y - pacman.Y) <= 10 && (enemies[i].X == pacman.X))
                        || (Math.Abs(enemies[i].Y - pacman.Y) <= 10 && (Math.Abs(enemies[i].X - pacman.X) <= 10)))
                    {
                        Status = GameStatus.looser;


                    }

                }

                for (int i = 0; i < apples.Count; i++)
                {

                    if (Math.Abs(pacman.X - apples[i].X) < 10 && Math.Abs(pacman.Y - apples[i].Y) < 10)
                    {

                        apples[i].Flag = true;

                        if (apples[i].Flag == true) ++CollectedApple1;
                    }
                    if (CollectedApple1 == 5) { Status = GameStatus.winner; }
                }




            }
        }
    }
}
