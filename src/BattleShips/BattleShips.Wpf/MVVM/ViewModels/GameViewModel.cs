using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BattleShips.Game.Enums;
using BattleShips.Game.Helper;
using BattleShips.Game.Players;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.Helper.DTOs;

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
    
    private readonly ICommand _updateCurrentViewModelCommand;
    private string _headline = "";
    private readonly string _leftGridHeadline = "";
    private readonly string _rightGridHeadline = "";
    private Player _currentPlayer;
    private Player _enemyPlayer;
    private Button[,] _leftButtons;
    private Button[,] _rightButtons;
    private string[,] _leftBtnContentArray;
    private string[,] _rightBtnContentArray;
    private bool _currentPlayerBoardIsLeft = true;
    
    public GameViewModel(GameDto dto)
    {
        _updateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(dto.Navigator);
        
        _currentPlayer = dto.Player1;
        _enemyPlayer = dto.Player2;
        _leftButtons = new Button[10, 10];
        _rightButtons = new Button[10, 10];
        _leftBtnContentArray = new string[10, 10];
        _rightBtnContentArray = new string[10, 10];

        Headline = $"{_currentPlayer.Name} du bist an der Reihe! Wähle ein Feld auf {_enemyPlayer.Name}'s Brett aus:";
        LeftGridHeadline = $"{dto.Player1.Name}'s Spielbrett:";
        RightGridHeadline = $"{dto.Player2.Name}'s Spielbrett:";
    }

    public void OceanButton_Clicked(object sender, RoutedEventArgs e)
    {
        if (sender is not Button btn) return;

        var x = Grid.GetColumn(btn) - 1;
        var y = Grid.GetRow(btn) - 1;
        var pos = new Position(x, y);

        if (!_currentPlayer.PlaceShot(_enemyPlayer, pos)) return;
        
        UpdateOcean();
        if (!_currentPlayer.IsHit(_enemyPlayer, pos))
        {
            SwitchPlayer();
        }
        else
        {
            btn.IsEnabled = false;
            CheckForWin();
        }
    }

    private void UpdateOcean()
    {
        var ships = new []
        {
            TileStatusEnum.Submarine,
            TileStatusEnum.Destroyer,
            TileStatusEnum.Cruiser,
            TileStatusEnum.Battleship,
            TileStatusEnum.Carrier
        };
        var buttons = _currentPlayerBoardIsLeft ? RightBtnContentArray : LeftBtnContentArray;
        
        for (var row = 0; row < 10; row++)
        {
            for (var column = 0; column < 10; column++)
            {
                var text = ships.Contains(_enemyPlayer.Board.Ocean[row, column].Status) ? 
                    (char)TileStatusEnum.Empty : (char)_enemyPlayer.Board.Ocean[row, column].Status;
                buttons[row, column] = text.ToString();
            }
        }

        OnPropertyChanged(nameof(LeftBtnContentArray));
        OnPropertyChanged(nameof(RightBtnContentArray));
    }
    
    private void SwitchPlayer()
    {
        // Werte tauschen
        (_currentPlayer, _enemyPlayer) = (_enemyPlayer, _currentPlayer);
        _currentPlayerBoardIsLeft = !_currentPlayerBoardIsLeft;
        Headline = $"{_currentPlayer.Name} du bist an der Reihe! Wähle ein Feld auf {_enemyPlayer.Name}'s Brett aus:";
        ChangeButtonStatusForGrid(_currentPlayerBoardIsLeft, true, _enemyPlayer);
        ChangeButtonStatusForGrid(!_currentPlayerBoardIsLeft, false, _currentPlayer);
    }
    
    private void ChangeButtonStatusForGrid(bool left, bool enabled, Player player)
    {
        var buttons = left ? RightButtons : LeftButtons;
        for (var row = 0; row < 10; row++)
        {
            for (var col = 0; col < 10; col++)
            {
                buttons[row, col].IsEnabled = !player.Board.Ocean[row, col].IsShot && enabled;
            }
        }
    }

    private void CheckForWin()
    {
        if (_enemyPlayer.HasLost)
        {
            MessageBox.Show($"Glückwunsch!\n{_currentPlayer.Name}\nDu hast Gewonnen!");
        }
    }
}