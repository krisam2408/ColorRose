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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ColorRose.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorRGBPage : Page
    {
        private bool isLoaded;
        private Color RGBColor;

        public ColorRGBPage()
        {
            this.InitializeComponent();
        }

        private void RefreshWheel()
        {
            if(isLoaded)
            {
                RGBColor.R = (byte)RedSlider.Value;
                RGBColor.G = (byte)GreenSlider.Value;
                RGBColor.B = (byte)BlueSlider.Value;
                ColorWheel.Fill = new SolidColorBrush(RGBColor);
                MainPage.GlobalColor = RGBColor;
                HexCodeBlox.Text = MainPage.ColorHexCode;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            isLoaded = false;
            Color temp = MainPage.GlobalColor;

            RedSlider.Value = temp.R;
            GreenSlider.Value = temp.G;
            BlueSlider.Value = temp.B;
            RedValueBox.Text = ((int)RedSlider.Value).ToString();
            GreenValueBox.Text = ((int)GreenSlider.Value).ToString();
            BlueValueBox.Text = ((int)BlueSlider.Value).ToString();

            RGBColor = temp;

            isLoaded = true;

            RefreshWheel();
        }

        private void RedValueBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int v = int.Parse(RedValueBox.Text);
                if(v >= 0 && v <= 255)
                {
                    double val = double.Parse(RedValueBox.Text);
                    RedSlider.Value = val;
                }
            }
            catch(FormatException)
            {
                RedValueBox.Text = MainPage.GlobalColor.R.ToString();
            }
        }

        private void GreenValueBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int v = int.Parse(GreenValueBox.Text);
                if (v >= 0 && v <= 255)
                {
                    double val = double.Parse(GreenValueBox.Text);
                    GreenSlider.Value = val;
                }
            }
            catch (FormatException)
            {
                GreenValueBox.Text = MainPage.GlobalColor.R.ToString();
            }
        }

        private void BlueValueBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int v = int.Parse(BlueValueBox.Text);
                if (v >= 0 && v <= 255)
                {
                    double val = double.Parse(BlueValueBox.Text);
                    BlueSlider.Value = val;
                }
            }
            catch (FormatException)
            {
                BlueValueBox.Text = MainPage.GlobalColor.R.ToString();
            }
        }

        private void RedSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            RedValueBox.Text = ((int)RedSlider.Value).ToString();
            RefreshWheel();
        }

        private void GreenSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            GreenValueBox.Text = ((int)GreenSlider.Value).ToString();
            RefreshWheel();
        }

        private void BlueSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            BlueValueBox.Text = ((int)BlueSlider.Value).ToString();
            RefreshWheel();
        }

    }
}
