using System;
using System.Collections.Generic;
using MyTripCountdown.ViewModels;
using Plugin.Media;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyTripCountdown.Views
{
    public partial class EditCountdownPage : ContentPage
    {
        public EditCountdownPage(EditCountdownViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }

        public EditCountdownViewModel ViewModel => BindingContext as EditCountdownViewModel;

        async void ImageCell_Tapped(System.Object sender, System.EventArgs e)
        {

            var status = await Permissions.RequestAsync<Permissions.Media>();

            if(status == PermissionStatus.Granted)
            {
                // Supply media options for saving our photo after it's taken.
                var mediaOptions = new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                };

                // Take a photo of the business receipt.
                var file = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                var vm = BindingContext as EditCountdownViewModel;

                byte[] bytes = null;
                var stream = file.GetStream();
                bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                ViewModel.ImageBytes = bytes;
            }
        }
    }
}
