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

namespace mVozac
{
    public sealed partial class Registracija : Page
    {
        public Registracija()
        {
            this.InitializeComponent();
        }

        private void Povratak_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
        }


        private void Register_Click(object sender, RoutedEventArgs e)
        {      
            if (TxtName.Text.Length == 0 || TxtPrezime.Text.Length == 0 || 
                TxtOIB.Text.Length == 0 || TxtKorIme.Text.Length == 0 || 
                TxtPwd.Password.Length == 0 || TxtAddr.Text.Length == 0)
            {
                var dialog = new MessageDialog("Niste unijeli sva polja!");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                dialog.ShowAsync();
            }
            else
            {
                Korisnik kor = new Korisnik();
                kor.Ime = TxtName.Text;
                kor.Prezime = TxtPrezime.Text;
                kor.DatumRodenja = DatumPicker.Date.Date;
                kor.OIB = TxtOIB.Text;
                kor.KorisnickoIme = TxtKorIme.Text;
                kor.Lozinka = TxtPwd.Password;
                kor.Email = TxtAddr.Text;

                Service1Client service = new Service1Client();
                service.InsertKorisnikaAsync(kor);

                var dialog = new MessageDialog("Uspješno ste se registrirali!");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                dialog.ShowAsync();

                this.Frame.Navigate(typeof(MainPage));
            }
        }
    }
}
