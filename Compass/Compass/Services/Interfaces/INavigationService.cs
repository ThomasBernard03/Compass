using System;
using System.Threading.Tasks;
using Compass.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Compass.Services.Interfaces;

public interface INavigationService
{
    Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel;
    Task GoBackAsync();

    Task NavigateToModalAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel;
    Task CloseModalAsync();
}

