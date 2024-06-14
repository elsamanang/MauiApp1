using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.ViewModel;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MonkeyViewModel monkeyViewModel)
        {
            InitializeComponent();
            BindingContext = monkeyViewModel;
        }

        private async void TapeGestureRecognizer_Taped(object sender, EventArgs e)
        {
            var monkey = ((VisualElement)sender).BindingContext as Monkey;

            if (monkey == null) return;

            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object> {
                { "Monkey", monkey },
            });
        }
    }

}
