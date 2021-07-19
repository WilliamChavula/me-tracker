using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeTracker.Repository;
using MeTracker.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MeTracker.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly ILocationReposiroty _locationRepository;
        private readonly ILocationTrackingService _locationTrackingService;
        private List<Models.Point> points;

        public List<Models.Point> Points
        {
            get => points;
            set
            {
                points = value;
                RaisePropertyChanged(nameof(Points));
            }
        }

        public MainViewModel(ILocationReposiroty locationReposiroty, ILocationTrackingService locationTrackingService)
        {
            _locationRepository = locationReposiroty;
            _locationTrackingService = locationTrackingService;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                _locationTrackingService.StartTracking();
                await LoadData();
            });
        }

        private async Task LoadData()
        {
            var locations = await _locationRepository.GetAll();
            var pointList = new List<Models.Point>();

            foreach (var location in locations)
            {
                if (!pointList.Any())
                {
                    pointList.Add(new Models.Point
                    {
                        Location = location
                    });
                    continue;
                }

                var pointFound = false;

                // try to find a point for the current location
                foreach (var point in pointList)
                {
                    var distance = Location.CalculateDistance(
                        new Location(point.Location.Latitude, point.Location.Longitude),
                        new Location(location.Latitude, location.Longitude), DistanceUnits.Kilometers);

                    if (distance < 0.2)
                    {
                        pointFound = true;
                        point.Count++;
                        break;
                    }
                }

                // if no point is found, add a new point to the list of points
                if (!pointFound)
                {
                    pointList.Add(new Models.Point
                    {
                        Location = location
                    });
                }

                if (pointList == null || !pointList.Any())
                    return;

                var pointMax = pointList.Select(x => x.Count).Max();
                var pointMin = pointList.Select(x => x.Count).Min();
                var diff = (float)(pointMax - pointMin);

                foreach (var point in pointList)
                {
                    var heat = (2f / 3f) - ((float)point.Count / diff);
                    point.Heat = Color.FromHsla(heat, 1, 0.5);
                }

            }

            Points = pointList;

        }
    }
}
