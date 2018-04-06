using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;
using System.Threading;
using Library.Repositories;
using Library.Entities;
using MyPacMan.BLL;
using System.Windows.Threading;
using MyPacMan.BLL.Interfaces;

using System.Reflection;
using Models;
using NLog;
using Xceed.Wpf.AvalonDock.Controls;
using Localizator;
using MyPacMan.Views;

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
        private DispatcherTimer timer2;
        MyCanvas currentCanvas;
        Window currentWindow;
        bool IsGameLoad = false;
        Button check;
        TextBox start;
        private Menu menu;
        public int lifesCount = 3;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Logger updateLogger = LogManager.GetLogger("UpdateLogger");
        private Random random;
        private List<Wall> walls = new List<Wall>();
        private DateTime date;
        private DateTime currdate;
        private Window register;
        public int LifesCount { get => lifesCount; set => lifesCount = value; }
        public DateTime Date { get => date; set => date = value; }
        #endregion

        #region Commands

        /// <summary>
        /// Returns or sets command for change language to russian
        /// </summary>
        public ICommand RussianCommand { get; set; }

        /// <summary>
        /// Returns or sets command for change language to english
        /// </summary>
        public ICommand EnglishCommand { get; set; }

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
                      check = obj as Button;


                      var fullpath = AppDomain.CurrentDomain.BaseDirectory;

                      var dllPath = string.Empty;
                      if (fullpath.Contains(@"MyPacMan\bin\Debug\"))
                      {
                          dllPath = fullpath.Replace(@"MyPacMan\bin\Debug\", @"MyPacMan\bin\Plugins");
                      }
                      else if (fullpath.Contains(@"MyPacMan\bin\Release\"))
                      {
                          dllPath = fullpath.Replace(@"MyPacMan\bin\Release\", @"MyPacMan\bin\Plugins");
                      }


                      DirectoryInfo info = new DirectoryInfo(dllPath);

                      FileInfo[] files = info.GetFiles();
                      List<Assembly> pluginAssemblies = new List<Assembly>();
                      foreach (var file in files)
                      {
                          pluginAssemblies.Add(Assembly.LoadFrom(file.FullName));
                      }

                          ;
                      Window window;

                      window = new PluginPresenter();

                      var listView = (ListView)window.Content;
                      listView.ItemsSource = pluginAssemblies;
                      Assembly selectedAssembly;
                      listView.SelectionChanged += (o, e) =>
                      {
                          ListView l = o as ListView;
                          selectedAssembly = l.SelectedItem as Assembly;
                          foreach (var plugin in selectedAssembly.GetTypes())
                          {
                              if (plugin.BaseType != null)

                                  gamePlaying = (IGamePlaying)Activator.CreateInstance(plugin);
                              logger.Info("Additional plugin was loaded");
                          }
                          currentCanvas.Children.Clear();
                          IsGameLoad = false;

                          CanvasLoadedCommand.Execute(currentCanvas);
                          window.Close();

                      };
                      listView.Items.Refresh();
                      window.ShowDialog();

                      //Assembly pluginAssembly=null;
                      //foreach (var file in files)
                      //{
                      //    pluginAssembly = Assembly.LoadFrom(file.FullName);
                      //}




                      //foreach (var plugin in pluginAssembly.GetTypes())
                      //{
                      //    if (plugin.BaseType != null)

                      //        gamePlaying = (IGamePlaying)Activator.CreateInstance(plugin, new object[] { 250, 8, 8, 30 });
                      //    logger.Info("Additional plugin was loaded");
                      //}
                      //currentCanvas.Children.Clear();
                      //IsGameLoad = false;

                      //CanvasLoadedCommand.Execute(currentCanvas);










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
                      start = obj as TextBox;

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

                      register.Close();


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

                      DrawWall(currentCanvas);
                      Display(currentCanvas);

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
                      if (start == null) start = obj as TextBox;
                      if (current.Name != null && current.Name != "")
                      {
                          if (gamePlaying.Status == GameStatus.playing)
                          {

                              thread.Abort();
                              gamePlaying.Status = GameStatus.stopping;
                              this.timer.Stop();
                              menu.IsEnabled = true;
                              check.IsEnabled = true;
                              start.Focusable = true;
                              logger.Info("The game was stopped");
                          }
                          else
                          {


                              menu.IsEnabled = false;
                              check.IsEnabled = false;
                              gamePlaying.Status = GameStatus.playing;
                              StartGame();
                              thread = new Thread(gamePlaying.Play);
                              thread.Start();
                              logger.Info("The game is being launched");
                              start.Focusable = false;
                              IEnumerable<Label> l = currentWindow.FindVisualChildren<Label>();
                              foreach (var lab in l)
                              {
                                  if (lab.Name == "lifeLabel") lab.Content = lifesCount;
                              }

                          }
                      }
                      else
                      {
                          // MessageBox.Show("Entrer your last name or create new!");
                          register = new Register();
                          register.Show();


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
        public ApplicationViewModel(Window window, Menu m)
        {
            menu = m;
            register = new Window();
            currentWindow = window;
            repos = new UnitOfRepository("MyDB");
            current = new Player();
            gamePlaying = new GamePlaying(250, 5, 5, 40);
            check = new Button();
            currentWindow = window;
            repos.PlayerRep.Get(0);
            random = new Random();
            date = new DateTime(0);
            currdate = new DateTime(0).AddMinutes(5);
            this.timer2 = new DispatcherTimer();
            this.timer2.Interval = TimeSpan.FromSeconds(1);
            this.timer2.Tick += this.Tick;
            RussianCommand = new ClassCommand(Russian);
            EnglishCommand = new ClassCommand(English);
            ResourceManagerService.RegisterManager("MainResources", MainResources.ResourceManager, true);

        }
        private void Russian(object obj)
        {

            ResourceManagerService.ChangeLocale("ru-RU");

        }

        private void English(object obj)
        {

            ResourceManagerService.ChangeLocale("en-US");
        }
        /// <summary>
        /// Runs the instance of  <see cref="Timer" />
        /// </summary>
        public void StartGame()
        {

            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(10);
            this.timer.Tick += this.GameLoop;
            this.timer.Start();
            this.timer2.Start();

        }
        private void Tick(object sender, EventArgs e)
        {

            date = date.AddSeconds(1);


        }

        private void GameLoop(object sender, EventArgs e)
        {

            IEnumerable<Label> l = currentWindow.FindVisualChildren<Label>();
            foreach (var lab in l)
            {
                if (lab.Name == "TimeLabel") lab.Content = date.Minute + " " + date.Second;
            }
            if (currdate.Minute - date.Minute == 0)
            {
                gamePlaying.Status = GameStatus.looser;
                this.timer2.Stop();
            }
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
            else if (gamePlaying.Status == GameStatus.winner)
            {
                thread.Abort();
                timer.Stop();
                if (current.Record < GamePlaying.CustomApple) current.Record = GamePlaying.CustomApple;
                repos.PlayerRep.Update(current);
                date = new DateTime(0);
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
                    var fullpath = AppDomain.CurrentDomain.BaseDirectory;

                    var dllPath = string.Empty;
                    if (fullpath.Contains(@"MyPacMan\bin\Debug\"))
                    {
                        dllPath = fullpath.Replace(@"MyPacMan\bin\Debug\", @"MyPacMan\bin\Plugins");
                    }
                    else if (fullpath.Contains(@"MyPacMan\bin\Release\"))
                    {
                        dllPath = fullpath.Replace(@"MyPacMan\bin\Release\", @"MyPacMan\bin\Plugins");
                    }


                    DirectoryInfo info = new DirectoryInfo(dllPath);

                    FileInfo[] files = info.GetFiles();
                    List<Assembly> pluginAssemblies = new List<Assembly>();
                    foreach (var file in files)
                    {
                        pluginAssemblies.Add(Assembly.LoadFrom(file.FullName));
                    }

                    foreach (var p in pluginAssemblies)
                    {

                        foreach (var plugin in p.GetTypes())
                        {
                            if (plugin.BaseType != null && gamePlaying.GetType().FullName == plugin.FullName)

                                gamePlaying = (IGamePlaying)Activator.CreateInstance(plugin);
                            logger.Info("Additional plugin was loaded");
                        }
                    }

                    currentCanvas.Children.Clear();
                    IsGameLoad = false;

                    CanvasLoadedCommand.Execute(currentCanvas);
                }

                PlayCommand.Execute(new object());

            }
            else
            {
                thread.Abort();
                timer.Stop();
                if (current.Record < GamePlaying.CustomApple) current.Record = GamePlaying.CustomApple;
                repos.PlayerRep.Update(current);

                if (lifesCount == 0)
                {
                    this.timer2.Stop();
                    date = new DateTime(0);



                    lifesCount = 3;
                    MessageBoxResult msbox = MessageBox.Show("Do you want to try again?", "The game is over. ", MessageBoxButton.OKCancel);
                    if (msbox == MessageBoxResult.OK)
                    {
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
                            var fullpath = AppDomain.CurrentDomain.BaseDirectory;

                            var dllPath = string.Empty;
                            if (fullpath.Contains(@"MyPacMan\bin\Debug\"))
                            {
                                dllPath = fullpath.Replace(@"MyPacMan\bin\Debug\", @"MyPacMan\bin\Plugins");
                            }
                            else if (fullpath.Contains(@"MyPacMan\bin\Release\"))
                            {
                                dllPath = fullpath.Replace(@"MyPacMan\bin\Release\", @"MyPacMan\bin\Plugins");
                            }


                            DirectoryInfo info = new DirectoryInfo(dllPath);

                            FileInfo[] files = info.GetFiles();
                            List<Assembly> pluginAssemblies = new List<Assembly>();
                            foreach (var file in files)
                            {
                                pluginAssemblies.Add(Assembly.LoadFrom(file.FullName));
                            }

                            foreach (var p in pluginAssemblies)
                            {

                                foreach (var plugin in p.GetTypes())
                                {
                                    if (plugin.BaseType != null && gamePlaying.GetType().FullName == plugin.FullName)

                                        gamePlaying = (IGamePlaying)Activator.CreateInstance(plugin);
                                    logger.Info("Additional plugin was loaded");
                                }
                            }

                            currentCanvas.Children.Clear();
                            IsGameLoad = false;

                            CanvasLoadedCommand.Execute(currentCanvas);

                        }

                        PlayCommand.Execute(new object());

                    }
                    else
                    {
                        MessageBox.Show("Your Result:" + GamePlaying.CustomApple);
                        GamePlaying.CustomApple = 0;
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
                            var fullpath = AppDomain.CurrentDomain.BaseDirectory;

                            var dllPath = string.Empty;
                            if (fullpath.Contains(@"MyPacMan\bin\Debug\"))
                            {
                                dllPath = fullpath.Replace(@"MyPacMan\bin\Debug\", @"MyPacMan\bin\Plugins");
                            }
                            else if (fullpath.Contains(@"MyPacMan\bin\Release\"))
                            {
                                dllPath = fullpath.Replace(@"MyPacMan\bin\Release\", @"MyPacMan\bin\Plugins");
                            }


                            DirectoryInfo info = new DirectoryInfo(dllPath);

                            FileInfo[] files = info.GetFiles();
                            List<Assembly> pluginAssemblies = new List<Assembly>();
                            foreach (var file in files)
                            {
                                pluginAssemblies.Add(Assembly.LoadFrom(file.FullName));
                            }

                            foreach (var p in pluginAssemblies)
                            {

                                foreach (var plugin in p.GetTypes())
                                {
                                    if (plugin.BaseType != null && gamePlaying.GetType().FullName == plugin.FullName)

                                        gamePlaying = (IGamePlaying)Activator.CreateInstance(plugin);
                                    logger.Info("Additional plugin was loaded");
                                }
                            }

                            currentCanvas.Children.Clear();
                            IsGameLoad = false;

                            CanvasLoadedCommand.Execute(currentCanvas);
                        }

                    }
                }
                else if (lifesCount > 0)
                {
                    lifesCount--;
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
                        var fullpath = AppDomain.CurrentDomain.BaseDirectory;

                        var dllPath = string.Empty;
                        if (fullpath.Contains(@"MyPacMan\bin\Debug\"))
                        {
                            dllPath = fullpath.Replace(@"MyPacMan\bin\Debug\", @"MyPacMan\bin\Plugins");
                        }
                        else if (fullpath.Contains(@"MyPacMan\bin\Release\"))
                        {
                            dllPath = fullpath.Replace(@"MyPacMan\bin\Release\", @"MyPacMan\bin\Plugins");
                        }


                        DirectoryInfo info = new DirectoryInfo(dllPath);

                        FileInfo[] files = info.GetFiles();
                        List<Assembly> pluginAssemblies = new List<Assembly>();
                        foreach (var file in files)
                        {
                            pluginAssemblies.Add(Assembly.LoadFrom(file.FullName));
                        }

                        foreach (var p in pluginAssemblies)
                        {

                            foreach (var plugin in p.GetTypes())
                            {
                                if (plugin.BaseType != null && gamePlaying.GetType().FullName == plugin.FullName)

                                    gamePlaying = (IGamePlaying)Activator.CreateInstance(plugin);
                                logger.Info("Additional plugin was loaded");
                            }
                        }

                        currentCanvas.Children.Clear();
                        IsGameLoad = false;

                        CanvasLoadedCommand.Execute(currentCanvas);
                    }


                    PlayCommand.Execute(new object());

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
            foreach (Wall wall in gamePlaying.Walls)
            {

                if (wall.CurrentImg.Source == null)
                {
                    wall.CurrentImg.Source = wall.Image.image;

                    MyCanvas.SetLeft(wall.CurrentImg, wall.X);
                    MyCanvas.SetTop(wall.CurrentImg, wall.Y);

                    canvas.Children.Add(wall.CurrentImg);
                }
                else
                {
                    wall.CurrentImg.Source = wall.Image.image;
                    MyCanvas.SetLeft(wall.CurrentImg, wall.X);
                    MyCanvas.SetTop(wall.CurrentImg, wall.Y);
                }


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
