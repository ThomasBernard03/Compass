using System;
using Compass.Models.Entities;
using Compass.Repositories.Interfaces;
using Compass.Services.Interfaces;

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
    }

    public override Task InitializeAsync(object parameters)
    {
        throw new NotImplementedException();
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
                Latitude = Latitude
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

    #endregion

}

