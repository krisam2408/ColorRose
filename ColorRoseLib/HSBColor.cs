using ColorRoseLib.Exceptions;
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
            set { hue = SetHue(value); }
        }

        private byte saturation;
        public byte Saturation
        {
            get { return saturation; }
            set
            {
                if (value >= MinSaturation && value <= MaxSaturation)
                    saturation = value;
                if (value < MinSaturation)
                    saturation = MinSaturation;
                if (value > MaxSaturation)
                    saturation = MaxSaturation;
            }
        }

        private byte brightness;
        public byte Brightness
        {
            get { return brightness; }
            set
            {
                if (value >= MinBrightness && value <= MaxBrightness)
                    brightness = value;
                if (value < MinBrightness)
                    brightness = MinBrightness;
                if (value > MaxBrightness)
                    brightness = MaxBrightness;
            }
        }

        private byte opacity;
        public byte Opacity
        {
            get { return opacity; }
            set
            {
                if (value >= MinOpacity && value <= MaxOpacity)
                    opacity = value;
                if (value < MinOpacity)
                    opacity = MinOpacity;
                if (value > MaxOpacity)
                    opacity = MaxOpacity;
            }
        }

        public static int MinHue { get { return 0; } }
        public static int MaxHue { get { return 360; } }
        public static byte MinSaturation { get { return 0; } }
        public static byte MaxSaturation { get { return 100; } }
        public static byte MinBrightness { get { return 0; } }
        public static byte MaxBrightness { get { return 100; } }
        public static byte MinOpacity { get { return 0; } }
        public static byte MaxOpacity { get { return 255; } }

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

        public static HSBColor RoseDark { get { return FromHexCode("#24292E"); } }
        public static HSBColor RoseGray { get { return FromHexCode("#343a40"); } }
        public static HSBColor RoseLight { get { return FromHexCode("#f8f9fa"); } }
        public static HSBColor RoseDarkLight { get { return FromHexCode("#b8b9ba"); } }
        public static HSBColor RoseBlue { get { return FromHexCode("#007bff"); } }
        public static HSBColor RoseIndigo { get { return FromHexCode("#6610f2"); } }
        public static HSBColor RosePurple { get { return FromHexCode("#6f42c1"); } }
        public static HSBColor RoseRose { get { return FromHexCode("#e83e8c"); } }
        public static HSBColor RoseRed { get { return FromHexCode("#D7221A"); } }
        public static HSBColor RoseOrange { get { return FromHexCode("#fd7e14"); } }
        public static HSBColor RoseYellow { get { return FromHexCode("#ffc107"); } }
        public static HSBColor RoseGreen { get { return FromHexCode("#28a745"); } }
        public static HSBColor RoseTeal { get { return FromHexCode("#20c997"); } }
        public static HSBColor RoseCyan { get { return FromHexCode("#17a2b8"); } }

        public HSBColor(int hue, byte saturation, byte brightness, byte opacity)
        {
            this.hue = 0;
            this.saturation = 0;
            this.brightness = 0;
            this.opacity = 0;
            Hue = hue;
            Saturation = saturation;
            Brightness = brightness;
            Opacity = opacity;
        }

        public HSBColor(int hue, byte saturation, byte brightness) : this(hue, saturation, brightness, 255) { }
        
        public HSBColor(int hue, int saturation, int brightness) : this(hue, (byte)saturation, (byte)brightness, 255) { }
        
        public static HSBColor FromARGB(byte opacity, byte red, byte green, byte blue)
        {
            Color color = Color.FromArgb(opacity, red, green, blue);
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
                relation = (double)channels[priority[1]] / (double)channels[priority[0]];

            int rangePole = 1;
            if(priority[0] == 0 && priority[1] == 2)
            {
                rangePole = -1;
                priority[0] = 3;
            }

            double brightnessFormula = (double)channelMax / 255.0 * 100.0;
            double saturationFormula = 100.0 - ((double)channelMin / 255.0 * 100.0);
            double hueFormula = (double)priority[0] * 120.0 + (double)relation * 60.0 * (double)rangePole;

            byte brightness = (byte)Math.Round(brightnessFormula);
            byte saturation = (byte)Math.Round(saturationFormula);
            int hue = (int)Math.Round(hueFormula);

            return new HSBColor(hue, saturation, brightness, color.A);
        }

        public static HSBColor FromARGB(byte[] colorChannels)
        {
            return FromARGB(colorChannels[0], colorChannels[1], colorChannels[2], colorChannels[3]);
        }

        public static HSBColor FromName(string colorName)
        {
            colorName = colorName.ToLower();

            switch(colorName)
            {
                case "red":
                    return Red;
                case "orange":
                    return Orange;
                case "yellow":
                    return Yellow;
                case "lime":
                    return Lime;
                case "green":
                    return Green;
                case "aqua":
                    return Aqua;
                case "cyan":
                    return Cyan;
                case "indigo":
                    return Indigo;
                case "blue":
                    return Blue;
                case "purple":
                    return Purple;
                case "magenta":
                    return Magenta;
                case "rose":
                    return Rose;
                case "white":
                    return White;
                case "black":
                    return Black;
                case "darkgray":
                    return DarkGray;
                case "lightgray":
                    return LightGray;
                case "transparent":
                    return Transparent;
                case "rosedark":
                    return RoseDark;
                case "rosegray":
                    return RoseGray;
                case "roselight":
                    return RoseLight;
                case "rosedarklight":
                    return RoseDarkLight;
                case "roseblue":
                    return RoseBlue;
                case "roseindigo":
                    return RoseIndigo;
                case "rosepurple":
                    return RosePurple;
                case "roserose":
                    return RoseRose;
                case "rosered":
                    return RoseRed;
                case "roseorange":
                    return RoseOrange;
                case "roseyellow":
                    return RoseYellow;
                case "rosegreen":
                    return RoseGreen;
                case "roseteal":
                    return RoseTeal;
                case "rosecyan":
                    return RoseCyan;
                default:
                    throw new NotValidColorException(nameof(colorName));
            }
        }

        public static HSBColor FromHexCode(string hexCode)
        {
            if (hexCode[0] != '#')
                throw new FormatException("Hexcode out of format");

            switch(hexCode.Length)
            {
                case 4:
                case 7:
                case 9:
                    hexCode = hexCode.Substring(1).ToUpper();
                    if (hexCode.Length == 3)
                    {
                        string dCode = hexCode;
                        hexCode = $"FF{dCode[0]}{dCode[0]}{dCode[1]}{dCode[1]}{dCode[2]}{dCode[2]}";
                    }
                    if (hexCode.Length == 6)
                    {
                        string dCode = hexCode;
                        hexCode = $"FF{dCode[0]}{dCode[1]}{dCode[2]}{dCode[3]}{dCode[4]}{dCode[5]}";
                    }

                    string[] channelHex = { $"{hexCode[0]}{hexCode[1]}", $"{hexCode[2]}{hexCode[3]}", $"{hexCode[4]}{hexCode[5]}", $"{hexCode[6]}{hexCode[7]}" };
                    byte[] channel = channelHex
                        .Select(h => Convert.ToByte(h, 16))
                        .ToArray();

                    return FromARGB(channel);
                default:
                    throw new FormatException("Hexcode out of format");
            }
        }

        private int SetHue(int val)
        {
            while (val < MinHue)
                val += MaxHue;
            while (val >= MaxHue)
                val -= MaxHue;
            return val;
        }

        public byte[] ToARGB()
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

            Color color = Color.FromArgb(Opacity, channels[0], channels[1], channels[2]);
            byte[] output = new byte[4] { color.A, color.R, color.G, color.B };

            return output;
        }

        public string RGBHexCode()
        {
            byte[] channels = ToARGB();
            Color color = Color.FromArgb(channels[0], channels[1], channels[2], channels[3]);
            return $"#{color.R:x2}{color.G:x2}{color.B:x2}";
        }

        public string ARGBHexCode()
        {
            byte[] channels = ToARGB();
            Color color = Color.FromArgb(channels[0], channels[1], channels[2], channels[3]);
            return $"#{color.A:x2}{color.R:x2}{color.G:x2}{color.B:x2}";
        }

        public void Darken(byte val)
        {
            Brightness -= val;
        }

        public void Brighten(byte val)
        {
            Saturation -= val;
        }

        public void SetOpacity(byte val)
        {
            Opacity = val;
        }
    }
}
