﻿using System;
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
using mVozac.Pages;
using ZXing.Mobile;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace mVozac.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PrikazKarte : Page
    {
        public PrikazKarte()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            KartaIspis karta = (KartaIspis)e.Parameter;

            txtCijena.Text = karta.CijenaVoznje.ToString();
            txtLinija.Text = karta.Linija;
            txtPopust.Text = karta.Popust;
            txtVozac.Text = karta.Vozac;

            var writer = new ZXing.Mobile.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 300,
                    Width = 300
                },
                Renderer = new ZXing.Mobile.WriteableBitmapRenderer()
                {
                    Foreground = Windows.UI.Colors.Black
                }
            };

            var writeableBitmap = writer.Write(karta.KartaID.ToString());

            qrCode.Source = writeableBitmap;

            base.OnNavigatedTo(e);
        }

        private void BtnPovratak_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}