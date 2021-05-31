using System;
using System.Drawing;
using System.Linq;

namespace ColorRoseLib
{
    public struct HSBColor
    {
        private int hue;
        public int Hue
        {
            get { return hue; }
            set
            {
                if (value == 360)
                    hue = 0;
                if (value >= 0 && value < 360)
                    hue = value;
                throw new ArgumentOutOfRangeException(nameof(hue), value, "Value must be an integer between 0 and 360");
            }
        }

        private byte saturation;
        public byte Saturation
        {
            get { return saturation; }
            set
            {
                if (value >= 0 && value <= 100)
                    saturation = value;
                throw new ArgumentOutOfRangeException(nameof(saturation), value, "Value must be an integer between 0 and 100");
            }
        }

        private byte brightness;
        public byte Brightness
        {
            get { return brightness; }
            set
            {
                if (value >= 0 && value <= 100)
                    brightness = value;
                throw new ArgumentOutOfRangeException(nameof(brightness), value, "Value must be an integer between 0 and 100");
            }
        }

        private byte alpha;
        public byte Alpha
        {
            get { return alpha; }
            set
            {
                if (value >= 0 && value <= 255)
                    alpha = value;
                throw new ArgumentOutOfRangeException(nameof(alpha), value, "Value must be an integer between 0 and 255");
            }
        }

        public static int MinHue { get { return 0; } }
        public static int MaxHue { get { return 360; } }
        public static int MinSaturation { get { return 0; } }
        public static int MaxSaturation { get { return 100; } }
        public static int MinBrightness { get { return 0; } }
        public static int MaxBrightness { get { return 100; } }
        public static int MinAlpha { get { return 0; } }
        public static int MaxAlpha { get { return 255; } }

        public static HSBColor Red { get { return new HSBColor(0, 100, 100); } }
        public static HSBColor Orange { get { return new HSBColor(30, 100, 100); } }
        public static HSBColor Yellow { get { return new HSBColor(60, 100, 100); } }
        public static HSBColor Lime { get { return new HSBColor(90, 100, 100); } }
        public static HSBColor Green { get { return new HSBColor(120, 100, 100); } }
        public static HSBColor Aqua { get { return new HSBColor(150, 100, 100); } }
        public static HSBColor Cyan { get { return new HSBColor(180, 100, 100); } }
        public static HSBColor Indigo { get { return new HSBColor(210, 100, 100); } }
        public static HSBColor Blue { get { return new HSBColor(240, 100, 100); } }
        public static HSBColor Purple { get { return new HSBColor(270, 100, 100); } }
        public static HSBColor Magenta { get { return new HSBColor(300, 100, 100); } }
        public static HSBColor Rose { get { return new HSBColor(330, 100, 100); } }
        public static HSBColor White { get { return new HSBColor(0, 0, 100); } }
        public static HSBColor Black { get { return new HSBColor(0, 0, 0); } }
        public static HSBColor Gray { get { return new HSBColor(0, 0, 50); } }
        public static HSBColor DarkGray { get { return new HSBColor(0, 0, 33); } }
        public static HSBColor LightGray { get { return new HSBColor(0, 0, 66); } }
       
        public static HSBColor Transparent { get { return new HSBColor(0, 0, 0, 0); } }

        public HSBColor(int hue, byte saturation, byte brightness, byte alpha)
        {
            this.hue = 0;
            this.saturation = 0;
            this.brightness = 0;
            this.alpha = 0;
            Hue = hue;
            Saturation = saturation;
            Brightness = brightness;
            Alpha = alpha;
        }

        public HSBColor(int hue, byte saturation, byte brightness) : this(hue, saturation, brightness, 255) { }
        
        public HSBColor(int hue, int saturation, int brightness) : this(hue, (byte)saturation, (byte)brightness, 255) { }
        
        public static HSBColor FromARGBColor(Color color)
        {
            byte[] channels = { color.R, color.G, color.B };
            int[] priority = { 0, 0, 0 };
            byte channelMax = 0;
            byte channelMin = 255;


            for(int i = 0; i < channels.Length; i++)
            {
                if(channels[i] > channelMax)
                {
                    channelMax = channels[i];
                    priority[0] = i;
                }
                if(channels[i] < channelMin)
                {
                    channelMin = channels[i];
                    priority[2] = i;
                }
            }

            for (int i = 0; i < priority.Length; i++)
                if (i != priority[0] && i != priority[2])
                {
                    priority[1] = i;
                    break;
                }

            double relation = 0;
            if (channels[priority[0]] > 0)
                relation = channels[priority[1]] / channels[priority[2]];

            int rangePole = 1;
            if(priority[0] == 0 && priority[1] == 2)
            {
                rangePole = -1;
                priority[0] = 3;
            }

            double brightnessFormula = channelMax / 255 * 100;
            double saturationFormula = 100 - (channelMin / 255 * 100);
            double hueFormula = priority[0] * 120 + relation * 60 * rangePole;

            byte brightness = (byte)Math.Round(brightnessFormula);
            byte saturation = (byte)Math.Round(saturationFormula);
            int hue = (int)Math.Round(hueFormula);

            return new HSBColor(hue, saturation, brightness, color.A);
        }

        public Color ToARGBColor()
        {
            byte[] channels = { 0, 0, 0 };
            int quadrant = Hue / 120;
            int spectrum = Hue - quadrant * 120;

            double[] x = new double[2];
            x[0] = 1 - (double)spectrum / 120;
            x[1] = (double)spectrum / 120;

            double xMax = x.Max();
            double coefficient = 255 / xMax;

            channels[quadrant > 2 ? 0 : quadrant] = (byte)(x[0] * coefficient);
            channels[quadrant >= 2 ? 0 : quadrant+1] = (byte)(x[1] * coefficient);

            double brightnessCoefficient = (double)Brightness / 100;
            channels[0] = (byte)Math.Round(channels[0] * brightnessCoefficient);
            channels[1] = (byte)Math.Round(channels[1] * brightnessCoefficient);
            channels[2] = (byte)Math.Round(channels[2] * brightnessCoefficient);

            double brightnessRatio = 255 * Brightness / 100;
            double saturationRatio = 255 * (100 - Saturation) / 100;
            if (saturationRatio > brightnessRatio)
                saturationRatio = brightnessRatio;

            for (int i = 0; i < channels.Length; i++)
                if (channels[i] < saturationRatio)
                    channels[i] = (byte)Math.Round(saturationRatio);

            return Color.FromArgb(Alpha, channels[0], channels[1], channels[2]);
        }

        public string RGBHexCode()
        {
            Color color = ToARGBColor();
            return $"#{color.R:x2}{color.G:x2}{color.B:x2}";
        }

        public string ARGBHexCode()
        {
            Color color = ToARGBColor();
            return $"#{color.A:x2}{color.R:x2}{color.G:x2}{color.B:x2}";
        }

    }
}
