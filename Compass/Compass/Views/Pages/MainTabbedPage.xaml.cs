﻿namespace Compass.Views.Pages;

public partial class MainTabbedPage : TabbedPage
{
	public MainTabbedPage(IServiceProvider serviceProvider)
	{
		InitializeComponent();

        var compassPage = serviceProvider.GetService<CompassPage>();
        var mapPage = serviceProvider.GetService<MapPage>();
        var settingsPage = serviceProvider.GetService<SettingsPage>();

        this.tab.Children.Add(compassPage);
        this.tab.Children.Add(mapPage);
        this.tab.Children.Add(settingsPage);
    }
}
