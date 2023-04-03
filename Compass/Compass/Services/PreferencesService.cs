using System;
using Compass.Services.Interfaces;

namespace Compass.Services;

public class PreferencesService : IPreferencesService
{
    public bool IsThemeBasedOnSystem
    {
        get => Preferences.Get(nameof(IsThemeBasedOnSystem), true);
        set => Preferences.Set(nameof(IsThemeBasedOnSystem), value);
    }

    public bool IsDarkTheme
	{
		get => Preferences.Get(nameof(IsDarkTheme), false);
		set => Preferences.Set(nameof(IsDarkTheme), value);
    }
}