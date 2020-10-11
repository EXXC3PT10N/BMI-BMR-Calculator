using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BMICalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
           
           //Po załadowaniu aplikacji, przejście do sekcji Menu
           frame.Navigate(typeof(MenuPage));
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Kliknięcie w przycisk "BMI" przenosi do stron z BMI 
            frame.Navigate(typeof(page_BMI));
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Kliknięcie w przycisk "BMR" przenosi do stron z BMR
            frame.Navigate(typeof(page_BMR));
            
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(MenuPage));
        }
    }
}
