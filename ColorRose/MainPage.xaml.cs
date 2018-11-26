using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ColorRose
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private Color Color;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            HueSlider.Value = 0;
            SatSlider.Value = 100;
            BrgSlider.Value = 100;
            HueValueBox.Text = ((float)HueSlider.Value).ToString() + "º";
            SatValueBox.Text = ((int)SatSlider.Value).ToString();
            BrgValueBox.Text = ((int)BrgSlider.Value).ToString();

            Color = Color.
            
        }

        private void HueSliderFunction(object sender, RangeBaseValueChangedEventArgs e)
        {
            HueValueBox.Text = ((float)HueSlider.Value).ToString() + "º";
        }

        private void SatSliderFunction(object sender, RangeBaseValueChangedEventArgs e)
        {
            SatValueBox.Text = ((int)SatSlider.Value).ToString();
        }

        private void BrgSliderFunction(object sender, RangeBaseValueChangedEventArgs e)
        {
            BrgValueBox.Text = ((int)BrgSlider.Value).ToString();
        }

        
    }
}
