using ColorRoseWPF.Models.Abstracts;
using ColorRoseWPF.Models;
using Color = System.Drawing.Color;
using System.Windows.Media;
using ColorRoseLib;
using System.Windows.Input;
using ColorRoseWPF.Core;
using System.Windows;
using System.Windows.Controls;
using System;

namespace ColorRoseWPF.ViewModels
{
    public class SingleColorViewModel:BaseViewModel,INavegable
    {
        private HSBColor SourceColor
        {
            get
            {
                return new HSBColor(Hue.Value, (byte)Saturation.Value, (byte)Brightness.Value, (byte)Alpha.Value);
            }
        }

        public Brush SampleColor { get { return new SolidColorBrush(SourceColor.ToARGBColor().ToMediaColor()); } }

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

        private ColorControl alpha;
        public ColorControl Alpha
        {
            get { return alpha; }
            set { SetValue(ref alpha, value); }
        }

        public ICommand LoadedCommand { get { return new RelayCommand(e => Loaded((RoutedEventArgs)e)); } }

        public SingleColorViewModel(HSBColor startingColor)
        {
            Initialize(startingColor);
            NotifyPropertyChanged(nameof(SampleColor));
        }

        private void Initialize(HSBColor startingColor)
        {
            Hue = new(() => { NotifyPropertyChanged(nameof(SampleColor)); })
            {
                Name = "Hue",
                MinValue = HSBColor.MinHue,
                MaxValue = HSBColor.MaxHue,
                Value = startingColor.Hue
            };
            Saturation = new(() => { NotifyPropertyChanged(nameof(SampleColor)); })
            {
                Name = "Saturation",
                MinValue = HSBColor.MinSaturation,
                MaxValue = HSBColor.MaxSaturation,
                Value = startingColor.Saturation
            };
            Brightness = new(() => { NotifyPropertyChanged(nameof(SampleColor)); })
            {
                Name = "Brightness",
                MinValue = HSBColor.MinBrightness,
                MaxValue = HSBColor.MaxBrightness,
                Value = startingColor.Brightness
            };
            Alpha = new(() => { NotifyPropertyChanged(nameof(SampleColor)); })
            {
                Name = "Alpha",
                MinValue = HSBColor.MinAlpha,
                MaxValue = HSBColor.MaxAlpha,
                Value = startingColor.Alpha
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
