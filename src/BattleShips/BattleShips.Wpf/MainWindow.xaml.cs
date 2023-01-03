using System.Windows;
using BattleShips.Wpf.MVVM.Helper.NavigationService;
using BattleShips.Wpf.MVVM.ViewModels;

namespace BattleShips.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = new MainViewModel(this, new Navigator());
        }
    }
}