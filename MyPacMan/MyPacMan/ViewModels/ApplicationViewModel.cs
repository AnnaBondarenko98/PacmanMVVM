using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;
using MessageBox = System.Windows.MessageBox;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;
using System.Threading;
using Library.Repositories;
using Library.Entities;
using System.Windows.Media.Imaging;
using MyPacMan.BLL;
using System.Windows.Threading;
using MyPacMan.BLL.Interfaces;

using System.Reflection;
using NLog;

namespace MyPacMan.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region vars
        UnitOfRepository repos;
        Player current;
        IGamePlaying gamePlaying;
        Thread thread;
        private DispatcherTimer timer;
        MyCanvas currentCanvas;
        Window currentWindow;
        bool IsGameLoad = false;
        CheckBox check;
        TextBox start;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Logger updateLogger = LogManager.GetLogger("UpdateLogger");

        #endregion

        #region Commands
        /// <summary>
        /// Returns or sets a command that checks CheckBox 
        /// </summary>
        private ClassCommand checkHardGame;
        public ClassCommand CheckHardGame
        {
            get
            {
                return checkHardGame ??
                  (checkHardGame = new ClassCommand(obj =>
                  {
                      check = obj as CheckBox;
                      if (check.IsChecked == true)
                      {
                          var fullpath = AppDomain.CurrentDomain.BaseDirectory;

                          var dllPath = string.Empty;

                          if (fullpath.Contains(@"MyPacMan\bin\Debug\"))
                          {
                              dllPath = fullpath.Replace(@"MyPacMan\bin\Debug\", @"Plugins\PacMan.Plugin.dll");
                          }
                          else if (fullpath.Contains(@"MyPacMan\bin\Release\"))
                          {
                              dllPath = fullpath.Replace(@"MyPacMan\bin\Release\", @"Plugins\PacMan.Plugin.dll");
                          }
                          var pluginAssembly = Assembly.LoadFrom(dllPath);


                          foreach (var plugin in pluginAssembly.GetTypes())
                          {
                              if (plugin.BaseType != null)

                                  gamePlaying = (IGamePlaying)Activator.CreateInstance(plugin, new object[] { 250, 10, 10, 20 });
                              logger.Info("Additional plugin was loaded");
                          }
                          currentCanvas.Children.Clear();
                          IsGameLoad = false;

                          CanvasLoadedCommand.Execute(currentCanvas);
                         
                         

                      }
                      else
                      {
                          gamePlaying = new GamePlaying(250, 5, 5, 40);
                          currentCanvas.Children.Clear();
                          IsGameLoad = false;
                          CanvasLoadedCommand.Execute(currentCanvas);
                         


                      }


                  }));
            }
        }

        private ClassCommand topPlayersCommand;
        public ClassCommand TopPlayersCommand
        {
            get
            {
                return topPlayersCommand ??
                  (topPlayersCommand = new ClassCommand(obj =>

                  {
                      IEnumerable<Player> players = repos.PlayerRep.GetAll();
                      IEnumerable<Player> ordered = players.OrderByDescending(p => p.Record).Take(10).AsEnumerable<Player>();
                      Presenter.Show(ordered);
                  }));
            }
        }
        /// <summary>
        ///  Returns or sets a command that creates or gets new <see cref="Player" /> 
        /// </summary>
        private ClassCommand newPlayerCommand;
        public ClassCommand NewPlayerCommand
        {
            get
            {

                return newPlayerCommand ??
                  (newPlayerCommand = new ClassCommand(obj =>

                  {

                      TextBox text = obj as TextBox;
                      if ((text.Text != "" && text.Text != null) && repos.PlayerRep.FindByPred(c => c.Name == text.Text).Count() == 0)
                      {
                          current = new Player();
                          current.Name = text.Text;
                          repos.PlayerRep.Create(current);
                          MessageBox.Show($" New Player {current.Name} was created!");

                      }
                      else if (repos.PlayerRep.FindByPred(c => c.Name == text.Text).Count() != 0)
                      {
                          current = repos.PlayerRep.FindByPred(c => c.Name == text.Text).First();
                          MessageBox.Show($"Your nickname is {current.Name}.");
                      }
                      else
                      {
                          MessageBox.Show("Entrer your last name or create new!");
                      }




                  }));
            }
        }

        /// <summary>
        ///  Returns or sets a command that loads Canvas
        /// </summary>
        private ClassCommand canvasLoadedCommand;
        public ClassCommand CanvasLoadedCommand
        {
            get
            {
                return canvasLoadedCommand ??
                  (canvasLoadedCommand = new ClassCommand(obj =>

                  {

                      currentCanvas = obj as MyCanvas;


                      Display(currentCanvas);
                      DrawWall(currentCanvas);
                      IsGameLoad = true;


                  }));
            }

        }
        /// <summary>
        ///  Returns or sets a command that runs  game
        /// </summary>
        private ClassCommand playCommand;
        public ClassCommand PlayCommand
        {
            get
            {
                return playCommand ??
                  (playCommand = new ClassCommand(obj =>

                  {
                       if(start==null) start = obj as TextBox;
                      if (current.Name != null && current.Name != "")
                      {
                          if (gamePlaying.Status == GameStatus.playing)
                          {

                              thread.Abort();
                              gamePlaying.Status = GameStatus.stopping;
                              this.timer.Stop();
                              check.IsEnabled = true;
                              start.Focusable = true;
                              logger.Info("The game was stopped");
                          }
                          else
                          {


                              check.IsEnabled = false;
                              gamePlaying.Status = GameStatus.playing;
                              StartGame();
                              thread = new Thread(gamePlaying.Play);
                              thread.Start();
                              logger.Info("The game is being launched");
                              start.Focusable = false;


                          }
                      }
                      else
                      {
                          MessageBox.Show("Entrer your last name or create new!");
                      }



                  }));
            }
        }
        private ClassCommand window_KeyDownCommand;
        public ClassCommand Window_KeyDownCommand
        {
            get
            {
                return window_KeyDownCommand ??
                  (window_KeyDownCommand = new ClassCommand(obj =>

                  {
                      Window_KeyDown(obj);
                  }));
            }
        }

        #endregion

        /// <summary>
        ///  Initialize new instance of the <see cref="ApplicationViewModel" /> class
        /// </summary>
        /// <param name="window"></param>
        public ApplicationViewModel(Window window)
        {
            currentWindow = window;
            repos = new UnitOfRepository("MyDB");
            current = new Player();
            gamePlaying = new GamePlaying(250, 5, 5, 40);
            check = new CheckBox();
            currentWindow = window;
            
        }
        /// <summary>
        /// Runs the instance of  <see cref="Timer" />
        /// </summary>
        public void StartGame()
        {
            //if(  this.timer!=null) this.timer.Dispatcher.BeginInvokeShutdown(DispatcherPriority.Send);
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(10);
            this.timer.Tick += this.GameLoop;
            this.timer.Start();

        }

        private void GameLoop(object sender, EventArgs e)
        {
            Display(currentCanvas);
        }
        /// <summary>
        /// Displays List of <see cref="Models.Enemey" />, <see cref="Models.Wall" />, <see cref="Models.Apple" /> and  <see cref="Models.PacMan" />
        /// </summary>
        /// <param name="canvas"></param>
        public void Display(MyCanvas canvas)
        {
            if (gamePlaying.Status == GameStatus.playing || IsGameLoad == false)
            {
                DrawApples(canvas);
                DrawEnemey(canvas);
                DrawPacman(canvas);
            }
            else
            {
                //gamePlaying.status = GameStatus.playing;
                //  PlayCommand.Execute(new object());
                // timer.Dispatcher.BeginInvokeShutdown(DispatcherPriority.Send);

                thread.Abort();
                timer.Stop();
                current.Record = gamePlaying.CollectedApple;
                repos.PlayerRep.Update(current);
                MessageBoxResult msbox = MessageBox.Show("Do you want to try again?", "The game is over. ", MessageBoxButton.OKCancel);
                if (msbox == MessageBoxResult.OK)
                {

                    //  gamePlaying.status = GameStatus.playing;
                    currentCanvas.Children.Clear();
                    IsGameLoad = false;
                    start = new TextBox();
                    
                    start.Focusable = true;
                    if (gamePlaying.GetType() == typeof(GamePlaying))
                    {
                        
                        gamePlaying = new GamePlaying(250, 5, 5, 40);
                        CanvasLoadedCommand.Execute(currentCanvas);
                      
                       
                    }
                    else
                    {
                        CheckHardGame.Execute(check);
                    }


                    PlayCommand.Execute(new object());





                }
                else
                {
                    currentWindow.Close();
                }
            }



        }
        /// <summary>
        /// Draws  <see cref="Models.PacMan" /> on the Canvas
        /// </summary>
        /// <param name="canvas"></param>
        private void DrawPacman(MyCanvas canvas)
        {
            if (gamePlaying.Pacman.CurrentPacman.Source == null)
            {
                gamePlaying.Pacman.CurrentPacman.Source = gamePlaying.Pacman.Image.PucImg;

                MyCanvas.SetLeft(gamePlaying.Pacman.CurrentPacman, gamePlaying.Pacman.X);
                MyCanvas.SetTop(gamePlaying.Pacman.CurrentPacman, gamePlaying.Pacman.Y);

                canvas.Children.Add(gamePlaying.Pacman.CurrentPacman);
            }
            else
            {
                gamePlaying.Pacman.CurrentPacman.Source = gamePlaying.Pacman.Image.PucImg;

                MyCanvas.SetLeft(gamePlaying.Pacman.CurrentPacman, gamePlaying.Pacman.X);
                MyCanvas.SetTop(gamePlaying.Pacman.CurrentPacman, gamePlaying.Pacman.Y);
            }
        }
        /// <summary>
        /// Draws  <see cref="Models.Apple" /> on the Canvas
        /// </summary>
        /// <param name="canvas"></param>
        public void DrawApples(MyCanvas canvas)
        {


            for (int i = 0; i < gamePlaying.Apples.Count; i++)
            {
                if (gamePlaying.Apples[i].CurrentImage.Source == null)
                {
                    gamePlaying.Apples[i].CurrentImage.Source = gamePlaying.Apples[i].Image.Image;

                    MyCanvas.SetLeft(gamePlaying.Apples[i].CurrentImage, gamePlaying.Apples[i].X);
                    MyCanvas.SetTop(gamePlaying.Apples[i].CurrentImage, gamePlaying.Apples[i].Y);

                    canvas.Children.Add(gamePlaying.Apples[i].CurrentImage);
                }
                else
                {
                    if (gamePlaying.Apples[i].Flag != true)
                    {
                        gamePlaying.Apples[i].CurrentImage.Source = gamePlaying.Apples[i].Image.Image;
                        MyCanvas.SetLeft(gamePlaying.Apples[i].CurrentImage, gamePlaying.Apples[i].X);
                        MyCanvas.SetTop(gamePlaying.Apples[i].CurrentImage, gamePlaying.Apples[i].Y);

                    }
                    else
                    {
                        canvas.Children.Remove(gamePlaying.Apples[i].CurrentImage);
                        gamePlaying.Apples.Remove((gamePlaying.Apples[i]));


                    }

                }
            }

        }
        /// <summary>
        /// Draws  <see cref="Models.Wall" />s on the Canwas
        /// </summary>
        /// <param name="canvas"></param>
        public void DrawWall(MyCanvas canvas)
        {
            int i = 1;
            for (int x = 20; x < 200; x += 40)
                for (int y = 20; y < 200; y += 40)
                {
                    Image img = new Image();
                    img.Source = gamePlaying.Wall.Image.image;
                    // img.Name = "img"+i;

                    MyCanvas.SetLeft(img, x);
                    MyCanvas.SetTop(img, y);

                    canvas.Children.Add(img);
                    i++;
                }

        }
        /// <summary>
        /// Draws  <see cref="Models.Enemey" /> on the Canvas
        /// </summary>
        /// <param name="canvas"></param>
        public void DrawEnemey(MyCanvas canvas)
        {
            foreach (var enemey in gamePlaying.Enemies)
            {

                if (enemey.CurrentImage.Source == null)
                {
                    enemey.CurrentImage.Source = enemey.Image.EnemeyImg;

                    MyCanvas.SetLeft(enemey.CurrentImage, enemey.X);
                    MyCanvas.SetTop(enemey.CurrentImage, enemey.Y);

                    canvas.Children.Add(enemey.CurrentImage);
                }
                else
                {
                    enemey.CurrentImage.Source = enemey.Image.EnemeyImg;
                    MyCanvas.SetLeft(enemey.CurrentImage, enemey.X);
                    MyCanvas.SetTop(enemey.CurrentImage, enemey.Y);
                }


            }

        }

        private void OnPropertyChanged(string propname)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));


        }
      
        public void Window_KeyDown(object sender)
        {
            if (Keyboard.IsKeyDown(Key.Left))
            {
                gamePlaying.Pacman.Nextdirect_x = -1;
                gamePlaying.Pacman.Nextdirect_y = 0;
            }

            else if (Keyboard.IsKeyDown(Key.Right))
            {
                gamePlaying.Pacman.Nextdirect_x = 1;
                gamePlaying.Pacman.Nextdirect_y = 0;
            }

            else if (Keyboard.IsKeyDown(Key.Up))
            {
                gamePlaying.Pacman.Nextdirect_x = 0;
                gamePlaying.Pacman.Nextdirect_y = -1;
            }
            else if (Keyboard.IsKeyDown(Key.Down))
            {
                gamePlaying.Pacman.Nextdirect_x = 0;
                gamePlaying.Pacman.Nextdirect_y = 1;
            }


        }



    }
}
