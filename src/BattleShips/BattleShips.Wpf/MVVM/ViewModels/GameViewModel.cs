using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BattleShips.Game.Enums;
using BattleShips.Game.Helper;
using BattleShips.Game.Players;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.Helper.DTOs;
using BattleShips.Wpf.MVVM.Helper.NavigationService;

namespace BattleShips.Wpf.MVVM.ViewModels;

public class GameViewModel : BaseViewModel
{
    public string Headline
    {
        get => _headline;
        set => SetProperty(ref _headline, value);
    }

    public string LeftGridHeadline
    {
        get => _leftGridHeadline;
        private init => SetProperty(ref _leftGridHeadline, value);
    }
    
    public string RightGridHeadline
    {
        get => _rightGridHeadline;
        private init => SetProperty(ref _rightGridHeadline, value);
    }
    
    public Button[,] LeftButtons
    {
        get => _leftButtons;
        set => SetProperty(ref _leftButtons, value);
    }
    
    public Button[,] RightButtons
    {
        get => _rightButtons;
        set => SetProperty(ref _rightButtons, value);
    }
    
    public string[,] LeftBtnContentArray
    {
        get => _leftBtnContentArray; 
        set => SetProperty(ref _leftBtnContentArray, value);
    }
    
    public string[,] RightBtnContentArray
    {
        get => _rightBtnContentArray; 
        set => SetProperty(ref _rightBtnContentArray, value);
    }
    
    public Color OceanButtonHoverColor
    {
        get => _oceanButtonHoverColor;
        set => SetProperty(ref _oceanButtonHoverColor, value);
    }
    
    private readonly INavigator _navigator;
    private readonly ICommand _updateCurrentViewModelCommand;
    private readonly string _leftGridHeadline = "";
    private readonly string _rightGridHeadline = "";
    private readonly GameDto _dto;
    private string _headline = "";
    private Player _currentPlayer;
    private Player _enemyPlayer;
    private Button[,] _leftButtons;
    private Button[,] _rightButtons;
    private string[,] _leftBtnContentArray;
    private string[,] _rightBtnContentArray;
    private bool _activeBoardIsLeft = false;
    private Color _oceanButtonHoverColor = Colors.Black;
    
    public GameViewModel(GameDto dto)
    {
        _navigator = dto.Navigator;
        _updateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator);
        _dto = dto;

        _currentPlayer = dto.Player1;
        _enemyPlayer = dto.Player2;
        _leftButtons = new Button[10, 10];
        _rightButtons = new Button[10, 10];
        _leftBtnContentArray = new string[10, 10];
        _rightBtnContentArray = new string[10, 10];

