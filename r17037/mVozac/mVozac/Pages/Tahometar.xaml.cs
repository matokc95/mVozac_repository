using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI;
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
using mVozac.ServiceReference2;
using Windows.Storage.Streams;
using System.Collections.ObjectModel;

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
            base.OnNavigatedTo(e);
            TxtPrijavljeni.Text = e.Parameter.ToString();
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
                    //MapControl1.MapElements.Add(new MapIcon() {  })
                    var ikona = new MapIcon();
                    ikona.Title = "test";
                    ikona.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/arrow.png"));
                    ikona.Location = mojaLokacija;
                    MapControl1.MapElements.Add(ikona);


                    MapControl1.MapElementClick += MapControl1_MapElementClick;
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
        }

        private void MapControl1_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            //  args.
        }

        private async void btnRuta_ClickAsync(object sender, RoutedEventArgs e)
        {
            //odredivanje pocetne stanice
            Service1Client service = new Service1Client();
            var lokacijaPocetak = await service.DohvatiLokacijuAsync(txtPolaziste.Text);
            BasicGeoposition startLocation = new BasicGeoposition() { Latitude = lokacijaPocetak.Latitude, Longitude = lokacijaPocetak.Longitude };

            //odredivanje puta od trenutne lokacije do pocetne stanice
            Geolocator geolocator = new Geolocator();

            Geoposition pos = await geolocator.GetGeopositionAsync();
            Geopoint mojaLokacija = pos.Coordinate.Point;
            
            var ikona = new MapIcon();
            ikona.Title = "Moja lokacija";
            ikona.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/arrow.png"));
            ikona.Location = mojaLokacija;
            MapControl1.MapElements.Add(ikona);


            MapRouteFinderResult routeResult1 =
                  await MapRouteFinder.GetDrivingRouteAsync(
                  mojaLokacija,
                  new Geopoint(startLocation),
                  MapRouteOptimization.Time,
                  MapRouteRestrictions.None);

            if (routeResult1.Status == MapRouteFinderStatus.Success)
            {

                MapRouteView viewOfRoute = new MapRouteView(routeResult1.Route);
                viewOfRoute.RouteColor = Colors.Red;
                viewOfRoute.OutlineColor = Colors.Black;

                //dodavanje rute na mapcontrol
                MapControl1.Routes.Add(viewOfRoute);

                await MapControl1.TrySetViewBoundsAsync(
                      routeResult1.Route.BoundingBox,
                      null,
                      Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);

                //kreiranje navigacijskog teksta za korisnika
                System.Text.StringBuilder routeInfo = new System.Text.StringBuilder();

                routeInfo.Append("Trajanje putovanja u minutama = ");
                routeInfo.Append(routeResult1.Route.EstimatedDuration.TotalMinutes.ToString());
                routeInfo.Append("\nDužina rute u kilometrima = ");
                routeInfo.Append((routeResult1.Route.LengthInMeters / 1000).ToString());

                // Display the directions.
                routeInfo.Append("\n\nUPUTE:\n");

                foreach (MapRouteLeg leg in routeResult1.Route.Legs)
                {
                    foreach (MapRouteManeuver maneuver in leg.Maneuvers)
                    {
                        routeInfo.AppendLine(maneuver.InstructionText);
                    }
                }

                //prikaz informacije o ruti
                tbOutputText.Text = routeInfo.ToString();
            }
            else
            {
                tbOutputText.Text = "Došlo je do pogreške: " + routeResult1.Status.ToString();
            }
            //odredivanje medustanica
            ObservableCollection<Grad> listaGradova = await service.ListaMedustanicaAsync(TxtPrijavljeni.Text);
            if (listaGradova.Count > 1)
            {
                foreach (Grad grad in listaGradova)
                {
                    var ikonaMedustanice = new MapIcon();
                    BasicGeoposition meduGrad = new BasicGeoposition();
                    meduGrad.Latitude = grad.Latitude;
                    meduGrad.Longitude = grad.Longitude;
                    Geopoint lokacijaGrada = new Geopoint(meduGrad);
                    ikonaMedustanice.Location = lokacijaGrada;
                    ikonaMedustanice.Title = grad.NazivGrada;
                    MapControl1.MapElements.Add(ikonaMedustanice);
                }
            }
 
            //odredivanje zavrsne stanice
            var lokacijaZavrsetak = await service.DohvatiLokacijuAsync(txtOdrediste.Text);
            BasicGeoposition endLocation = new BasicGeoposition() { Latitude = lokacijaZavrsetak.Latitude, Longitude = lokacijaZavrsetak.Longitude };


            //dohvacanje rute izmedu pocetne i zavrsne lokacije
            MapRouteFinderResult routeResult =
                  await MapRouteFinder.GetDrivingRouteAsync(
                  mojaLokacija,
                  new Geopoint(endLocation),
                  MapRouteOptimization.Time,
                  MapRouteRestrictions.None);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {

                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Yellow;
                viewOfRoute.OutlineColor = Colors.Black;

                //dodavanje rute na mapcontrol
                MapControl1.Routes.Add(viewOfRoute);

                await MapControl1.TrySetViewBoundsAsync(
                      routeResult.Route.BoundingBox,
                      null,
                      Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);

                //kreiranje navigacijskog teksta za korisnika
                System.Text.StringBuilder routeInfo = new System.Text.StringBuilder();

                routeInfo.Append("Trajanje putovanja u minutama = ");
                routeInfo.Append(routeResult.Route.EstimatedDuration.TotalMinutes.ToString());
                routeInfo.Append("\nDužina rute u kilometrima = ");
                routeInfo.Append((routeResult.Route.LengthInMeters / 1000).ToString());

                // Display the directions.
                routeInfo.Append("\n\nUPUTE:\n");

                foreach (MapRouteLeg leg in routeResult.Route.Legs)
                {
                    foreach (MapRouteManeuver maneuver in leg.Maneuvers)
                    {
                        routeInfo.AppendLine(maneuver.InstructionText);
                    }
                }

                //prikaz informacije o ruti
                tbOutputText.Text = routeInfo.ToString();
            }
            else
            {
                tbOutputText.Text = "Došlo je do pogreške: " + routeResult.Status.ToString();
            }
        }
        /*
        private async void OnPositionChanged(Geolocator sender, PositionChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _rootPage.NotifyUser("Location updated.", NotifyType.StatusMessage);
                UpdateLocationData(e.Position);
            });
        }
        */
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Service1Client service = new Service1Client();
                var res = await service.SelectVoznjuAsync(TxtPrijavljeni.Text);
                string linija_naziv = res.NazivLinije;
                var res2 = await service.GetLinijaIDAsync(linija_naziv);
                int linija_id = int.Parse(res2.LinijaID);
                var res3 = await service.SelectStanicaIDPocetakAsync(linija_id);
                txtPolaziste.Text = res3.StanicaNaziv;
                var res4 = await service.SelectStanicaIDZavrsetakAsync(linija_id);
                txtOdrediste.Text = res4.StanicaNaziv;
            }
            catch (Exception ex)
            {
                var dialog2 = new MessageDialog("Korisnik nema definiranu vožnju!");
                dialog2.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                await dialog2.ShowAsync();
                this.Frame.GoBack();
            }



        }
    }
}
