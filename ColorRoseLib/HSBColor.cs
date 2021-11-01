using ColorRoseLib.Exceptions;
using System;
using System.Linq;

namespace ColorRoseLib
{
    /// <summary>
    /// A Color representation using the HSB (Hue, Saturation, Brightness) standard.
    /// </summary>
    public struct HSBColor
    {
        private int hue;
        /// <summary>
        /// Determines the tint of the color.
        /// 0º -> Red.
        /// 120º -> Green.
        /// 240º -> Blue.
        /// 360º -> Red.
        /// </summary>
        public int Hue
        {
            get { return hue; }
            set { hue = SetHue(value); }
        }

        private byte saturation;
        /// <summary>
        /// Determines the richness of the color, varying from white to degrees of gray until the defined color by hue.
        /// </summary>
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
        /// <summary>
        /// Determines how bright the color is, varying from black to the defined color by hue.
        /// </summary>
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
        /// <summary>
        /// Determines how opaque the color is, varying from transparent to a full opaque color.
        /// </summary>
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

        /// <summary>
        /// Minimum value for Hue. Returns 0.
        /// </summary>
        public static int MinHue { get { return 0; } }
        /// <summary>
        /// Minimum value for Hue. Returns 360.
        /// </summary>
        public static int MaxHue { get { return 360; } }
        /// <summary>
        /// Minimum value for Saturation. Returns 0.
        /// </summary>
        public static byte MinSaturation { get { return 0; } }
        /// <summary>
        /// Minimum value for Saturation. Returns 100.
        /// </summary>
        public static byte MaxSaturation { get { return 100; } }
        /// <summary>
        /// Minimum value for Brightness. Returns 0.
        /// </summary>
        public static byte MinBrightness { get { return 0; } }
        /// <summary>
        /// Minimum value for Brightness. Returns 100.
        /// </summary>
        public static byte MaxBrightness { get { return 100; } }
        /// <summary>
        /// Minimum value for Opacity. Returns 0.
        /// </summary>
        public static byte MinOpacity { get { return 0; } }
        /// <summary>
        /// Minimum value for Opacity. Returns 255.
        /// </summary>
        public static byte MaxOpacity { get { return 255; } }

        /// <summary>
        /// HSB Red color
        /// </summary>
        public static HSBColor Red { get { return new HSBColor(0, 100, 100); } }
        /// <summary>
        /// HSB Orange color
        /// </summary>
        public static HSBColor Orange { get { return new HSBColor(30, 100, 100); } }
        /// <summary>
        /// HSB Yellow color
        /// </summary>
        public static HSBColor Yellow { get { return new HSBColor(60, 100, 100); } }
        /// <summary>
        /// HSB Lime color
        /// </summary>
        public static HSBColor Lime { get { return new HSBColor(90, 100, 100); } }
        /// <summary>
        /// HSB Green color
        /// </summary>
        public static HSBColor Green { get { return new HSBColor(120, 100, 100); } }
        /// <summary>
        /// HSB Aqua color
        /// </summary>
        public static HSBColor Aqua { get { return new HSBColor(150, 100, 100); } }
        /// <summary>
        /// HSB Cyan color
        /// </summary>
        public static HSBColor Cyan { get { return new HSBColor(180, 100, 100); } }
        /// <summary>
        /// HSB Indigo color
        /// </summary>
        public static HSBColor Indigo { get { return new HSBColor(210, 100, 100); } }
        /// <summary>
        /// HSB Blue color
        /// </summary>
        public static HSBColor Blue { get { return new HSBColor(240, 100, 100); } }
        /// <summary>
        /// HSB Purple color
        /// </summary>
        public static HSBColor Purple { get { return new HSBColor(270, 100, 100); } }
        /// <summary>
        /// HSB Magenta color
        /// </summary>
        public static HSBColor Magenta { get { return new HSBColor(300, 100, 100); } }
        /// <summary>
        /// HSB Rose color
        /// </summary>
        public static HSBColor Rose { get { return new HSBColor(330, 100, 100); } }
        /// <summary>
        /// HSB White color
        /// </summary>
        public static HSBColor White { get { return new HSBColor(0, 0, 100); } }
        /// <summary>
        /// HSB Black color
        /// </summary>
        public static HSBColor Black { get { return new HSBColor(0, 0, 0); } }
        /// <summary>
        /// HSB Gray color
        /// </summary>
        public static HSBColor Gray { get { return new HSBColor(0, 0, 50); } }
        /// <summary>
        /// HSB DarkGray color
        /// </summary>
        public static HSBColor DarkGray { get { return new HSBColor(0, 0, 33); } }
        /// <summary>
        /// HSB Light Gray color
        /// </summary>
        public static HSBColor LightGray { get { return new HSBColor(0, 0, 66); } }

