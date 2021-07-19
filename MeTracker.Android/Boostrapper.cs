using System;
using Autofac;
using MeTracker.Droid.Services;
using MeTracker.Services;

namespace MeTracker.Droid
{
    public class Boostrapper : MeTracker.BootStrapper
    {
        public Boostrapper()
        {
        }

        public static void Init()
        {
            var instance = new Boostrapper();

        }

        protected override void Initialize()
        {
            base.Initialize();

            ContainerBuilder.RegisterType<LocationTrackingService>().As<ILocationTrackingService>().SingleInstance();
        }
    }
}
