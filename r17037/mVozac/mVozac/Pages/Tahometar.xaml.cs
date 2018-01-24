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
        Geoposition pozicija;
        Geopoint mojaLok;
        private MapIcon ikona;
        Service1Client service;

        public Tahometar()
        {
            this.InitializeComponent();
            service = new Service1Client();
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

        private async void btnRuta_ClickAsync(object sender, RoutedEventArgs e)
        {
            //odredivanje polazišta
            var lokacijaPocetak = await service.DohvatiLokacijuAsync(txtPolaziste.Text);
            BasicGeoposition startLocation = new BasicGeoposition() { Latitude = lokacijaPocetak.Latitude, Longitude = lokacijaPocetak.Longitude };

            //odredivanje puta od trenutne lokacije do polazišta
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracy = PositionAccuracy.High;
            geolocator.PositionChanged += OnPositionChanged;

            try
            {
                pozicija = await geolocator.GetGeopositionAsync();

                ikona = new MapIcon();
                ikona.Title = "Moja lokacija";
                ikona.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/arrow.png"));
                ikona.Location = mojaLok;
                MapControl1.MapElements.Add(ikona);

                MapRouteFinderResult routeResult1 =
                      await MapRouteFinder.GetDrivingRouteAsync(
                      mojaLok,
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
                }
                else
                {
                    var dialog = new MessageDialog("Došlo je do pogreške!");
                    dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                    await dialog.ShowAsync();
                }

                //odredivanje medustanica
                ObservableCollection<Grad> listaGradova = await service.ListaMedustanicaAsync(TxtPrijavljeni.Text);
                if (listaGradova[0].NazivGrada != null)
                {
                    foreach (Grad grad in listaGradova)
                    {
                        var ikoneMedustanica = new MapIcon();
                        BasicGeoposition meduGrad = new BasicGeoposition();
                        meduGrad.Latitude = grad.Latitude;
                        meduGrad.Longitude = grad.Longitude;
                        Geopoint lokacijaGrada = new Geopoint(meduGrad);
                        ikoneMedustanica.Location = lokacijaGrada;
                        ikoneMedustanica.Title = grad.NazivGrada;
                        MapControl1.MapElements.Add(ikoneMedustanica);
                    }
                }

                //odredivanje odredišta
                var lokacijaZavrsetak = await service.DohvatiLokacijuAsync(txtOdrediste.Text);
                BasicGeoposition endLocation = new BasicGeoposition() { Latitude = lokacijaZavrsetak.Latitude, Longitude = lokacijaZavrsetak.Longitude };

                //dohvacanje rute izmedu pocetne i zavrsne lokacije
                MapRouteFinderResult routeResult =
                      await MapRouteFinder.GetDrivingRouteAsync(
                      mojaLok,
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
                }
                else
                {
                    var dialog = new MessageDialog("Došlo je do pogreške!!");
                    dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                    await dialog.ShowAsync();
                }
            }
            catch
            {
                var dialog = new MessageDialog("GPS na uređaju nije uključen!!!");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                await dialog.ShowAsync();
            }
            

            
        }

        private async void OnPositionChanged(Geolocator sender, PositionChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                mojaLok = e.Position.Coordinate.Point;
                try
                {
                    ikona.Location = mojaLok;
                }
                catch
                {
                    var dialog = new MessageDialog("Nema GPS signala!!!");
                    dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                }
                
            });
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var resNaziv = await service.SelectVoznjuAsync(TxtPrijavljeni.Text);
                string nazivLinije = resNaziv.NazivLinije;
                var resLinija = await service.GetLinijaIDAsync(nazivLinije);
                int idLinije = int.Parse(resLinija.LinijaID);
                var resStanicaPocetak = await service.SelectStanicaIDPocetakAsync(idLinije);
                txtPolaziste.Text = resStanicaPocetak.StanicaNaziv;
                var resStanicaZavrsetak = await service.SelectStanicaIDZavrsetakAsync(idLinije);
                txtOdrediste.Text = resStanicaZavrsetak.StanicaNaziv;
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog("Korisnik nema definiranu vožnju!");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                await dialog.ShowAsync();

                this.Frame.GoBack();
            }
        }
    }
}