using Compass.ViewModels;

namespace Compass.Views.Pages;

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
        //MessagingCenter.Unsubscribe<CompassViewModel>(this, "refresh");
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        //MessagingCenter.Subscribe<CompassViewModel>(this, "refresh", x => drawLocations());

        await _viewModel.OnNavigatedTo();
        base.OnNavigatedTo(args);
        drawLocations();
    }


    private void drawLocations()
    {
        compassGrid.Children.Clear();

        double rayon = 136; // Définissez le rayon du cercle
        double centreX = compassGrid.Width / 2;
        double centreY = compassGrid.Height / 2;

        foreach (var location in _viewModel.Locations)
        {
            // Convertir les degrés en radians
            double angleRadians = (location.Angle - 90) * Math.PI / 180;

            // Calculer les coordonnées X et Y de l'objet
            double x = centreX + rayon * Math.Cos(angleRadians);
            double y = centreY + rayon * Math.Sin(angleRadians);

            // Créer un Label pour afficher le nom de l'objet

            var frame = new Frame
            {
                HeightRequest = 10,
                WidthRequest = 10,
                CornerRadius = 5,
                BackgroundColor = location.Color,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                TranslationX = x,
                TranslationY = y,
                HasShadow = false
            };

            // Ajouter le label au Grid
            compassGrid.Children.Add(frame);
        }
    }
}


