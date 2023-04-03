using System;
using Compass.Services.Interfaces;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace Compass.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private readonly IPreferencesService _preferencesService;

    public SettingsViewModel(INavigationService navigationService, IPreferencesService preferencesService) : base(navigationService)
    {
        _preferencesService = preferencesService;

        GoSettingsCommand = new Command(OnGoSettingsCommand);
        OpenUrlCommand = new Command<string>(async x => await OnOpenUrlCommand(x));
    }


    public Command GoSettingsCommand { get; private set; }
    private void OnGoSettingsCommand() {
        AppInfo.Current.ShowSettingsUI();
    }

    public Command<string> OpenUrlCommand { get; private set; }
    private async Task OnOpenUrlCommand(string url)
    {
        Uri uri = new Uri(url);
        BrowserLaunchOptions options = new BrowserLaunchOptions()
        {
            LaunchMode = BrowserLaunchMode.SystemPreferred,
            TitleMode = BrowserTitleMode.Show,
        };

        await Browser.Default.OpenAsync(uri, options);
    }


    private void ThemeChanged(bool isDark)
    {
        if (!_preferencesService.IsThemeBasedOnSystem)
            Application.Current.UserAppTheme = isDark ? AppTheme.Dark : AppTheme.Light;
    }

    private void SystemeThemePreferenceChanged(bool isBasedOnSystem)
    {
        Application.Current.UserAppTheme = isBasedOnSystem ? AppTheme.Unspecified : _preferencesService.IsDarkTheme ? AppTheme.Dark : AppTheme.Light;
    }


    public string Version { get; } = AppInfo.Current.VersionString;

    #region IsSystemeTheme
    public bool IsSystemeTheme
    {
        get => _preferencesService.IsThemeBasedOnSystem;
        set {
            _preferencesService.IsThemeBasedOnSystem = value;
            OnPropertyChanged(nameof(IsSystemeTheme));
            SystemeThemePreferenceChanged(value);
        }
    }
    #endregion

    #region IsDarkMode
    public bool IsDarkMode
    {
        get => _preferencesService.IsDarkTheme;
        set
        {
            _preferencesService.IsDarkTheme = value;
            OnPropertyChanged(nameof(IsDarkMode));
            ThemeChanged(value);
        }
    }
    #endregion
}

