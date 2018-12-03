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
using ColorRoseLib;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ColorRose.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorHSB : Page
    { 
        private ColorRoseLib.ColorHSB Color;

        public ColorHSB()
        {
            this.InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            HueSlider.Value = 60;
            SatSlider.Value = 100;
            BrgSlider.Value = 100;
            HueValueBox.Text = ((float)HueSlider.Value).ToString() + "º";
            SatValueBox.Text = ((int)SatSlider.Value).ToString();
            BrgValueBox.Text = ((int)BrgSlider.Value).ToString();

            RefreshWheel();

        }

        private void RefreshWheel()
        {
            Color = new ColorRoseLib.ColorHSB((int)HueSlider.Value, (int)SatSlider.Value, (int)BrgSlider.Value);
            ColorWheel.Fill = new SolidColorBrush(Color.RGBUI);
            HexCodeBlox.Text = Color.RGBHexCode;
        }

        private void HueSliderFunction(object sender, RangeBaseValueChangedEventArgs e)
        {
            HueValueBox.Text = ((float)HueSlider.Value).ToString() + "º";
            RefreshWheel();
        }

        private void SatSliderFunction(object sender, RangeBaseValueChangedEventArgs e)
        {
            SatValueBox.Text = ((int)SatSlider.Value).ToString();
            RefreshWheel();
        }

        private void BrgSliderFunction(object sender, RangeBaseValueChangedEventArgs e)
        {
            BrgValueBox.Text = ((int)BrgSlider.Value).ToString();
            RefreshWheel();
        }

        private void HueBoxFunction(object sender, TextChangedEventArgs e)
        {
            try
            {
                string str = HueValueBox.Text.Replace("º", "");
                int v = int.Parse(str);
                if (v >= 0 && v <= 360)
                {
                    double val = double.Parse(str);
                    HueSlider.Value = val;
                    HueValueBox.Text = str + "º";
                }
            }
            catch (FormatException)
            {
                HueValueBox.Text = Color.RGB.GetHue().ToString() + "º";
            }
        }

        private void SatBoxFunction(object sender, TextChangedEventArgs e)
        {
            try
            {
                int v = int.Parse(SatValueBox.Text);
                if (v >= 0 && v <= 100)
                {
                    double val = double.Parse(SatValueBox.Text);
                    SatSlider.Value = val;
                }
            }
            catch (FormatException)
            {
                SatValueBox.Text = Color.RGB.GetSaturation().ToString();
            }
        }

        private void BrgBoxFunction(object sender, TextChangedEventArgs e)
        {
            try
            {
                int v = int.Parse(BrgValueBox.Text);
                if (v >= 0 && v <= 100)
                {
                    double val = double.Parse(BrgValueBox.Text);
                    BrgSlider.Value = val;
                }
            }
            catch (FormatException)
            {
                BrgValueBox.Text = Color.RGB.GetSaturation().ToString();
            }
        }
    }
}
