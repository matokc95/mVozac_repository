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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace mVozac
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Registracija), null);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            List<Korisnik> listaKorisnika = new List<Korisnik>();
            Korisnik kor = new Korisnik()
            {
                KorisnickoIme = TxtKorIme.Text
            };
            Service1Client service = new Service1Client();
            //listaKorisnika.Add(service.SelectKorisnikaAsync(kor));
        }
    }
}