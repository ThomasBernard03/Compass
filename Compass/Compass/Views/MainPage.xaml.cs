using Compass.ViewModels;

namespace Compass;

public partial class MainPage : ContentPage
{
    private readonly CompassViewModel _viewModel;

    public MainPage(CompassViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}


