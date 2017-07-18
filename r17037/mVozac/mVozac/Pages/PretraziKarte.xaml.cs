using mVozac.ServiceReference2;
using System;
using System.Collections.Generic;
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
    public sealed partial class PretraziKarte : Page
    {
        public PretraziKarte()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            TxtPrijavljeni.Text = e.Parameter.ToString();
        }
        private void BtnPovratak_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private async void btnFind_Click(object sender, RoutedEventArgs e)
        {
            if (txtBrojKarte.Text.Length == 0)
            {
                var dialog = new MessageDialog("Niste unijeli broj karte!");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                await dialog.ShowAsync();
            }
            else
            {
                Service1Client serviceKarta = new Service1Client();
                var res = await serviceKarta.FindKartaAsync(int.Parse(txtBrojKarte.Text));

                if (res.KartaID == 0)
                {
                    var dialog = new MessageDialog("Karta sa unešenim brojem ne postoji!");
                    dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                    await dialog.ShowAsync();

                    txtPopust.Text = "";
                    txtVozac.Text = "";
                    txtLinija.Text = "";
                    txtPrice.Text = "";
                }
                else
                {
                    float price = res.CijenaVoznje;

                    float ukupnaCijena = price - (price * (res.KolicinaPopusta / 100));

                    txtPopust.Text = res.Popust;
                    txtVozac.Text = res.Vozac;
                    txtLinija.Text = res.Linija;
                    txtPrice.Text = ukupnaCijena.ToString();
                }
            }
        }
    }
}
