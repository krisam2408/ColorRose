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
using ColorRose.Lib;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ColorRose.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorHSBPage : Page
    {
        private ColorHSB HSBColor;
        private bool isLoaded;

        public ColorHSBPage()
        {
            this.InitializeComponent();
            
        }

        private void RefreshWheel()
        {
            if(isLoaded)
            {
                HSBColor.Hue = (int)HueSlider.Value;
                HSBColor.Saturation = (byte)SatSlider.Value;
                HSBColor.Brightness = (byte)BrgSlider.Value;
                ColorWheel.Fill = new SolidColorBrush(HSBColor.ColorRGB);
                MainPage.GlobalColor = this.HSBColor.ColorRGB;
                HexCodeBlox.Text = MainPage.ColorHexCode;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            isLoaded = false;
            ColorHSB temp = new ColorHSB(MainPage.GlobalColor);

            HueSlider.Value = temp.Hue;
            SatSlider.Value = temp.Saturation;
            BrgSlider.Value = temp.Brightness;
            HueValueBox.Text = ((int)HueSlider.Value).ToString() + "º";
            SatValueBox.Text = ((int)SatSlider.Value).ToString();
            BrgValueBox.Text = ((int)BrgSlider.Value).ToString();

            HSBColor = temp;
            isLoaded = true;

            RefreshWheel();
        }

        private void HueSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            HueValueBox.Text = ((float)HueSlider.Value).ToString() + "º";
            RefreshWheel();
        }

        private void SatSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SatValueBox.Text = ((int)SatSlider.Value).ToString();
            RefreshWheel();
        }

        private void BrgSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            BrgValueBox.Text = ((int)BrgSlider.Value).ToString();
            RefreshWheel();
        }

        private void HueValueBox_TextChanged(object sender, TextChangedEventArgs e)
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
                HueValueBox.Text = new ColorHSB(MainPage.GlobalColor).Hue + "º";
            }
        }

        private void SatValueBox_TextChanged(object sender, TextChangedEventArgs e)
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
                SatValueBox.Text = new ColorHSB(MainPage.GlobalColor).Saturation.ToString();
            }
        }

        private void BrgValueBox_TextChanged(object sender, TextChangedEventArgs e)
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
                BrgValueBox.Text = new ColorHSB(MainPage.GlobalColor).Brightness.ToString();
            }
        }

    }
}
