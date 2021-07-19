using System;
using MeTracker.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = Resolver.Resolve<MainView>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            MainPage = Resolver.Resolve<MainView>();
        }
    }
}
