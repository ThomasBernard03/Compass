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

    protected async override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        await _viewModel.OnNavigatedFrom(args);
        base.OnNavigatedFrom(args);
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await _viewModel.OnNavigatedTo(args);
        base.OnNavigatedTo(args);
    }
}


