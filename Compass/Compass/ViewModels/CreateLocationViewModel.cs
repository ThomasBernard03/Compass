using System;
using Compass.Models.Entities;
using Compass.Repositories.Interfaces;
using Compass.Services.Interfaces;
using Compass.Views.Pages;

namespace Compass.ViewModels;

public class CreateLocationViewModel : BaseViewModel
{
    private readonly IGpsService _gpsService;
    private readonly IRepository<LocationEntity> _locationRepository;

    public CreateLocationViewModel(INavigationService navigationService, IGpsService gpsService, IRepository<LocationEntity> locationRepository) : base(navigationService)
	{
        _gpsService = gpsService;
        _locationRepository = locationRepository;

        GetLocationCommand = new Command(async x => await OnGetLocationCommand());
        TakePictureCommand = new Command(async x => await OnTakePictureCommand());
        CreateLocationCommand = new Command(async x => await OnCreateLocationCommand());
        OpenMapCommand = new Command(async x => await OnOpenMapCommand());

        var colors = new List<Color>()
        {
            Microsoft.Maui.Graphics.Colors.Red,
            Microsoft.Maui.Graphics.Colors.Blue,
            Microsoft.Maui.Graphics.Colors.Green,
            Microsoft.Maui.Graphics.Colors.Yellow,
            Microsoft.Maui.Graphics.Colors.Orange,
            Microsoft.Maui.Graphics.Colors.Purple,
        };

        Colors = colors;
    }


    #region Commands & Methods

    #region GetLocationCommand => OnGetLocationCommand
    public Command GetLocationCommand { get; private set; }
    private async Task OnGetLocationCommand()
    {
        var location = await _gpsService.GetLocationAsync();

        Latitude = location.Latitude;
        Longitude = location.Longitude;
    }
    #endregion


    #region OpenMapCommand => OnOpenMapCommand
    public Command OpenMapCommand { get; private set; }
    private async Task OnOpenMapCommand()
    {
        await NavigationService.NavigateToModalAsync<MapViewModel>();
    }
    #endregion

    #region TakePictureCommand => OnTakePictureCommand
    public Command TakePictureCommand { get; private set; }
    private async Task OnTakePictureCommand()
    {
        try
        {
            var mediaPickerOptions = new MediaPickerOptions
            {
                Title = "Prendre une photo"
            };

            var photo = await MediaPicker.PickPhotoAsync(mediaPickerOptions);

            if (photo != null)
            {
                // Utilisez 'photo.FullPath' pour accéder au chemin du fichier de la photo

                Picture = ImageSource.FromFile(photo.FullPath);
            }
        }
        catch(Exception e)
        {

        }

    }
    #endregion

    #region CreateLocationCommand => OnCreateLocationCommand
    public Command CreateLocationCommand { get; private set; }
    private async Task OnCreateLocationCommand()
    {
        try
        {

            var location = new LocationEntity()
            {
                Name = Name,
                Longitude = Longitude,
                Latitude = Latitude,
                Color = Color.ToHex()

            };
            _locationRepository.Insert(location);


            await NavigationService.CloseModalAsync();

        }
        catch (Exception e)
        {

        }

    }
    #endregion

    #endregion

    #region Properties

    #region Latitude
    private double _latitude;
    public double Latitude
    {
        get => _latitude;
        set
        {
            _latitude = value;
            OnPropertyChanged(nameof(Latitude));
        }
    }
    #endregion

    #region Longitude
    private double _longitude;
    public double Longitude
    {
        get => _longitude;
        set
        {
            _longitude = value;
            OnPropertyChanged(nameof(Longitude));
        }
    }
    #endregion

    #region Picture
    private ImageSource _picture;
    public ImageSource Picture
    {
        get => _picture;
        set
        {
            _picture = value;
            OnPropertyChanged(nameof(Picture));
        }
    }
    #endregion

    #region Name
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
    #endregion

    #region Color
    private Color _color;
    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            OnPropertyChanged(nameof(Color));
        }
    }
    #endregion

    #region Colors
    private IEnumerable<Color> _colors;
    public IEnumerable<Color> Colors
    {
        get => _colors;
        set
        {
            _colors = value;
            OnPropertyChanged(nameof(Colors));
        }
    }
    #endregion

    #endregion

}

