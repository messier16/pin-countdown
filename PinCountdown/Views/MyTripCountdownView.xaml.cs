using FFImageLoading.Forms;
using PinCountdown.ViewModels;
using PinCountdown.ViewModels.Base;
using Xamarin.Forms;

namespace PinCountdown.Views
{
    public partial class PinCountdownView : ContentPage
	{
        bool _isVisible = true;
		public PinCountdownView ()
		{
			InitializeComponent ();

            BindingContext = App.vieModel;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as BaseViewModel;
            await vm?.LoadAsync();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var vm = BindingContext as BaseViewModel;
            await vm?.UnloadAsync();
        }

        async void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if(sender == EditButton)
            {
                var vm = BindingContext as PinCountdownViewModel;
                var editVm = new EditCountdownViewModel
                {
                    ImageBytes = vm.ImageBytes,
                    Name = vm.Name,
                    CreationDate = vm.Creation,
                    EndDate = vm.Date

                };
                await Navigation.PushAsync(new EditCountdownPage(editVm));
            }
            else
            {
                await Navigation.PushAsync(new AboutPage());
            }
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            _isVisible = !_isVisible;
            EditButton.IsVisible = _isVisible;
            AboutButton.IsVisible = _isVisible;
        }
    }
}