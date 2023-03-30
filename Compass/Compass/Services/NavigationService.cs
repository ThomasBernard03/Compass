using System;
using Compass.Services.Interfaces;
using Compass.ViewModels;
using Compass.Views;
using Compass.Views.Pages;

namespace Compass.Services;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<Type, Type> _mappings;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _mappings = new Dictionary<Type, Type>();
        CreateMap();
    }

    private void CreateMap()
    {
        // Ajoutez les mappings entre les ViewModels et les Views ici
        _mappings.Add(typeof(CompassViewModel), typeof(CompassPage));
        _mappings.Add(typeof(MapViewModel), typeof(MapPage));
    }

    public async Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
    {
        var viewModelType = typeof(TViewModel);
        if (!_mappings.ContainsKey(viewModelType))
        {
            throw new ArgumentException($"No mapping found for {viewModelType}.");
        }

        var viewType = _mappings[viewModelType];
        var view = (Page)_serviceProvider.GetService(viewType);
        var viewModel = (TViewModel)_serviceProvider.GetService(viewModelType);

        viewModel.NavigationService = this;
        view.BindingContext = viewModel;

        if (view is CompassPage mainPage)
        {
            var navPage = new NavigationPage(mainPage);
            await Application.Current.MainPage.Navigation.PushAsync(navPage);
        }
        else
        {
            await Application.Current.MainPage.Navigation.PushAsync(view);
        }
    }

    public async Task GoBackAsync()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }

    public async Task NavigateToModalAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
    {
        var viewModelType = typeof(TViewModel);
        if (!_mappings.ContainsKey(viewModelType))
        {
            throw new ArgumentException($"No mapping found for {viewModelType}.");
        }

        var viewType = _mappings[viewModelType];
        var view = (Page)_serviceProvider.GetService(viewType);
        var viewModel = (TViewModel)_serviceProvider.GetService(viewModelType);

        viewModel.NavigationService = this;
        view.BindingContext = viewModel;


        await Application.Current.MainPage.Navigation.PushModalAsync(view);
    }

    public async Task CloseModalAsync()
    {
        await Application.Current.MainPage.Navigation.PopModalAsync();
    }
}
