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
    public sealed partial class RasporedVoznje : Page
    {
        public RasporedVoznje()
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

        private async void Page_Loading(FrameworkElement sender, object args)
        {
            Service1Client service = new Service1Client();
            var res = await service.SelectVoznjuAsync(TxtPrijavljeni.Text);
            if (res.ImeVozaca != null)
            {
                txtIme.Text = res.ImeVozaca;
                txtPrezime.Text = res.PrezimeVozaca;
                txtBrojSjedala.Text = res.BrojSjedala.ToString();
                txtLinija.Text = res.NazivLinije;
                txtDuzina.Text = res.DuzinaBusa.ToString();
                txtSirina.Text = res.SirinaBusa.ToString();
                txtBrzina.Text = res.MaxBrzina.ToString();
            }
            else
            {
                var dialog = new MessageDialog("Korisnik nema definiranu rutu!");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                dialog.ShowAsync();
            }
        }

        private async void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            Service1Client service = new Service1Client();
          
            var res = await service.PotvrdiVoznjuAsync(txtLinija.Text);
            if (res == -1)
            {
                var dialog = new MessageDialog("Došlo je do pogreške!");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                await dialog.ShowAsync();
            }
            else
            {
                var dialog2 = new MessageDialog("Vožnja je prihvaćena!");
                dialog2.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                await dialog2.ShowAsync();
                this.Frame.GoBack();
            }     
        }
    }
}
