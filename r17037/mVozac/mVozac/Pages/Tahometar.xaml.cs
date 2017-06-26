using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace mVozac.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Tahometar : Page
    {

        public Tahometar()
        {
            this.InitializeComponent();

            MapControl MapControl2 = new MapControl();
            MapControl2.ZoomInteractionMode = MapInteractionMode.GestureAndControl;
            MapControl2.TiltInteractionMode = MapInteractionMode.GestureAndControl;
            MapControl2.MapServiceToken = "NUDduMOOVmZwz1cjsgca~ePRq5HHEc8Eu-ODahG9JOA~Agf7dNjHmoa9EnFTRdMWbAGtRDl0_FMmMKmQHW3eZU_2Qzl6zurdWXrEw4U57MCp";
            pageGrid.Children.Add(MapControl2);
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           /* base.OnNavigatedTo(e);
            TxtPrijavljeni.Text = e.Parameter.ToString();*/
            // Specify a known location.
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 47.604, Longitude = -122.329 };
            Geopoint cityCenter = new Geopoint(cityPosition);

            // Set the map location.
            MapControl1.Center = cityCenter;
            MapControl1.ZoomLevel = 12;
            MapControl1.LandmarksVisible = true;

        }
        private void BtnPovratak_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
        async private void OnStatusChanged(Geolocator sender, StatusChangedEventArgs e)
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    // Get the current location.
                    Geolocator geolocator = new Geolocator();
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    Geopoint myLocation = pos.Coordinate.Point;

                    // Set the map location.
                    MapControl1.Center = myLocation;
                    MapControl1.ZoomLevel = 12;
                    MapControl1.LandmarksVisible = true;
                    break;

                case GeolocationAccessStatus.Denied:
                    // Handle the case  if access to location is denied.
                    break;

                case GeolocationAccessStatus.Unspecified:
                    // Handle the case if  an unspecified error occurs.
                    break;
            }
        }
        private async void showStreetsideView()
        {
            // Check if Streetside is supported.
            if (MapControl1.IsStreetsideSupported)
            {
                // Find a panorama near Avenue Gustave Eiffel.
                BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 48.858, Longitude = 2.295 };
                Geopoint cityCenter = new Geopoint(cityPosition);
                StreetsidePanorama panoramaNearCity = await StreetsidePanorama.FindNearbyAsync(cityCenter);

                // Set the Streetside view if a panorama exists.
                if (panoramaNearCity != null)
                {
                    // Create the Streetside view.
                    StreetsideExperience ssView = new StreetsideExperience(panoramaNearCity);
                    ssView.OverviewMapVisible = true;
                    MapControl1.CustomExperience = ssView;
                }
            }
            else
            {
                // If Streetside is not supported
                ContentDialog viewNotSupportedDialog = new ContentDialog()
                {
                    Title = "Streetside is not supported",
                    Content = "\nStreetside views are not supported on this device.",
                    PrimaryButtonText = "OK"
                };
                await viewNotSupportedDialog.ShowAsync();
            }
        }
        private async void display3DLocation()
        {
            if (MapControl1.Is3DSupported)
            {
                // Set the aerial 3D view.
                MapControl1.Style = MapStyle.Aerial3DWithRoads;

                // Specify the location.
                BasicGeoposition hwGeoposition = new BasicGeoposition() { Latitude = 43.773251, Longitude = 11.255474 };
                Geopoint hwPoint = new Geopoint(hwGeoposition);

                // Create the map scene.
                MapScene hwScene = MapScene.CreateFromLocationAndRadius(hwPoint,
                                                                                     80, /* show this many meters around */
                                                                                     0, /* looking at it to the North*/
                                                                                     60 /* degrees pitch */);
                // Set the 3D view with animation.
                await MapControl1.TrySetSceneAsync(hwScene, MapAnimationKind.Bow);
            }
            else
            {
                // If 3D views are not supported, display dialog.
                ContentDialog viewNotSupportedDialog = new ContentDialog()
                {
                    Title = "3D is not supported",
                    Content = "\n3D views are not supported on this device.",
                    PrimaryButtonText = "OK"
                };
                await viewNotSupportedDialog.ShowAsync();
            }
        }

    }
}
