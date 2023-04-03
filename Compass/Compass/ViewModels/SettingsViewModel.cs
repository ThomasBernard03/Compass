using System;
using Compass.Services.Interfaces;

namespace Compass.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private readonly IPreferencesService _preferencesService;

    public SettingsViewModel(INavigationService navigationService, IPreferencesService preferencesService) : base(navigationService)
    {
        _preferencesService = preferencesService;

        GoSettingsCommand = new Command(OnGoSettingsCommand);
        OpenUrlCommand = new Command<string>(async x => await OnOpenUrlCommand(x));

        IsDarkMode = !_preferencesService.IsThemeLight;
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
        _preferencesService.IsThemeLight = !isDark;
        Application.Current.UserAppTheme = isDark ? AppTheme.Dark : AppTheme.Light;
    }


    public string Version { get; } = AppInfo.Current.VersionString;

    #region IsSystemeTheme
    private bool _isSystemTheme;
    public bool IsSystemeTheme
    {
        get => _isSystemTheme;
        set {
            _isSystemTheme = value;
            OnPropertyChanged(nameof(IsSystemeTheme));
        }
    }
    #endregion

    #region IsDarkMode
    private bool _isDarkMode;
    public bool IsDarkMode
    {
        get => _isDarkMode;
        set
        {
            _isDarkMode = value;
            OnPropertyChanged(nameof(IsDarkMode));
            ThemeChanged(value);
        }
    }
    #endregion
}

