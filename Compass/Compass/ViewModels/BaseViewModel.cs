using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Compass.Services.Interfaces;

namespace Compass.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{

    public INavigationService NavigationService;

	public BaseViewModel(INavigationService navigationService)
	{
        NavigationService = navigationService;
    }


    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string name = "") =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public virtual async Task OnNavigatedFrom(NavigatedFromEventArgs args)
    {

    }

    public virtual async Task OnNavigatedTo()
    {

    }
}

