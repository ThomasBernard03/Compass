using Compass.ViewModels;

namespace Compass.Views.DataTemplates;

public partial class CreateLocationDataTemplate : ContentView
{
	private readonly CreateLocationViewModel _createLocationViewModel;

    public CreateLocationDataTemplate(CreateLocationViewModel createLocationViewModel)
	{
		InitializeComponent();
        _createLocationViewModel = createLocationViewModel;
        BindingContext = createLocationViewModel;

    }
}
