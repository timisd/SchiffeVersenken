using System.Windows.Input;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.Helper.DTOs;

namespace BattleShips.Wpf.MVVM.ViewModels;

public class GameViewModel : BaseViewModel
{
    private readonly ICommand _updateCurrentViewModelCommand;
    public GameViewModel(GameDto dto)
    {
        _updateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(dto.Navigator);
    }
}