        /// <summary>
        /// HSB Transparent color.
        /// </summary>
        public static HSBColor Transparent { get { return new HSBColor(0, 0, 0, 0); } }

        /// <summary>
        /// Custom HSB Dark color
        /// </summary>
        public static HSBColor RoseDark { get { return FromHexCode("#24292E"); } }
        /// <summary>
        /// Custom HSB Gray color
        /// </summary>
        public static HSBColor RoseGray { get { return FromHexCode("#343a40"); } }
        /// <summary>
        /// Custom HSB Light color
        /// </summary>
        public static HSBColor RoseLight { get { return FromHexCode("#f8f9fa"); } }
        /// <summary>
        /// Custom HSB Dark Light color
        /// </summary>
        public static HSBColor RoseDarkLight { get { return FromHexCode("#b8b9ba"); } }
        /// <summary>
        /// Custom HSB Blue color
        /// </summary>
        public static HSBColor RoseBlue { get { return FromHexCode("#007bff"); } }
        /// <summary>
        /// Custom HSB Indigo color
        /// </summary>
        public static HSBColor RoseIndigo { get { return FromHexCode("#6610f2"); } }
        /// <summary>
        /// Custom HSB Purple color
        /// </summary>
        public static HSBColor RosePurple { get { return FromHexCode("#6f42c1"); } }
        /// <summary>
        /// Custom HSB Rose color
        /// </summary>
        public static HSBColor RoseRose { get { return FromHexCode("#e83e8c"); } }
        /// <summary>
        /// Custom HSB Red color
        /// </summary>
        public static HSBColor RoseRed { get { return FromHexCode("#D7221A"); } }
        /// <summary>
        /// Custom HSB Orange color
        /// </summary>
        public static HSBColor RoseOrange { get { return FromHexCode("#fd7e14"); } }
        /// <summary>
        /// Custom HSB Yellow color
        /// </summary>
        public static HSBColor RoseYellow { get { return FromHexCode("#ffc107"); } }
        /// <summary>
        /// Custom HSB Green color
        /// </summary>
        public static HSBColor RoseGreen { get { return FromHexCode("#28a745"); } }
        /// <summary>
        /// Custom HSB Teal color
        /// </summary>
        public static HSBColor RoseTeal { get { return FromHexCode("#20c997"); } }
        /// <summary>
        /// Custom HSB Cyan color
        /// </summary>
        public static HSBColor RoseCyan { get { return FromHexCode("#17a2b8"); } }

        /// <summary>
        /// A new HSBColor.
        /// </summary>
        /// <param name="hue">Hue value.</param>
        /// <param name="saturation">Saturation value.</param>
        /// <param name="brightness">Brightness value.</param>
        /// <param name="opacity">Opacity value.</param>
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

        /// <summary>
        /// A new HSBColor.
        /// </summary>
        /// <param name="hue">Hue value.</param>
        /// <param name="saturation">Saturation value.</param>
        /// <param name="brightness">Brightness value.</param>
        public HSBColor(int hue, byte saturation, byte brightness) : this(hue, saturation, brightness, 255) { }

        /// <summary>
        /// A new HSBColor.
        /// </summary>
        /// <param name="hue">Hue value.</param>
        /// <param name="saturation">Saturation value.</param>
        /// <param name="brightness">Brightness value.</param>
        public HSBColor(int hue, int saturation, int brightness) : this(hue, (byte)saturation, (byte)brightness, 255) { }
        
        /// <summary>
        /// Returns a HSBColor from the ARGB channels of another color.
        /// </summary>
        /// <param name="opacity">Opacity value.</param>
        /// <param name="red">Red value.</param>
        /// <param name="green">Green value.</param>
        /// <param name="blue">Blue value.</param>
        /// <returns>A new HSBColor from the values of another ARGB Color.</returns>
        public static HSBColor FromARGB(byte opacity, byte red, byte green, byte blue)
        {
            byte[] channels = { red, green, blue };
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

            return new HSBColor(hue, saturation, brightness, opacity);
        }

        /// <summary>
        /// Returns a HSBColor from the ARGB channels of another color.
        /// </summary>
        /// <param name="colorChannels">Byte array containing the color channels of another ARGB color.</param>
        /// <returns>A new HSBColor from the values of another ARGB Color.</returns>
        public static HSBColor FromARGB(byte[] colorChannels)
        {
            if(colorChannels.Length == 4)
                return FromARGB(colorChannels[0], colorChannels[1], colorChannels[2], colorChannels[3]);
            if(colorChannels.Length == 3)
                return FromARGB(255, colorChannels[0], colorChannels[1], colorChannels[2]);

            throw new NotValidColorByteArrayException();
        }

