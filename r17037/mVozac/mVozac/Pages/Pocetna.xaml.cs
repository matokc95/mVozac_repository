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

namespace mVozac
{
    public sealed partial class Pocetna : Page
    {
        public Pocetna()
        {
            this.InitializeComponent();
  
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Korisnik s = (Korisnik)e.Parameter;
            TxtPrijavljeni.Text = s.KorisnickoIme;
        }

        private void BtnOdjava_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void ProdajKartu_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProdajKartu));
        }

        private void PretraziKarte_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PretraziKarte));
        }

        private void RasporedVoznje_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RasporedVoznje));
        }

        private void PonistiKarte_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PonistiKarte));
        }

        private void Tahometar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Tahometar));
        }

        private void Statistika_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Statistika));
        }
    }
}
