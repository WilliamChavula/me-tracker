using System;
using System.Collections.Generic;
using MeTracker.Repository;
using MeTracker.Services;
using MeTracker.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MeTracker.Views
{
    public partial class MainView : ContentPage
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync();
                }

                Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(5)));

            });
        }
    }
}
