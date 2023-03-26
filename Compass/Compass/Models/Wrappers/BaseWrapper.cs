using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Compass.Models.Wrappers;

public abstract class BaseWrapper : INotifyPropertyChanged
{
	public BaseWrapper()
	{
	}

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string name = "") =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

