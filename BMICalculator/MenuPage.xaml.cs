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

namespace BMICalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuPage : Page
    {
        public MenuPage()
        {
            this.InitializeComponent();
        }

        private void BMIbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(page_BMI));
            
            
        }

        private void BMRbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(page_BMR));
            
        }

        private void AplikBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OAplikacjiPage));
        }

        private void AutorBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OAutorzePage));
        }
    }
}
