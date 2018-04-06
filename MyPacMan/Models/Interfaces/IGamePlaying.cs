using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPacMan.BLL.Interfaces
{/// <summary>
/// Interface for logic in the game 
/// </summary>
    public interface IGamePlaying
    {

        /// <summary>
        /// Returns or sets array of <see cref="Models.Enemey" /> for game
        /// </summary>
        List<Wall> Walls { get; set; }
        /// <summary>
        /// Returns or sets array of <see cref="Models.Enemey" /> for game
        /// </summary>
        List<Enemey> Enemies { get; set; }
        /// <summary>
        /// Returns or sets array of <see cref="Models.Apple" /> for game
        /// </summary>
        List<Apple> Apples { get; set; }
        /// <summary>
        ///  Returns or sets a count of Apples
        /// </summary>
        int CollectedApple { get; set; }
        /// <summary>
        /// Returns or sets a  <see cref="Models.PacMan" /> for game
        /// </summary>
        PacMan Pacman { get; set; }
        /// <summary>
        /// Returns or sets array of <see cref="GameStatus" /> for game
        /// </summary>
        GameStatus Status { get; set; }
        int CollectedApple1 { get; set; }
        /// <summary>
        /// Returns or sets a  <see cref="Models.Wall" /> for game
        /// </summary>
        Wall Wall { get; set; }
        /// <summary>
        /// Returns or sets a speed for game
        /// </summary>
        int SpeedGame { get; set; }
        /// <summary>
        /// Returns or sets a amount of apples for game
        /// </summary>
        int AmountApples { get; set; }
        /// <summary>
        /// Returns or sets a amount of <see cref="Models.Enemey" /> for game
        /// </summary>
        int AmountEnemy { get; set; }
        /// <summary>
        /// Returns or sets a size of field for game
        /// </summary>
        int SizeField { get; set; }

        /// <summary>
        /// Runs the program
        /// </summary>
        void Play();
        /// <summary>
        /// Creates a List of  <see cref="Models.Apple" /> class
        /// </summary>
        void CreateEnemies();
        /// <summary>
        /// Creates a List of  <see cref="Models.Apple" /> 
        /// </summary>
        void CreateApples();
        /// <summary>
        /// Random's parameter for X coordinate
        /// </summary>
        int RandomParam1 { get; set; }
        /// <summary>
        /// Random's parameter for Y coordinate
        /// </summary>
        int RandomParam2 { get; set; }
    }
}
