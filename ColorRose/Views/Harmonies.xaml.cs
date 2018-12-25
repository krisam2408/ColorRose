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
using Windows.UI.Xaml.Shapes;
using ColorRose.Lib;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ColorRose.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Harmonies : Page
    {
        public Harmonies()
        {
            this.InitializeComponent();
        }

        private void Update(HarmoniesType htt)
        {
            OutputPanel.Children.Clear();
            ColorHSB color = new ColorHSB(MainPage.GlobalColor);
            List<ColorHSB[]> harmonies = new List<ColorHSB[]>();
            List<int> hues = new List<int>();
            int colorCount;

            switch(htt)
            {
                case HarmoniesType.Analogous:
                    colorCount = 3;
                    hues.Add(color.Hue);
                    hues.Add(color.Hue - 30 >= 0 ? color.Hue - 30 : color.Hue + 330);
                    hues.Add(color.Hue+30<=360?color.Hue+30:color.Hue-330);
                    break;
                case HarmoniesType.Triadric:
                    colorCount = 4;
                    hues.Add(color.Hue);
                    hues.Add(color.Hue + 90 <= 360 ? color.Hue + 90 : color.Hue - 270);
                    hues.Add(color.Hue + 180 <= 360 ? color.Hue + 180 : color.Hue - 180);
                    hues.Add(color.Hue + 270 <= 360 ? color.Hue + 270 : color.Hue - 90);
                    break;
                case HarmoniesType.Complimentary:
                    colorCount = 2;
                    hues.Add(color.Hue);
                    hues.Add(color.Hue + 180 <= 360 ? color.Hue + 180 : color.Hue - 180);
                    break;
                case HarmoniesType.SplitComplimentary:
                    colorCount = 3;
                    hues.Add(color.Hue);
                    hues.Add(color.Hue + 150 <= 360 ? color.Hue + 150 : color.Hue - 230);
                    hues.Add(color.Hue - 150 >= 0 ? color.Hue - 150 : color.Hue + 230);
                    break;
                case HarmoniesType.DoubleComplimentary:
                    colorCount = 4;
                    hues.Add(color.Hue - 30 >= 0 ? color.Hue - 30 : color.Hue + 330);
                    hues.Add(color.Hue + 30 <= 360 ? color.Hue + 30 : color.Hue - 330);
                    hues.Add(color.Hue + 150 <= 360 ? color.Hue + 150 : color.Hue - 230);
                    hues.Add(color.Hue - 150 >= 0 ? color.Hue - 150 : color.Hue + 230);
                    break;
                default:
                    colorCount = 1;
                    hues.Add(color.Hue);
                    break;
            }

            foreach(int i in hues)
            {
                harmonies.Add(SingleColorPalette(i));
            }

            for(int i = 0; i < colorCount; i++)
            {
                StackPanel sPanel = new StackPanel
                {
                    Orientation = Orientation.Vertical
                };
                for (int e = 0; e < 11; e++)
                {
                    Rectangle rect = new Rectangle
                    {
                        Height=25,
                        Width = 50,
                        Fill = new SolidColorBrush(harmonies[i][e].ColorRGB)
                    };
                    sPanel.Children.Add(rect);
                }
                OutputPanel.Children.Add(sPanel);
            }

        }

        private ColorHSB[] SingleColorPalette(int hue)
        {
            ColorHSB[] palette = new ColorHSB[11];
            for(int step = 0; step < 11; step++)
            {
                if (step < 6)
                {
                    palette[step] = new ColorHSB(hue, 100, step * 20);
                }
                if (step == 6)
                {
                    palette[step] = new ColorHSB(hue, 100, 100);
                }
                if (step > 6)
                {
                    palette[step] = new ColorHSB(hue, 100 - ((step - 5) * 20), 100);
                }
            }

            return palette;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HarmoniesType htt = MainPage.HarmonyOption;
            switch(htt)
            {
                case HarmoniesType.Analogous:
                    Option1.IsChecked = true;
                    break;
                case HarmoniesType.Triadric:
                    Option2.IsChecked = true;
                    break;
                case HarmoniesType.Complimentary:
                    Option3.IsChecked = true;
                    break;
                case HarmoniesType.SplitComplimentary:
                    Option4.IsChecked = true;
                    break;
                case HarmoniesType.DoubleComplimentary:
                    Option5.IsChecked = true;
                    break;
                default:
                    Option0.IsChecked = true;
                    break;
            }
            Update(htt);
        }

        private void Option_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            string op = rb.Name.Remove(0, 6);
            HarmoniesType htt = (HarmoniesType)int.Parse(op);
            Update(htt);
            MainPage.HarmonyOption = htt;
        }
    }
}
