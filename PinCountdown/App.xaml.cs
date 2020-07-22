using System;
using Akavache;
using PinCountdown.ViewModels;
using PinCountdown.Views;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reactive.Linq;
using PinCountdown.Models;
using System.Collections.Generic;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PinCountdown
{
    public partial class App : Application
    {



        public static PinCountdownViewModel vieModel;
        public App()
        {
            InitializeComponent();
            Akavache.Registrations.Start("Countdown");
            App.vieModel = new PinCountdownViewModel();

            MainPage = new CustomNavigationPage(new PinCountdownView());
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await CrossMedia.Current.Initialize();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }
    }
}