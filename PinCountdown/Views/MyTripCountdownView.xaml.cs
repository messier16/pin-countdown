using FFImageLoading.Forms;
using PinCountdown.ViewModels;
using PinCountdown.ViewModels.Base;
using Xamarin.Forms;

namespace PinCountdown.Views
{
    public partial class PinCountdownView : ContentPage
	{
        bool _isVisible = true;
        private double width;
        private double height;

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

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    this.HorizontalView.IsVisible = true;
                    this.VerticalView.IsVisible = false;
                }
                else
                {
                    this.HorizontalView.IsVisible = false;
                    this.VerticalView.IsVisible = true;
                }
            }
        }

        async void EditButton_Clicked(System.Object sender, System.EventArgs e)
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

        async void AboutButton_Clicked(System.Object sender, System.EventArgs e)
        {
                await Navigation.PushAsync(new AboutPage());
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            _isVisible = !_isVisible;
            EditButton.IsVisible = _isVisible;
            AboutButton.IsVisible = _isVisible;
        }
    }
}