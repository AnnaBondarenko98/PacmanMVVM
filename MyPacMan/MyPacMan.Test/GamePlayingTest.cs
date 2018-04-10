using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPacMan.BLL;
using Moq;
using System.Collections.Generic;
using Models;
using MyPacMan.BLL.Interfaces;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;


namespace MyPacMan.Test
{
   
    [TestClass]
    public class GamePlayingTest
    {
        [TestMethod]
        public void CreateWallsTest()
        {
            var mock = new Mock<GamePlaying>(250, 1, 1, 1);

            mock.Object.CreateWalls();

            Assert.AreEqual(20, mock.Object.Walls.Count);



        }
        [TestMethod]
        public void CreateApplesTest()
        {
            var mock = new Mock<GamePlaying>(250,1,1,1);
            var mock2 = new Mock<List<Apple>>();
            mock.Object.Apples = mock2.Object;
            mock.Object.AmountApples = 1;
            mock.Object.CreateEnemies();

            Assert.AreEqual(1, mock.Object.Enemies.Count);




        }
        [TestMethod]
        public void CreateApplesAmountTest()
        {
            var mock = new Mock<GamePlaying>(250, 1, 1, 1);
            var mock2 = new Mock<List<Apple>>();
            mock.Object.Apples = mock2.Object;
            mock.Object.AmountApples = 0;
         

            mock.Object.CreateApples();

            Assert.AreEqual(0, mock.Object.Apples.Count);



        }
    
        [TestMethod]
        public void CreateEnemiesTest()
        {
            var mock = new Mock<GamePlaying>(250, 1, 1, 1);
            var mock2 = new Mock<List<Enemey>>();
            mock.Object.Enemies = mock2.Object;
            mock.Object.AmountEnemy = 1;

            mock.Object.CreateEnemies();

            Assert.AreEqual(1, mock.Object.Enemies.Count);



        }
        [TestMethod]
        public void CreateEnemiesAmountTest()
        {
            var mock = new Mock<GamePlaying>(250, 1, 1, 1);
            var mock2 = new Mock<List<Enemey>>();
            mock.Object.Enemies = mock2.Object;
            mock.Object.AmountEnemy = 0;

            mock.Object.CreateEnemies();

            Assert.AreEqual(0, mock.Object.Enemies.Count);



        }
        [TestMethod]
        public void PlayStatusTest()
        {
            var mock = new Mock<GamePlaying>(250, 1, 1, 1);
            mock.Object.Pacman.X = 0;
            mock.Object.Pacman.Y = 0;
            mock.Object.Status = GameStatus.stopping;

            mock.Object.Play();
            Assert.AreEqual(0, mock.Object.Pacman.X);
            Assert.AreEqual(0, mock.Object.Pacman.Y );

        }
        [TestMethod]
        public void PlayPacManRunTest()
        {
            var mock = new Mock<GamePlaying>(250, 1, 1, 1);
            var pacmanmock = new Mock<PacMan>(250);
            mock.Object.Pacman = pacmanmock.Object;
            mock.Object.Pacman.X = 0;
            mock.Object.Pacman.Y = 0;
            mock.Object.Pacman.Direct_x = 1;
            mock.Object.Pacman.Direct_y = 1;
            mock.Object.Status = GameStatus.playing;

            mock.Object.Play();
          
            Assert.AreNotEqual(0, mock.Object.Pacman.X );
            Assert.AreNotEqual(0, mock.Object.Pacman.Y);

        }
        [TestMethod]
        public void PlayPacManTurnTest()
        {
            var mock = new Mock<GamePlaying>(250, 1, 1, 1);
            var pacmanmock = new Mock<PacMan>(250);
            mock.Object.Pacman = pacmanmock.Object;
            mock.Object.Pacman.X = 40;
            mock.Object.Pacman.Y = 40;
            mock.Object.Pacman.Direct_x = 0;
            mock.Object.Pacman.Direct_y = 0;
            mock.Object.Pacman.Nextdirect_x = 1;
            mock.Object.Pacman.Nextdirect_y = 1;
            mock.Object.Status = GameStatus.playing;

            mock.Object.Play();

            Assert.AreEqual(1, mock.Object.Pacman.Direct_x);
            Assert.AreEqual(1, mock.Object.Pacman.Direct_y);

        }
        [TestMethod]
        public void PlayPacManPutImageTest()
        {
            var mock = new Mock<GamePlaying>(250, 1, 1, 1);
            var pacmanmock = new Mock<PacMan>(250);
            mock.Object.Pacman = pacmanmock.Object;
            mock.Object.Pacman.X = 40;
            mock.Object.Pacman.Y = 40;
            mock.Object.Pacman.Direct_x = 0;
            mock.Object.Pacman.Direct_y = 0;
            mock.Object.Pacman.Nextdirect_x = 1;
            mock.Object.Pacman.Nextdirect_y = 1;
            mock.Object.Status = GameStatus.playing;

            mock.Object.Play();

            Assert.AreEqual(1, mock.Object.Pacman.Direct_x);
            Assert.AreNotSame(mock.Object.Pacman.Image.PucRight, mock.Object.Pacman.Image.PucImg);

        }
        [TestMethod]
        public void PlayLooserTest()
        {
            var mock = new Mock<GamePlaying>(250, 1, 1, 1);
            var pacmanmock = new Mock<PacMan>(250);
            var enemiesmock = new Mock<List<Enemey>>();
            enemiesmock.Object.Add(new Enemey(250,10,10,new List<Wall>() {new Wall(1,2),new Wall(1,2) }));
            mock.Object.Pacman = pacmanmock.Object;
            mock.Object.Pacman.X = 5;
            mock.Object.Pacman.Y = 5;
            mock.Object.Enemies = enemiesmock.Object;
           
            mock.Object.Status = GameStatus.playing;

            mock.Object.Play();

            Assert.AreEqual(GameStatus.looser, mock.Object.Status);
           

        }
        [TestMethod]
        public void PlayWinnerTest()
        {
            var mock = new Mock<GamePlaying>(250, 1, 1, 1);
            var pacmanmock = new Mock<PacMan>(250);
            var applesmock = new Mock<List<Apple>>();
            applesmock.Object.Add(new Apple( 7, 7));
            mock.Object.Pacman = pacmanmock.Object;
            mock.Object.Pacman.X = 5;
            mock.Object.Pacman.Y = 5;
            mock.Object.Apples = applesmock.Object;

            mock.Object.Status = GameStatus.playing;

            mock.Object.Play();

            Assert.AreEqual(GameStatus.winner, mock.Object.Status);


        }
    }
}
