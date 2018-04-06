using MyPacMan.ViewModels;
using System.Windows;

namespace MyPacMan.Views
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register
    {
      
        public  ApplicationViewModel _viewModel;
        public Register()
        {
            
            _viewModel = MainWindow.viewModel;
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
