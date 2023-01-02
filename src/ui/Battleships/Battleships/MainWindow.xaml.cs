using System.Windows;
using Battleships.MVVM.ViewModels;
using Battleships.Services.Navigation;

namespace Battleships;

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

    private void WindowBar_OnLoaded(object sender, RoutedEventArgs e)
    {
        MouseLeftButtonDown += delegate { DragMove(); };
    }
}