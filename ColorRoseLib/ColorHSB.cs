using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Windows.UI;

namespace ColorRoseLib
{
    public class ColorHSB
    {
    
        #region Fields
        private int hue;
        private byte sat;
        private byte brg;
        #endregion

        #region Properties
        public int Hue
        {
            get
            {
                return hue;
            }
            set
            {
                if (value <= 360 && value >= 0) hue = value;
                else throw new ArgumentOutOfRangeException("hue", hue, "Value must be an integer between 0 and 360.");
            }
        }
        public byte Saturation
        {
            get
            {
                return sat;
            }
            set
            {
                if (value <= 100 && value >= 0) sat = value;
                else throw new ArgumentOutOfRangeException("sat", sat, "Saturation must be an integer between 0 and 100.");
            }
        }
        public byte Brightness
        {
            get
            {
                return brg;
            }
            set
            {
                if (value <= 100 && value >= 0) brg = value;
                else throw new ArgumentOutOfRangeException("brg", brg, "Brightness must be an integer between 0 and 100.");
            }
        }
        public byte Alpha { get; set; }
        public System.Drawing.Color RGB { get; private set; }
        public string RGBHexCode
        {
            get
            {
                return string.Format("#{0}{1}{2}", RGB.R.ToString("x2"), RGB.G.ToString("x2"), RGB.B.ToString("x2"));
            }
        }
        public string RGBAHexCode
        {
            get
            {
                return string.Format("#{0}{1}{2}{3}", RGB.R.ToString("x2"), RGB.G.ToString("x2"), RGB.B.ToString("x2"), RGB.A.ToString("x2"));
            }
        }
        public Windows.UI.Color RGBUI
        {
            get
            {
                return new Windows.UI.Color()
                {
                    A = RGB.A,
                    R = RGB.R,
                    G = RGB.G,
                    B = RGB.B
                };
            }
        }

        #endregion

        #region Static Properties
        public static ColorHSB Red
        {
            get
            {
                return new ColorHSB(0, 100, 100);
            }
        }
        public static ColorHSB Green
        {
            get
            {
                return new ColorHSB(120, 100, 100);
            }
        }
        public static ColorHSB Blue
        {
            get
            {
                return new ColorHSB(240, 100, 100);
            }
        }
        public static ColorHSB Yellow
        {
            get
            {
                return new ColorHSB(60, 100, 100);
            }
        }
        public static ColorHSB Cyan
        {
            get
            {
                return new ColorHSB(180, 100, 100);
            }
        }
        public static ColorHSB Magenta
        {
            get
            {
                return new ColorHSB(300, 100, 100);
            }
        }
        public static ColorHSB Orange
        {
            get
            {
                return new ColorHSB(30, 100, 100);
            }
        }
        public static ColorHSB Lime
        {
            get
            {
                return new ColorHSB(90, 100, 100);
            }
        }
        public static ColorHSB Aqua
        {
            get
            {
                return new ColorHSB(150, 100, 100);
            }
        }
        public static ColorHSB Indigo
        {
            get
            {
                return new ColorHSB(180, 100, 100);
            }
        }
        public static ColorHSB Purple
        {
            get
            {
                return new ColorHSB(300, 100, 100);
            }
        }
        public static ColorHSB Rose
        {
            get
            {
                return new ColorHSB(330, 100, 100);
            }
        }
        #endregion

        #region Constructors
        public ColorHSB()
        {
            Hue = 0;
            Saturation = 100;
            Brightness = 100;
            Alpha = 255;
            RGB = ToRGB();
        }

        public ColorHSB(System.Drawing.Color color)
        {
            Hue = (int)color.GetHue();
            Saturation = (byte)(color.GetSaturation() * 100);
            Brightness = (byte)(color.GetBrightness() * 100);
            Alpha = color.A;
            RGB = color;
        }

        public ColorHSB(Windows.UI.Color color)
        {
            RGB = System.Drawing.Color.FromArgb(color.R, color.G, color.B);
            Hue = (int)RGB.GetHue();
            Saturation = (byte)RGB.GetSaturation();
            Brightness = (byte)RGB.GetBrightness();
            Alpha = (byte)RGB.A;
        }

        public ColorHSB(int h, int s, int b)
        {
            Hue = h;
            Saturation = (byte)s;
            Brightness = (byte)b;
            Alpha = 255;
            RGB = ToRGB();
        }

        public ColorHSB(byte a, int h, int s, int b)
        {
            Hue = h;
            Saturation = (byte)s;
            Brightness = (byte)b;
            Alpha = a;
            RGB = ToRGB();
        }

        public ColorHSB(int a, int h, int s, int b)
        {
            Hue = h;
            Saturation = (byte)s;
            Brightness = (byte)b;
            if (a >= 0 && a <= 255) Alpha = (byte)a;
            else throw new ArgumentOutOfRangeException("Alpha", Alpha, "Alpha must be an integer between 0 and 255");
            RGB = ToRGB();
        }
        #endregion

        #region Methods
        public System.Drawing.Color ToRGB()
        {
            byte[] cha = { 0, 0, 0 };
            byte quad = (byte)(Hue == 360?0:Hue/120);
            byte spectrum = (byte)(Hue == 360?0:Hue - quad * 120);

            double[] x = new double[2];
            x[0] = 1 - (double)spectrum/120;
            x[1] = (double)spectrum/120;

            double iMax = x.Max(f => f);

            double coef = 255 / iMax;

            cha[(quad > 2 ? 0 : quad)] = (byte)(x[0] * coef);
            cha[(quad >= 2 ? 0 : quad + 1)] = (byte)(x[1] * coef);
            
            double brgCoef = (double)Brightness / 100;

            cha[0] = (byte)(cha[0] * brgCoef);
            cha[1] = (byte)(cha[1] * brgCoef);
            cha[2] = (byte)(cha[2] * brgCoef);

            byte brgByte = (byte)(255 * Brightness / 100);
            byte satByte = (byte)(255 * (100 - Saturation) / 100);

            for (byte i = 0; i < 3; i++)
            {
                if(cha[i] < brgByte)
                {
                    if (satByte > brgByte) satByte = brgByte;
                    cha[i] += satByte;
                }
                
            }
            

            return System.Drawing.Color.FromArgb(Alpha, cha[0], cha[1], cha[2]);

          
        }

        #endregion
    }
}