        Headline = $"       {_currentPlayer.Name} du bist an der Reihe!\nWähle ein Feld auf {_enemyPlayer.Name}'s Brett aus:";
        LeftGridHeadline = $"{dto.Player1.Name}'s Spielbrett:";
        RightGridHeadline = $"{dto.Player2.Name}'s Spielbrett:";
    }

    public void OceanButton_Clicked(object sender, RoutedEventArgs args)
    {
        if (sender is not Button btn) return;

        if ((btn.Name.StartsWith("Left") && !_activeBoardIsLeft) || (btn.Name.StartsWith("Right") && _activeBoardIsLeft))
            return;
        
        var x = Grid.GetColumn(btn) - 1;
        var y = Grid.GetRow(btn) - 1;
        var pos = new Position(x, y);

        if (!_currentPlayer.PlaceShot(_enemyPlayer, pos)) return;
        
        UpdateOceans();
        if (!_currentPlayer.IsHit(_enemyPlayer, pos))
        {
            SwitchPlayer();
        }
        else
        {
            CheckForWin();
        }
    }
    
    public void OceanButton_MouseEnter(object sender, RoutedEventArgs args)
    {
        if (sender is not Button btn) return;
        
        btn.Style = (Style)Application.Current.Resources["OceanGameButtonHover"];

        Color colors;
        
        if (btn.Name.StartsWith("Left"))
        {
            colors = _activeBoardIsLeft ? Colors.Green : Colors.Red;
        }
        else
        {
            colors = !_activeBoardIsLeft ? Colors.Green : Colors.Red;
        }

        OceanButtonHoverColor = colors;
    }
    
    public void OceanButton_MouseLeave(object sender, RoutedEventArgs args)
    {
        if (sender is not Button btn) return;
        
        btn.Style = (Style)Application.Current.Resources["OceanGameButton"];
    }
    
    private void UpdateOceans()
    {
        var ships = new []
        {
            TileStatusEnum.Submarine,
            TileStatusEnum.Destroyer,
            TileStatusEnum.Cruiser,
            TileStatusEnum.Battleship,
            TileStatusEnum.Carrier
        };
        
        for (var row = 0; row < 10; row++)
        {
            for (var column = 0; column < 10; column++)
            {
                var text = ships.Contains(_dto.Player1.Board.Ocean[row, column].Status) ? 
                    (char)TileStatusEnum.Empty : (char)_dto.Player1.Board.Ocean[row, column].Status;
                LeftBtnContentArray[row, column] = text.ToString();
                
                text = ships.Contains(_dto.Player2.Board.Ocean[row, column].Status) ? 
                    (char)TileStatusEnum.Empty : (char)_dto.Player2.Board.Ocean[row, column].Status;
                RightBtnContentArray[row, column] = text.ToString();
            }
        }
        
        OnPropertyChanged(nameof(LeftBtnContentArray));
        OnPropertyChanged(nameof(RightBtnContentArray));
    }
    
    private void SwitchPlayer()
    {
        // Werte tauschen
        (_currentPlayer, _enemyPlayer) = (_enemyPlayer, _currentPlayer);
        _activeBoardIsLeft = !_activeBoardIsLeft;
        Headline = $"       {_currentPlayer.Name} du bist an der Reihe! Wähle ein Feld auf {_enemyPlayer.Name}'s Brett aus:";

        CheckIfSecondPlayerIsComputer();
    }
    
    private void CheckForWin()
    {
        if (!_enemyPlayer.HasLost) return;
        
        var selection = MessageBox.Show(
            $"Glückwunsch!\n{_currentPlayer.Name}\nDu hast Gewonnen!\n\n" +
            $"Ja = Revenge | Nein = Hauptmenü | Abbruch = Beenden", 
            "Spiel vorbei",
            MessageBoxButton.YesNoCancel,
            MessageBoxImage.Information);
        
        switch (selection)
        {
            case MessageBoxResult.Yes:
                _updateCurrentViewModelCommand.Execute(new ShipPlacementDto(
                    _navigator, 
                    ViewsEnum.ShipPlacement,
                    new HumanPlayer(_enemyPlayer.Name),
                    new HumanPlayer(_currentPlayer.Name),
                    false));
                break;
            case MessageBoxResult.No:
                _updateCurrentViewModelCommand.Execute(new ViewSwitchDto(_navigator, ViewsEnum.Menu));
                break;
            default:
                Environment.Exit(0);
                break;
        }
    }

    private void CheckIfSecondPlayerIsComputer()
    {
        if (_currentPlayer is not ComputerPlayer bot) return;

        ComputerPlayerTurn(bot);
    }

    private void ComputerPlayerTurn(Player bot)
    {
        var rng = new Random();

        var isHit = true;
        do
        {
            var pos = new Position(rng.Next(0, 10), rng.Next(0, 10));
            if (bot.PlaceShot(_enemyPlayer, pos))
            {
                isHit = bot.IsHit(_enemyPlayer, pos);
            }
            UpdateOceans();
        } while (isHit);
        
        SwitchPlayer();
    }
    
    private Position GetPositionFromButton(UIElement btn)
    {
        var row = Grid.GetRow(btn) - 1;
        var column = Grid.GetColumn(btn) - 1;
        return new Position(column, row);
    }
}