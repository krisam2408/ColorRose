using ColorRoseWPF.Core.Abstracts;
using ColorRoseWPF.Models;
using System.Windows.Media;
using ColorRoseLib;
using System.Windows.Input;
using ColorRoseWPF.Core;
using System.Windows;
using System.Windows.Controls;

namespace ColorRoseWPF.ViewModels
{
    public class SingleColorViewModel : BaseViewModel, INavegable
    {
        private HSBColor SourceColor
        {
            get
            {
                HSBColor output = new HSBColor(Hue.Value, (byte)Saturation.Value, (byte)Brightness.Value, (byte)Opacity.Value);
                HexCode = output.RGBHexCode();
                return output;
            }
        }

        public Brush SampleColor { get { return new SolidColorBrush(SourceColor.ToARGB().ToMediaColor()); } }

        private double sampleHeight;
        public double SampleHeight { get { return sampleHeight; } set { SetValue(ref sampleHeight, value); } }

        private double sampleLeftMargin;
        public double SampleLeftMargin { get { return sampleLeftMargin; } set { SetValue(ref sampleLeftMargin, value); } }

        private double sampleTopMargin;
        public double SampleTopMargin { get { return sampleTopMargin; } set { SetValue(ref sampleTopMargin, value); } }

        private ColorControl hue;
        public ColorControl Hue
        {
            get { return hue; }
            set { SetValue(ref hue, value); }
        }

        private ColorControl saturation;
        public ColorControl Saturation
        {
            get { return saturation; }
            set { SetValue(ref saturation, value); }
        }

        private ColorControl brightness;
        public ColorControl Brightness
        {
            get { return brightness; }
            set { SetValue(ref brightness, value); }
        }

        private ColorControl opacity;
        public ColorControl Opacity
        {
            get { return opacity; }
            set { SetValue(ref opacity, value); }
        }

        private string hexCode;
        public string HexCode 
        { 
            get { return hexCode; }
            set { SetValue(ref hexCode, value); }
        }

        public ICommand LoadedCommand { get { return new RelayCommand(e => Loaded((RoutedEventArgs)e)); } }

        public SingleColorViewModel(HSBColor startingColor)
        {
            Initialize(startingColor);
            NotifyPropertyChanged(nameof(SampleColor));
        }

        private void Initialize(HSBColor startingColor)
        {
            Hue = new(() => 
            { 
                NotifyPropertyChanged(nameof(SampleColor)); 
                NotifyPropertyChanged("Hue"); 
            })
            {
                Name = "Hue",
                MinValue = HSBColor.MinHue,
                MaxValue = HSBColor.MaxHue,
                Value = startingColor.Hue
            };
            Saturation = new(() => 
            { 
                NotifyPropertyChanged(nameof(SampleColor)); 
                NotifyPropertyChanged("Saturation.Value"); 
            })
            {
                Name = "Saturation",
                MinValue = HSBColor.MinSaturation,
                MaxValue = HSBColor.MaxSaturation,
                Value = startingColor.Saturation
            };
            Brightness = new(() => 
            { 
                NotifyPropertyChanged(nameof(SampleColor));
                NotifyPropertyChanged(nameof(Brightness));
            })
            {
                Name = "Brightness",
                MinValue = HSBColor.MinBrightness,
                MaxValue = HSBColor.MaxBrightness,
                Value = startingColor.Brightness
            };
            Opacity = new(() => 
            { 
                NotifyPropertyChanged(nameof(SampleColor)); 
                NotifyPropertyChanged(nameof(Opacity)); 
            })
            {
                Name = "Opacity",
                MinValue = HSBColor.MinOpacity,
                MaxValue = HSBColor.MaxOpacity,
                Value = startingColor.Opacity
            };

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
