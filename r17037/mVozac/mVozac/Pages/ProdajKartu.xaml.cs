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
    public sealed partial class ProdajKartu : Page
    {
        public ProdajKartu()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            TxtPrijavljeni.Text = e.Parameter.ToString();
            DohvatiPopuste();
            DohvatiVoznje();
        }

        private async void DohvatiPopuste()
        {
            Service1Client combo = new Service1Client();
            var comboPopust = await combo.PopustiComboAsync();
            ime.ItemsSource = comboPopust;
        }

        private async void DohvatiVoznje()
        {
            Service1Client vozac = new Service1Client();
            var id = await vozac.GetKorisnikIDAsync(TxtPrijavljeni.Text);

            Service1Client combo = new Service1Client();
            var comboVoznja = await combo.LinijeComboAsync(id);

            cmbVoznja.ItemsSource = comboVoznja;
        }

        private void BtnPovratak_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private async void btnIzdaj_ClickAsync(object sender, RoutedEventArgs e)
        {
            KartaIspis kartaIspis = new KartaIspis();

            Service1Client servicePopust = new Service1Client();
            var resPopust = await servicePopust.SelectPopustAsync(ime.SelectedItem.ToString());

            Service1Client serviceVoznja = new Service1Client();
            var resCijena = await serviceVoznja.SelectVoznjaCijenaAsync(cmbVoznja.SelectedItem.ToString(), TxtPrijavljeni.Text);

            float ukupnaCijena = resCijena - (resCijena * (resPopust.KolicinaPopusta / 100));

            Karta karta = new Karta();
            Service1Client servis = new Service1Client();

            var resKartaPopust = await servis.GetPopustIDAsync(ime.SelectedItem.ToString());
            karta.Popust = resKartaPopust;
            var resKartaVOzac = await servis.GetKorisnikIDAsync(TxtPrijavljeni.Text);
            karta.Vozac = resKartaVOzac;
            var resKartaVoznja = await servis.GetVoznjaIDAsync(cmbVoznja.SelectedItem.ToString(), TxtPrijavljeni.Text);
            karta.Voznja = resKartaVoznja;

            var resInsert = await servis.InsertKartaAsync(karta);

            if (resInsert == 0)
            {
                var dialog = new MessageDialog("Pogreška kod kreiranja karte.");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                await dialog.ShowAsync();
            }
            else
            {
                kartaIspis.CijenaVoznje = ukupnaCijena;
                kartaIspis.KolicinaPopusta = resPopust.KolicinaPopusta;
                kartaIspis.Linija = cmbVoznja.SelectedItem.ToString();
                kartaIspis.Popust = ime.SelectedItem.ToString();
                kartaIspis.Vozac = TxtPrijavljeni.Text;
                kartaIspis.KartaID = await servis.GetCountAsync();

                this.Frame.Navigate(typeof(PrikazKarte), kartaIspis);
            }
        }
    }
}