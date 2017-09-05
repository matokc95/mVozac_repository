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
using ZXing.Mobile;
using System.Threading.Tasks;
using ZXing;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace mVozac.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PretraziKarte : Page
    {
        private MediaCapture _mediaCapture;

        public PretraziKarte()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            TxtPrijavljeni.Text = e.Parameter.ToString();
            txtPonistena.Opacity = 0;
        }

        private void BtnPovratak_Click(object sender, RoutedEventArgs e)
        {
            _mediaCapture.Dispose();
            this.Frame.GoBack();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DeviceInformationCollection webcamList = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            DeviceInformation backWebCam = (from webcam in webcamList
                                            where webcam.IsEnabled
                                            select webcam).FirstOrDefault();

            _mediaCapture = new MediaCapture();

            await _mediaCapture.InitializeAsync(new MediaCaptureInitializationSettings
            {
                VideoDeviceId = backWebCam.Id,
                AudioDeviceId = "",
                StreamingCaptureMode = StreamingCaptureMode.Video,
                PhotoCaptureSource = PhotoCaptureSource.VideoPreview
            });

            captureElement.Source = _mediaCapture;
            await _mediaCapture.StartPreviewAsync();

            var imgProp = new ImageEncodingProperties
            {
                Subtype = "BMP",
                Width = 600,
                Height = 800
            };

            var bcReader = new BarcodeReader();

            while (true)
            {
                var stream = new InMemoryRandomAccessStream();

                try
                {
                    await _mediaCapture.CapturePhotoToStreamAsync(imgProp, stream);

                    stream.Seek(0);
                    var wbm = new WriteableBitmap(600, 800);
                    await wbm.SetSourceAsync(stream);

                    SoftwareBitmap sbmp = SoftwareBitmap.CreateCopyFromBuffer
                        (wbm.PixelBuffer, BitmapPixelFormat.Bgra8, wbm.PixelWidth, wbm.PixelHeight);

                    SoftwareBitmapLuminanceSource luminanceSource = new SoftwareBitmapLuminanceSource(sbmp);

                    var result = bcReader.Decode(luminanceSource);

                    if (result != null)
                    {
                        Service1Client serviceKarta = new Service1Client();
                        var res = await serviceKarta.FindKartaAsync(int.Parse(result.Text));

                        if (res.KartaID == 0)
                        {
                            var dialog = new MessageDialog("Karta ne postoji!");
                            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                            await dialog.ShowAsync();

                            txtPopust.Text = "";
                            txtVozac.Text = "";
                            txtLinija.Text = "";
                            txtPrice.Text = "";
                            txtPonistena.Text = "";
                        }
                        else
                        {
                            if (res.Ponistena == true)
                            {
                                /*
                                var dialog = new MessageDialog("Karta se ne može pretražiti jer je poništena!");
                                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                                await dialog.ShowAsync();
                                */

                                float price = res.CijenaVoznje;

                                float ukupnaCijena = price - (price * (res.KolicinaPopusta / 100));

                                txtPopust.Text = res.Popust;
                                txtVozac.Text = res.Vozac;
                                txtLinija.Text = res.Linija;
                                txtPrice.Text = ukupnaCijena.ToString();
                                txtPonistena.Opacity = 100;
                                txtPonistena.Text = "Karta je poništena";
                            }
                            else
                            {
                                float price = res.CijenaVoznje;

                                float ukupnaCijena = price - (price * (res.KolicinaPopusta / 100));

                                txtPopust.Text = res.Popust;
                                txtVozac.Text = res.Vozac;
                                txtLinija.Text = res.Linija;
                                txtPrice.Text = ukupnaCijena.ToString();
                                txtPonistena.Opacity = 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var dialog = new MessageDialog(ex.Message);
                    dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                    await dialog.ShowAsync();

                    //throw;
                }
            }
        }
    }
}