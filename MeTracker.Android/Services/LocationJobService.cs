using System;
using Android.App;
using Android.App.Job;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using MeTracker.Repository;

namespace MeTracker.Droid.Services
{
    [Service(Name = "MeTracker.Droid.Services.LocationJobService", Permission = "android.permission.BIND_JOB_SERVICE")]
    public class LocationJobService : JobService, ILocationListener
    {
        private readonly ILocationReposiroty locationRepo;
        private static LocationManager locationManager;

        public LocationJobService()
        {
            locationRepo = Resolver.Resolve<ILocationReposiroty>();
        }

        public void OnLocationChanged(Location location)
        {
            var newLocation = new Models.Location(location.Latitude, location.Longitude);
            locationRepo.Save(newLocation);
        }

        public void OnProviderDisabled(string provider)
        {

        }

        public void OnProviderEnabled(string provider)
        {

        }

        public override bool OnStartJob(JobParameters @params)
        {
            locationManager = (LocationManager)ApplicationContext.GetSystemService(LocationService);
            locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 1000L, 0.1F, this);

            return true;
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {

        }

        public override bool OnStopJob(JobParameters @params)
        {
            return true;
        }
    }
}
