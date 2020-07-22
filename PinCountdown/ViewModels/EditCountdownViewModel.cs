using System;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Akavache;
using PinCountdown.Models;
using PinCountdown.ViewModels.Base;
using Xamarin.Forms;

namespace PinCountdown.ViewModels
{
    public class EditCountdownViewModel : BaseViewModel
    {
        private DateTime endDate;
        private DateTime creationDate;
        private string name;
        private ImageSource _image;
        private byte[] _imageBytes;

        public EditCountdownViewModel()
        {
        }

        public DateTime EndDate
        {
            get => endDate;
            set => SetProperty(ref endDate, value);
        }

        public DateTime CreationDate
        {
            get => creationDate;
            set => SetProperty(ref creationDate, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public byte[] ImageBytes
        {
            get { return _imageBytes; }
            set
            {
                SetProperty(ref _imageBytes, value);
                var stream = new MemoryStream(value);
                this.Image = ImageSource.FromStream(() => stream);
            }
        }

        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public ICommand Save => new Command(async () => await SaveCountdown());

        private async Task SaveCountdown()
        {
            var countdownDate = new CountdownDate
            {
                Name = this.Name,
                EndDate = this.EndDate,
                CreationDate = this.CreationDate,
            };
            await BlobCache.UserAccount.Insert(Keys.Image, this.ImageBytes);
            await BlobCache.UserAccount.InsertObject(Keys.CountdownDate, countdownDate);
        }
    }
}
