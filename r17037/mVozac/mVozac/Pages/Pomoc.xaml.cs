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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace mVozac.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pomoc : Page
    {
        string tekst = "Prodaja karata -> funkcionalnost kojom vozač prodaje kartu za svoju trenutnu vožnju.\n" +
            "Poništavanje karte -> funkcionalnost kojom vozač može poništiti, odnosno obrisati kartu unošenjem ID broja karte.\n" +
            "Pretraživanje karte -> funkcionalnost kojom vozač može provjeriti da li karta postoji, i ako postoji ispisuje podatke o njoj na temelju ID broja karte.\n" +
            "Raspored vožnje -> funkcionalnost kojom vozač autobusa vidi informacije o svojoj dnevnoj vožnji i prihvaća ih\n " +
            "Tahometar -> ako je vozač autobusas potvrdio relaciju u funkcionalnosti 'Raspored vožnje', na ekranu mu se prikaže polazišni i " +
            "odredišni kolodvor i pritiskom na tipku 'Započni rutu' na karti mu se prikaže ruta, kao i navigacijske informacije.Postoji i mogućnost " +
            "lociranja korisnikove lokacije koja se vrši pritiskom na gumb 'Moja lokacija'".Replace("\n", Environment.NewLine);
        public Pomoc()
        {
            this.InitializeComponent();
        }

        private void Page_Loading(FrameworkElement sender, object args)
        {
            txtPomoc.Text = tekst;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
