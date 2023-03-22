using System;
using Compass.Services.Interfaces;

namespace Compass.ViewModels;

public class CreateLocationViewModel : BaseViewModel
{
    public CreateLocationViewModel(INavigationService navigationService) : base(navigationService)
	{
	}

    public override Task InitializeAsync(object parameters)
    {
        throw new NotImplementedException();
    }
}

