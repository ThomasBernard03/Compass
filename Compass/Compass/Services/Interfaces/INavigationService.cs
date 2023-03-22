using System;
using System.Threading.Tasks;
using Compass.ViewModels;

namespace Compass.Services.Interfaces;

public interface INavigationService
{
    Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel;
    Task GoBackAsync();
}

