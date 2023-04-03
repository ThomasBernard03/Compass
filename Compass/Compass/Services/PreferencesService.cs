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

    public bool IsThemeLight
	{
		get => Preferences.Get(nameof(IsThemeLight), true);
		set => Preferences.Set(nameof(IsThemeLight), value);
    }
}