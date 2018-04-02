using Library.Entities;
using MyPacMan.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyPacMan
{
    public class Presenter
    {
        /// <summary>
        /// Shows windows
        /// </summary>

        public static void Show(IEnumerable<Player> p)
        {

            

            Window window;


            window = new ListPresenter();


            var listView = (ListView)window.Content;
            listView.ItemsSource = p;

            listView.Items.Refresh();
            window.ShowDialog();




        }

    }
}
