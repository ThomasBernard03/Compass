using System;
using Compass.Services.Interfaces;

namespace Compass.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    public SettingsViewModel(INavigationService navigationService) : base(navigationService)
    {
        GoSettingsCommand = new Command(OnGoSettingsCommand);
    }


    public Command GoSettingsCommand { get; private set; }
    private void OnGoSettingsCommand() {
        AppInfo.Current.ShowSettingsUI();
    }


    public string Version => AppInfo.Current.VersionString;
}