        /// <summary>
        /// Returns a HSBColor from a known color name. E.g. "Blue".
        /// </summary>
        /// <param name="colorName">A known color name.</param>
        /// <returns>A new HSBColor based on the known color name.</returns>
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

        /// <summary>
        /// Returns a HSBColor from a color Hexcode.
        /// </summary>
        /// <param name="hexCode">A Hexcode. It can be in #RGB, #RRGGBB or in #AARRGGBB format.</param>
        /// <returns>A new HSBColor based on the Hexcode.</returns>
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

        private int SetSecondarySpectrum(int sector)
        {
            int[] limits = { 60, 180, 300 };
            if (limits.Contains(Hue))
                return 60;

            return Hue - (sector * 60);
        }

        private byte SetSecondarySpectrumByte(int spectrum)
        {
            double byteStep = (double)byte.MaxValue / 60.0;
            byte output = (byte)Math.Round(spectrum * byteStep, MidpointRounding.AwayFromZero);

            return output;
        }

        private byte SetChannelBrightnessByte(byte channel)
        {
            double brightnessCoefficient = (double)Brightness / 100.0;
           
            byte output = (byte)Math.Round(channel * brightnessCoefficient, MidpointRounding.AwayFromZero);

            return output;
        }

        /// <summary>
        /// Returns the byte channels of the color in ARGB format.
        /// </summary>
        /// <returns>Returns a byte[] that contains the ARGB channels of a color.</returns>
        public byte[] ToARGB()
        {
            byte[] channels = { 0, 0, 0 };
            int sector = Hue / 60;
            int secSpectrum = SetSecondarySpectrum(sector);
            byte secByte = SetSecondarySpectrumByte(secSpectrum);

            switch (sector)
            {
                // Red
                case 0:
                    channels[0] = byte.MaxValue;
                    channels[1] = secByte;
                    break;
                case 5:
                    channels[0] = byte.MaxValue;
                    channels[2] = secByte == byte.MaxValue ? byte.MaxValue : (byte)(byte.MaxValue - secByte);
                    break;
                // Green
                case 1:
                    channels[1] = byte.MaxValue;
                    channels[0] = secByte == byte.MaxValue ? byte.MaxValue : (byte)(byte.MaxValue - secByte);
                    break;
                case 2:
                    channels[1] = byte.MaxValue;
                    channels[2] = secByte;
                    break;
                // Blue
                case 3:
                    channels[2] = byte.MaxValue;
                    channels[1] = secByte == byte.MaxValue ? byte.MaxValue : (byte)(byte.MaxValue - secByte);
                    break;
                case 4:
                    channels[2] = byte.MaxValue;
                    channels[0] = secByte;
                    break;
            }

            channels[0] = SetChannelBrightnessByte(channels[0]);
            channels[1] = SetChannelBrightnessByte(channels[1]);
            channels[2] = SetChannelBrightnessByte(channels[2]);

            byte[] output = new byte[4] { Opacity, channels[0], channels[1], channels[2] };

            return output;
        }

        /// <summary>
        /// Prints the RGB Hexcode of the color.
        /// </summary>
        /// <returns>Color Hexcode in #RRGGBB format.</returns>
        public string RGBHexCode()
        {
            byte[] channels = ToARGB();
            return $"#{channels[1]:x2}{channels[2]:x2}{channels[3]:x2}";
        }

        /// <summary>
        /// Prints the ARGB Hexcode of the color.
        /// </summary>
        /// <returns>Color Hexcode in #AARRGGBB format.</returns>
        public string ARGBHexCode()
        {
            byte[] channels = ToARGB();
            return $"#{channels[0]:x2}{channels[1]:x2}{channels[2]:x2}{channels[3]:x2}";
        }

        /// <summary>
        /// Lowers the brightness of the HSBColor.
        /// </summary>
        /// <param name="value">Value by which the brightness of the color will be lowered</param>
        public void Darken(byte value)
        {
            Brightness -= value;
        }

        /// <summary>
        /// Lowers the saturation of the HSBColor.
        /// </summary>
        /// <param name="value">Value by which the saturation of the color will be lowered.</param>
        public void Desaturate(byte value)
        {
            Saturation -= value;
        }

        /// <summary>
        /// Changes the opacity of a HSBColor.
        /// </summary>
        /// <param name="val">New opacity value.</param>
        public void SetOpacity(byte opacity)
        {
            Opacity = opacity;
        }
    }
}
