using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Compass.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{
	public BaseViewModel()
	{
	}

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string name = "") =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

