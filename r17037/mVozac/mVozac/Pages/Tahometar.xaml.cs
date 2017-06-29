using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
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
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            /* base.OnNavigatedTo(e);
             TxtPrijavljeni.Text = e.Parameter.ToString();*/

            // Set your current location.
            

        }
        private void BtnPovratak_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private async void btnLokacija_Click(object sender, RoutedEventArgs e)
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    //Dohvati lokaciju
                    Geolocator geolocator = new Geolocator();
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    Geopoint mojaLokacija = pos.Coordinate.Point;

                    //Prikazi lokaciju na karti
                    MapControl1.Center = mojaLokacija;
                    MapControl1.ZoomLevel = 12;
                    MapControl1.LandmarksVisible = true;
                    break;

                case GeolocationAccessStatus.Denied:
                    var dialog = new MessageDialog("Lokaciju nije moguće dohvatiti!");
                    dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                    await dialog.ShowAsync();
                    break;

                case GeolocationAccessStatus.Unspecified:
                    var dialog2 = new MessageDialog("Error!");
                    dialog2.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                    await dialog2.ShowAsync();
                    break;
            }
        }//dodaj po potrebi
    }
}
