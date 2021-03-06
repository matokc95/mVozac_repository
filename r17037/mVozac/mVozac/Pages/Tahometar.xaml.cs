﻿using System;
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


namespace mVozac.Pages
{

    public sealed partial class Tahometar : Page
    {
        private Geoposition pozicija;
        private Geopoint mojaLok;
        private MapIcon ikona;
        private Service1Client service;

        public Tahometar()
        {
            this.InitializeComponent();
            //sakrivanje legende
            crvena.Visibility = Visibility.Collapsed;
            txtLegendaCrvena.Visibility = Visibility.Collapsed;
            zuta.Visibility = Visibility.Collapsed;
            txtLegendaZuta.Visibility = Visibility.Collapsed;

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
            
            //azuriranje trenutne lokacije korisnika
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

                //odredivanje puta od trenutne lokacije do polazišta
                PrintRoute(mojaLok, startLocation, Colors.Red);

                //odredivanje medustanica
                ObservableCollection<Grad> listaMedustanica = await service.ListaMedustanicaAsync(TxtPrijavljeni.Text);
                if (listaMedustanica[0].NazivGrada != null)
                {
                    foreach (Grad grad in listaMedustanica)
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
                PrintRoute(new Geopoint(startLocation),endLocation,Colors.Yellow);

                //dodavanje legende
                crvena.Visibility = Visibility.Visible;
                txtLegendaCrvena.Visibility = Visibility.Visible;
                txtLegendaCrvena.FontSize = 9;
                txtLegendaCrvena.Text = "Putanja od trenutne lokacije do polazišta";
                zuta.Visibility = Visibility.Visible;
                txtLegendaZuta.Visibility = Visibility.Visible;
                txtLegendaZuta.FontSize = 9;
                txtLegendaZuta.Text = "Putanja od polazišta do odredišta";
            }
            catch
            {
                var dialog = new MessageDialog("GPS na uređaju nije uključen!!!");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                await dialog.ShowAsync();
            }
        }
        private async void PrintRoute(Geopoint start, BasicGeoposition end, Color routeColor)
        {
            try
            {
                MapRouteFinderResult routeResult =
                      await MapRouteFinder.GetDrivingRouteAsync(
                      start,
                      new Geopoint(end),
                      MapRouteOptimization.Time,
                      MapRouteRestrictions.None);

                if (routeResult.Status == MapRouteFinderStatus.Success)
                {
                    MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                    viewOfRoute.RouteColor = routeColor;
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
                var dialog = new MessageDialog("Došlo je do pogreške u određivanju rute!!");
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

        private async void Page_Loading(FrameworkElement sender, object args)
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