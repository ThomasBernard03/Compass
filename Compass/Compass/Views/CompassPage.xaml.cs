using Compass.ViewModels;

namespace Compass.Views;

public partial class CompassPage : ContentPage
{
    private readonly CompassViewModel _viewModel;

    public CompassPage(CompassViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}


