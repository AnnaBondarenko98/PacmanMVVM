using Models;
using MyPacMan.BLL.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace MyPacMan.BLL
{
    public class GamePlaying : IGamePlaying
    {
         Random random;
        private int sizeField;
        private int amountEnemy;
        private int amountApples;
        private int speedGame;
        private List<Enemey> enemies;
        private List<Apple> apples;
        private Wall wall;
        private int collectedApple;
        private PacMan pacman;
        private int  randomParam1;
        private int randomParam2 ;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Logger updateLogger = LogManager.GetLogger("UpdateLogger");

        private GameStatus status;
        /// <summary>
        /// Returns or sets array of <see cref="Models.Enemey" /> for game
        /// </summary>
        public List<Enemey> Enemies { get => enemies; set => enemies = value; }
        /// <summary>
        ///  Returns or sets array of <see cref="Models.Apple" /> for game
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
        /// Returns or sets array of <see cref="GameStatus" /> for game
        /// </summary>
        public GameStatus Status { get => status; set => status = value; }
        public int CollectedApple1 { get => collectedApple; set => collectedApple = value; }
        /// <summary>
        /// Returns or sets a  <see cref="Models.Wall" /> for game
        /// </summary>
        public Wall Wall { get => wall; set => wall = value; }
        /// <summary>
        /// Returns or sets a speed for game
        /// </summary>
        public int SpeedGame { get => speedGame; set => speedGame = value; }
        /// <summary>
        /// Returns or sets a amount of apples for game
        /// </summary>
        public int AmountApples { get => amountApples; set => amountApples = value; }
        /// <summary>
        /// Returns or sets a amount of <see cref="Models.Enemey" /> for game
        /// </summary>
        public int AmountEnemy { get => amountEnemy; set => amountEnemy = value; }
        /// <summary>
        /// Returns or sets a size of field for game
        /// </summary>
        public int SizeField { get => sizeField; set => sizeField = value; }
        /// <summary>
        /// Random's parameter for X coordinate
        /// </summary>
        public int RandomParam1 { get => randomParam1; set => randomParam1 = value; }
        /// <summary>
        /// Random's parameter for Y coordinate
        /// </summary>
        public int RandomParam2 { get => randomParam2; set => randomParam2 = value; }

        /// <summary>
        ///  Initialize new instance of the <see cref="GamePlaying" /> class
        /// </summary>
        /// <param name="sizeField"></param>
        /// <param name="amountEnemy"></param>
        /// <param name="amountApples"></param>
        /// <param name="speedGame"></param>
        public GamePlaying(int sizeField, int amountEnemy, int amountApples, int speedGame)
        {
            random = new Random();
            this.SizeField = sizeField;
            this.AmountEnemy = amountEnemy;
            this.AmountApples = amountApples;
            this.SpeedGame = speedGame;
            Status = GameStatus.stopping;
            enemies = new List<Enemey>();
            apples = new List<Apple>();
            Wall = new Wall();
            Pacman = new PacMan(sizeField);
            randomParam1 = 6;
            randomParam2=5;
            CreateApples();
            CreateEnemies();
            


        }
        /// <summary>
        /// Creates a List of  <see cref="Models.Apple" /> 
        /// </summary>
        public void CreateApples()
        {
            int x, y;
            bool flag = true;
            while (apples.Count < AmountApples)
            {
                x = random.Next(RandomParam1) * 40;
                y = random.Next(RandomParam1) * 40;
                foreach (Apple en in apples)
                {
                    flag = true;
                    if (en.X == x && en.Y == y)
                        flag = false;
                   
                    break;
                }
                if (flag)
                {
                    apples.Add(new Apple(x, y));
                    logger.Info("One Apple was created ");
                }
            }
            logger.Info("All Apples were created");
        }
        /// <summary>
        /// Creates a List of  <see cref="Models.Apple" /> class
        /// </summary>
        public void CreateEnemies()
        {
            int x, y;
            bool flag = true;
            while (enemies.Count < AmountEnemy)
            {
                x = random.Next(RandomParam1) * 40;
                y = random.Next(RandomParam2) * 40;
                foreach (Enemey en in enemies)
                {
                    flag = true;
                    if ((en.X == x && en.Y == y))
                        flag = false;
                    
                    break;
                }
                if (flag)
                {
                    enemies.Add(new Enemey(SizeField, x, y));
                    logger.Info("One Enemey was created ");
                }
                logger.Info("All Enemies were created");
            }
        }
        /// <summary>
        /// Runs the program
        /// </summary>
        public void Play()
        {
            logger.Info($"Start Game in the method Play()");
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
                        logger.Info("The game is over.The player are looser.");

                    }

                }

                for (int i = 0; i < apples.Count; i++)
                {

                    if (Math.Abs(pacman.X - apples[i].X) < 3 && Math.Abs(pacman.Y - apples[i].Y) < 3)
                    {

                        apples[i].Flag = true;

                        if (apples[i].Flag == true) ++CollectedApple1;
                    }
                    if (CollectedApple1 == 5) { Status = GameStatus.winner; logger.Info("The game is over.The player are winner."); }
                }
               



            }
        }

      
    }
}
