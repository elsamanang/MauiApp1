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
        public Command GetMonkeyCommand { get; }

        public MonkeyViewModel(MonkeyService monkeyService)
        {
            Title = "Monkey list";
            _monkeyService = monkeyService;
            GetMonkeyCommand = new Command(async () => await GetMonkeyAsync());
        }

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
    }
}
