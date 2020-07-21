﻿using FFImageLoading.Forms;
using MyTripCountdown.ViewModels;
using MyTripCountdown.ViewModels.Base;
using Xamarin.Forms;

namespace MyTripCountdown.Views
{
    public partial class MyTripCountdownView : ContentPage
	{
		public MyTripCountdownView ()
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
            var vm = BindingContext as MyTripCountdownViewModel;
            var editVm = new EditCountdownViewModel
            {
                ImageBytes = vm.ImageBytes,
                Name = vm.Name,
                CreationDate = vm.Creation,
                EndDate = vm.Date

            };
            await Navigation.PushAsync(new EditCountdownPage(editVm));
        }
    }
}