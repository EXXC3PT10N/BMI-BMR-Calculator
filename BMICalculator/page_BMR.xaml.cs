using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.IO.IsolatedStorage;
using Windows.UI.ViewManagement;
using Windows.Graphics.Display;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BMICalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class page_BMR : Page
    {
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
        public page_BMR()
        {
            this.InitializeComponent();
            info_frame.Navigate(typeof(page_info_BMR));
            Window.Current.SizeChanged += Current_SizeChanged;
            CheckSize();

            //Sprawdzanie czy plik istnieje
            if (isoStore.FileExists("bmr.txt"))
            {
                System.Diagnostics.Debug.WriteLine("The file already exists!");
                //otwarcie strumienia (pliku)
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("bmr.txt", FileMode.Open, isoStore))
                {
                    //czytanie pliku przy uzyciu StreamReadera
                    using (StreamReader reader = new StreamReader(isoStream))
                    {
                        bmrBlock.Text = "Ostatnie BMR: " + reader.ReadToEnd();
                        bmrBlock.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            CheckSize();
        }

        //Sprawdza wielkosc okna i ukrywa element z informacjami jesli okno jest za waskie, ukrywa ramke z informacjami
        private void CheckSize()
        {
            // Get the visible bounds for current view
            var visibleBounds = ApplicationView.GetForCurrentView().VisibleBounds;

            // Get the scale factor from display information
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;

            // Get the application screen size
            var size = new Size(visibleBounds.Width * scaleFactor, visibleBounds.Height * scaleFactor);
            
            if (size.Width < 850)
                info_frame.Visibility = Visibility.Collapsed;
            else info_frame.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Obliczanie BMR
                //W zależności od wybranej płci oblicza odpowiedni BMR
                double bmr = 0;
                if (Convert.ToBoolean(female.IsChecked))
                {
                    bmr = 655 + (9.6 * Convert.ToDouble(weight.Text)) + (1.8 * Convert.ToDouble(height.Text)) - (4.7 * Convert.ToDouble(age.Text));
                }
                else if (Convert.ToBoolean(male.IsChecked))
                {
                    bmr = 66 + (13.7 * Convert.ToDouble(weight.Text)) + (5 * Convert.ToDouble(height.Text)) - (6.76 * Convert.ToDouble(age.Text));
                }
                

                //W zależności od wybranej aktywności fizycznej, przypisuje odpowiednie activity_ratio i oblicza finalne BMR
                double activity_ratio = 0;
                if (activity1.IsSelected)
                    activity_ratio = 1.2;
                else if (activity2.IsSelected)
                    activity_ratio = 1.35;
                else if (activity3.IsSelected)
                    activity_ratio = 1.55;
                else if (activity4.IsSelected)
                    activity_ratio = 1.75;
                else if (activity5.IsSelected)
                    activity_ratio = 2.05;


                bmr *= activity_ratio;
                result.Text = bmr.ToString();
                //Otwarcie lub utworzenie pliku
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("bmr.txt", FileMode.OpenOrCreate, isoStore))
                {
                    //zapis do pliku przy uzyciu StreamWritera
                    using (StreamWriter writer = new StreamWriter(isoStream))
                    {
                        writer.WriteLine(bmr);

                    }
                }


            } catch (Exception ex)
            {
                result.Text = "Wprowadzono nieprawidlowe dane.";
            }
            

        }

        private void Activity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
