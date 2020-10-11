using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class page_BMI : Page
    {
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

        public page_BMI()
        {
            
            this.InitializeComponent();
            info_frame.Navigate(typeof(page_info_BMI));
            Window.Current.SizeChanged += Current_SizeChanged;
            CheckSize();

            //Sprawdzanie czy plik istnieje
            if (isoStore.FileExists("bmi.txt"))
            {
                System.Diagnostics.Debug.WriteLine("The file already exists!");
                //otwarcie strumienia (pliku)
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("bmi.txt", FileMode.Open, isoStore))
                {
                    //czytanie pliku przy uzyciu StreamReadera
                    using (StreamReader reader = new StreamReader(isoStream))
                    {
                        bmiBlock.Text = "Ostatnie BMI: " + reader.ReadToEnd();
                        bmiBlock.Visibility = Visibility.Visible;
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
                //Obliczanie i wyświetlanie BMI
                double bmi = Convert.ToDouble(weight.Text) / Math.Pow((Convert.ToDouble(height.Text) / 100), 2);
                result.Text = Math.Round(bmi, 2).ToString();
                //W zależności od wyniku BMI, przypisz odpowiedni opis
                switch (bmi)
                {
                    case double n when (n < 16):
                        result_description.Text = "Wygłodzenie";
                        result_description.Background = new SolidColorBrush(Color.FromArgb(40, 255, 0, 0));
                        break;
                    case double n when (n >= 16 && n <= 16.99):
                        result_description.Text = "Wychudzenie";
                        result_description.Background = new SolidColorBrush(Color.FromArgb(40, 255, 0, 0));
                        break;
                    case double n when (n >= 17 && n <= 18.49):
                        result_description.Text = "Niedowaga";
                        result_description.Background = new SolidColorBrush(Color.FromArgb(40, 230, 164, 0));
                        break;
                    case double n when (n >= 18.5 && n <= 24.99):
                        result_description.Text = "Pożądana masa ciała";
                        result_description.Background = new SolidColorBrush(Color.FromArgb(40, 0, 255, 0));
                        break;
                    case double n when (n >= 25 && n <= 29.99):
                        result_description.Text = "Nadwaga";
                        result_description.Background = new SolidColorBrush(Color.FromArgb(40, 230, 164, 0));
                        break;
                    case double n when (n >= 30 && n <= 34.99):
                        result_description.Text = "Otyłość I stopnia";
                        result_description.Background = new SolidColorBrush(Color.FromArgb(40, 230, 164, 0));
                        break;
                    case double n when (n >= 35 && n <= 39.99):
                        result_description.Text = "Otyłość II stopnia (duża)";
                        result_description.Background = new SolidColorBrush(Color.FromArgb(40, 255, 0, 0));
                        break;
                    case double n when (n > 40):
                        result_description.Text = "Otyłość III stopnia (chorobliwa)";
                        result_description.Background = new SolidColorBrush(Color.FromArgb(40, 255, 0, 0));
                        break;

                }
                //Ujawnienie ukrytego tekstu z opisem 
                result_description.Visibility = Visibility.Visible;

                //Otwarcie lub utworzenie pliku
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("bmi.txt", FileMode.OpenOrCreate, isoStore))
                {
                    //zapis do pliku przy uzyciu StreamWritera
                    using (StreamWriter writer = new StreamWriter(isoStream))
                    {
                        writer.WriteLine(Math.Round(bmi, 2));

                    }
                }

            } catch (System.FormatException ex)
            {
                result.Text = "Wprowadzono nieprawidłowe dane.";
                result_description.Text = ex.Message;
                result_description.Background = new SolidColorBrush(Color.FromArgb(40, 255, 0, 0));
                result_description.Visibility = Visibility.Visible;
            }
           
        }
    }
}
