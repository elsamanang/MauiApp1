using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    [QueryProperty(nameof(Monkey), "Monkey")]
    public partial class DetailsPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Monkey monkey;

        IMap map;

        public DetailsPageViewModel(IMap _map)
        {
            this.map = _map;
        }

        [RelayCommand]
        async Task OpenMap()
        {
            try
            {
                await map.OpenAsync(Monkey.Latitude, Monkey.Longitude, new MapLaunchOptions
                {
                    Name = Monkey.Name,
                    NavigationMode = NavigationMode.None,
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to launch map: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error, no map !", ex.Message, "Ok");
            }
        }
    }
}
