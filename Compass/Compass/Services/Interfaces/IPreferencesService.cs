using System;
namespace Compass.Services.Interfaces;

public interface IPreferencesService
{
    bool IsThemeBasedOnSystem { get; set; }
    bool IsThemeLight { get; set; }
}