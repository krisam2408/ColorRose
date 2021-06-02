using ColorRoseWPF.Models.Abstracts;
using ColorRoseWPF.Models;
using Color = System.Drawing.Color;
using System.Windows.Media;
using ColorRoseLib;
using System.Windows.Input;
using ColorRoseWPF.Core;
using System.Windows;
using System.Windows.Controls;

namespace ColorRoseWPF.ViewModels
{
    public class SingleColorViewModel:BaseViewModel,INavegable
    {
        private HSBColor sampleColor;

        public Brush SampleColor { get { return new SolidColorBrush(sampleColor.ToARGBColor().ToMediaColor()); } }

        private double sampleHeight;
        public double SampleHeight { get { return sampleHeight; } set { SetValue(ref sampleHeight, value); } }

        private double sampleLeftMargin;
        public double SampleLeftMargin { get { return sampleLeftMargin; } set { SetValue(ref sampleLeftMargin, value); } }

        private double sampleTopMargin;
        public double SampleTopMargin { get { return sampleTopMargin; } set { SetValue(ref sampleTopMargin, value); } }

        private int hue;
        public int Hue
        {
            get { return hue; }
            set
            {
                if(SetValue(ref hue, value))
                {
                    sampleColor.Hue = value;
                    NotifyPropertyChanged(nameof(SampleColor));
                }
            }
        }

        private int saturation;
        public int Saturation
        {
            get { return saturation; }
            set
            {
                if (SetValue(ref saturation, value))
                {
                    sampleColor.Saturation = (byte)value;
                    NotifyPropertyChanged(nameof(SampleColor));
                }
            }
        }

        private int brightness;
        public int Brightness
        {
            get { return brightness; }
            set
            {
                if (SetValue(ref brightness, value))
                {
                    sampleColor.Brightness = (byte)value;
                    NotifyPropertyChanged(nameof(SampleColor));
                }
            }
        }

        private int alpha;
        public int Alpha
        {
            get { return alpha; }
            set
            {
                if (SetValue(ref alpha, value))
                {
                    sampleColor.Alpha = (byte)value;
                    NotifyPropertyChanged(nameof(SampleColor));
                }
            }
        }

        public int HueMaxValue { get { return HSBColor.MaxHue; } }
        public int HueMinValue { get { return HSBColor.MinHue; } }
        public int SaturationMaxValue { get { return HSBColor.MaxSaturation; } }
        public int SaturationMinValue { get { return HSBColor.MinSaturation; } }
        public int BrightnessMaxValue { get { return HSBColor.MaxBrightness; } }
        public int BrightnessMinValue { get { return HSBColor.MinBrightness; } }
        public int AlphaMaxValue { get { return HSBColor.MaxAlpha; } }
        public int AlphaMinValue { get { return HSBColor.MinAlpha; } }

        public ICommand LoadedCommand { get { return new RelayCommand(e => Loaded((RoutedEventArgs)e)); } }

        public SingleColorViewModel()
        {
            sampleColor = HSBColor.Purple;
            Hue = sampleColor.Hue;
            Saturation = sampleColor.Saturation;
            Brightness = sampleColor.Brightness;
            Alpha = sampleColor.Alpha;
            NotifyPropertyChanged(nameof(SampleColor));
        }

        private void Loaded(RoutedEventArgs e)
        {
            Canvas sender = (Canvas)e.Source;
            double height = sender.ActualHeight;
            double width = sender.ActualWidth;
            SampleHeight = height * 3 / 4;
            height -= SampleHeight;
            width -= SampleHeight;
            SampleTopMargin = height / 2;
            SampleLeftMargin = width / 2;
        }
    }
}
