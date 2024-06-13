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
    }

}
