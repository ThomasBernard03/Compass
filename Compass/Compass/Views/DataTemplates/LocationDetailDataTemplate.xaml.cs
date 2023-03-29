using Compass.Models.Wrappers;

namespace Compass.Views.DataTemplates;

public partial class LocationDetailDataTemplate : ContentView
{
	public LocationWrapper Location { get; private set; }

	public LocationDetailDataTemplate(LocationWrapper location)
	{
		Location = location;

        InitializeComponent();
		this.BindingContext = this;
	}
}
