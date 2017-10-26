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
using Windows.UI.Popups;
using Windows.System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace mVozac
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Prijava : Page
    {
        public Prijava()
        {
            this.InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Registracija), null);
        }

        private async void Login_ClickAsync(object sender, RoutedEventArgs e)
        {
            Korisnik kor = new Korisnik()
            {
                KorisnickoIme = TxtKorIme.Text,
                Lozinka = TxtLozinka.Password

            };

            Service1Client service = new Service1Client();
            var res = await service.SelectKorisnikaAsync(kor);

            if (res.KorisnickoIme == kor.KorisnickoIme && res.Lozinka == kor.Lozinka)
            {
                this.Frame.Navigate(typeof(Pocetna), res);
            }
            else
            {
                var dialog = new MessageDialog("Neuspjela prijava!");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                await dialog.ShowAsync();
            }
        }

        private async void TxtLozinka_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                Login_ClickAsync(sender,e);

            }
        }
    }
}