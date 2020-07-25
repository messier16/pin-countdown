using System;
using System.Collections.Generic;
using PinCountdown.ViewModels;
using Plugin.Media;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PinCountdown.Views
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

            var status = await Permissions.RequestAsync<Permissions.Photos>();

            if(status == PermissionStatus.Granted)
            {
                var mediaOptions = new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                };

                var file = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                if (file != null)
                {
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
}
