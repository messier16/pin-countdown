using System;
using Akavache;
using MyTripCountdown.ViewModels;
using MyTripCountdown.Views;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reactive.Linq;
using MyTripCountdown.Models;
using System.Collections.Generic;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyTripCountdown
{
    public partial class App : Application
    {



        public static MyTripCountdownViewModel vieModel;
        public App()
        {
            InitializeComponent();
            Akavache.Registrations.Start("Countdown");
            App.vieModel = new MyTripCountdownViewModel();

            MainPage = new CustomNavigationPage(new MyTripCountdownView());
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