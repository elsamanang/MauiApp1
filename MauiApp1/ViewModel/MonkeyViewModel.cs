using CommunityToolkit.Mvvm.Input;
using Java.Util;
using MauiApp1.Models;
using MauiApp1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public partial class MonkeyViewModel : BaseViewModel
    {
        public ObservableCollection<Monkey> Monkeys { get; } = new();
        MonkeyService _monkeyService;
        IGeolocation geolocation;

        public MonkeyViewModel(MonkeyService monkeyService, IGeolocation geolocation)
        {
            Title = "Monkey list";
            _monkeyService = monkeyService;
            this.geolocation = geolocation;
        }

        [RelayCommand]
        public async Task GetMonkeyAsync()
        {
            if(IsBusy) return;

            try
            {
                IsBusy = true;

                var monkeys = await _monkeyService.GetMonkeysAsync();

                if(monkeys.Count != 0) Monkeys.Clear(); 

                foreach(var monkey in monkeys)
                {
                    Monkeys.Add(monkey);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Something wrong : {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GetClosestMonkey()
        {
            if (IsBusy || Monkeys.Count == 0) return;

            try
            {
                var location = await geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                var first = Monkeys.OrderBy(m => location.CalculateDistance(
                     new Location(m.Latitude, m.Longitude), DistanceUnits.Miles))
                    .FirstOrDefault();

                await Application.Current.MainPage.DisplayAlert(" ", first.Name + " " + first.Location, "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to query location: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

    }
}
