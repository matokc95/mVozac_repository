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
            //ime.SelectionChanged += Ime_SelectionChangedAsync;
            //ObservableCollection<Popust> lista = new ObservableCollection<Popust>();
            //lista.Add(new Popust() { NazivPopusta = "a", KolicinaPopusta = 10 });
            //lista.Add(new Popust() { NazivPopusta = "b", KolicinaPopusta = 10 });
            //ime.ItemsSource = lista;
            //lista.cha

            //Service1Client combo = new Service1Client();
            //var comboPopust = combo.PopustiComboAsync();

            //ime.ItemsSource = comboPopust;
            DohvatiPopuste();
            DohvatiVoznje();

            TxtPrijavljeni.Text = e.Parameter.ToString();
        }

        private async void DohvatiPopuste()
        {
            //ime.SelectionChanged += Ime_SelectionChangedAsync;

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

        /*
        private async void Ime_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            var dialog = new MessageDialog(ime.SelectedItem.ToString());
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
            await dialog.ShowAsync();
        }
        */

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
            txtPrice.Text = ukupnaCijena.ToString();

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
                /*
                var dialog = new MessageDialog("Uspješno ste kreirali kartu!");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                await dialog.ShowAsync();
                */
                kartaIspis.CijenaVoznje = ukupnaCijena;
                kartaIspis.KolicinaPopusta = resPopust.KolicinaPopusta;
                kartaIspis.Linija = cmbVoznja.SelectedItem.ToString();
                kartaIspis.Popust = ime.SelectedItem.ToString();
                kartaIspis.Vozac = TxtPrijavljeni.Text;

                this.Frame.Navigate(typeof(PrikazKarte), kartaIspis);
            }

        }
    }
}
