using mVozac.ServiceReference2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
    public sealed partial class Statistika : Page
    {
        public Statistika()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            TxtPrijavljeni.Text = e.Parameter.ToString();
            DohvatiKarte();
        }

        private async void DohvatiKarte()
        {
            Service1Client servis = new Service1Client();
            var comboKarte = await servis.PonisteneKarteAsync();
            cmbKarte.ItemsSource = comboKarte;
        }

        private void BtnPovratak_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private async void btnPovrat_Click(object sender, RoutedEventArgs e)
        {
            if (cmbKarte.Items.Count != 0)
            {
                Service1Client activate = new Service1Client();
                await activate.AktivirajKartuAsync(int.Parse(cmbKarte.SelectedItem.ToString()));

                var dialog = new MessageDialog("Karta uspješno aktivirana!");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                await dialog.ShowAsync();

                DohvatiKarte();
            }
            else
            {
                var dialog = new MessageDialog("Nema poništenih karata.");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                await dialog.ShowAsync();
            }
        }
    }
}