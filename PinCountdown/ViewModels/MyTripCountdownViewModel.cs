using Akavache;
using PinCountdown.Models;
using PinCountdown.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PinCountdown.ViewModels
{
    public class PinCountdownViewModel : BaseViewModel
    {
        private DateTime _date;
        private DateTime _creation;
        private Countdown _countdown;
        private int _days;
        private string _name;
        private ImageSource _image;
        private byte[] _imageBytes;
        private int _hours;
        private int _minutes;
        private double _progress;

        public PinCountdownViewModel()
        {
            _countdown = new Countdown();
        }


        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public DateTime Creation
        {
            get => _creation;
            set => SetProperty(ref _creation, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public byte[] ImageBytes
        {
            get { return _imageBytes; }
            set {
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

        public int Days
        {
            get => _days;
            set => SetProperty(ref _days, value);
        }

        public int Hours
        {
            get => _hours;
            set => SetProperty(ref _hours, value);
        }

        public int Minutes
        {
            get => _minutes;
            set => SetProperty(ref _minutes, value);
        }

        public double Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        public ICommand RestartCommand => new Command(Restart);

        public override async Task LoadAsync()
        {
            CountdownDate countdownDate;
            try
            {
                countdownDate = await BlobCache.UserAccount.GetObject<CountdownDate>(Keys.CountdownDate);

            }
            catch (KeyNotFoundException)
            {
                countdownDate = new CountdownDate
                {
                    Name = "Future event",
                    CreationDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7)
                };
            }


            this.Date = countdownDate.EndDate;
            this.Creation = countdownDate.CreationDate;
            this.Name = countdownDate.Name;
            
            try
            {
                this.ImageBytes = await BlobCache.UserAccount.Get(Keys.Image);
            }
            catch (KeyNotFoundException)
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                using (Stream resFilestream = assembly.GetManifestResourceStream("PinCountdown.sunset.jpg"))
                {
                    byte[] ba = new byte[resFilestream.Length];
                    resFilestream.Read(ba, 0, ba.Length);
                    this.ImageBytes = ba;
                }
            }

            _countdown.EndDate = Date;
            _countdown.Start();

            _countdown.Ticked += OnCountdownTicked;
            _countdown.Completed += OnCountdownCompleted;

            await base.LoadAsync();
        }

        public override Task UnloadAsync()
        {
            _countdown.Ticked -= OnCountdownTicked;
            _countdown.Completed -= OnCountdownCompleted;

            return base.UnloadAsync();
        }

        void OnCountdownTicked()
        {
            Days = _countdown.RemainTime.Days;
            Hours = _countdown.RemainTime.Hours;
            Minutes = _countdown.RemainTime.Minutes;

            var totalSeconds = (Date - Creation).TotalSeconds;
            var remainSeconds = _countdown.RemainTime.TotalSeconds;
            Progress = remainSeconds / totalSeconds;
        }

        void OnCountdownCompleted()
        {
            Days = 0;
            Hours = 0;
            Minutes = 0;

            Progress = 0;
        }


        void Restart()
        {
            Debug.WriteLine("Restart");
        }
    }
}