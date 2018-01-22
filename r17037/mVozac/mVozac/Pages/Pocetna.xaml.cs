using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using mVozac.ServiceReference2;
using mVozac.Pages;
using Windows.UI.Popups;

namespace mVozac
{
    public sealed partial class Pocetna : Page
    {
        public Pocetna()
        {
            this.InitializeComponent();

        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Korisnik s = (Korisnik)e.Parameter;
            TxtPrijavljeni.Text = s.KorisnickoIme;
            base.OnNavigatedTo(e);
        }

        private void BtnOdjava_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Prijava));
        }

        private void ProdajKartu_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProdajKartu), TxtPrijavljeni.Text);
        }

        private void PretraziKarte_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PretraziKarte), TxtPrijavljeni.Text);
        }

        private void RasporedVoznje_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RasporedVoznje), TxtPrijavljeni.Text);
        }

        private void PonistiKarte_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PonistiKarte), TxtPrijavljeni.Text);
        }

        private void Tahometar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Tahometar), TxtPrijavljeni.Text);
        }

        private void Statistika_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Statistika), TxtPrijavljeni.Text);
        }

        private async void btnPomoc_ClickAsync(object sender, RoutedEventArgs e)
        {
            var uriBing = new Uri(@"https://github.com/foivz/r17037/wiki/Korisni%C4%8Dka-dokumentacija");

            var success = await Windows.System.Launcher.LaunchUriAsync(uriBing);

            if (!success)
            {
                var dialog = new MessageDialog("Lokacija se ne može otvoriti u pregledniku!");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                await dialog.ShowAsync();
            }
        }
        //this.Frame.Navigate(typeof(Pomoc), null);
    }
}
