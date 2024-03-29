﻿using ColorRoseLib;
using ColorRoseLib.Exceptions;
using ColorRoseWPF.Models;
using System;
using System.Linq;
using System.Windows.Media;

namespace ColorRoseWPF.Core
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited =true)]
    public class ButtonSettingsAttribute : Attribute
    {
        public string Icon { get; set; }
        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }

        public ButtonSettingsAttribute(string icon, string backgroundColor, string textColor)
        {
            Icon = icon;
            Color bgc;
            Color txt;
            try
            {
                bgc = TranslateColor(backgroundColor);
            }
            catch(NotValidColorException)
            {
                HSBColor color = HSBColor.Orange;
                byte[] colorChannels = color.ToARGB();
                bgc = colorChannels.ToMediaColor();
            }
            try
            {
                txt = TranslateColor(textColor);
            }
            catch(Exception)
            {
                HSBColor color = HSBColor.White;
                byte[] colorChannels = color.ToARGB();
                txt = colorChannels.ToMediaColor();
            }
            BackgroundColor = bgc;
            TextColor = txt;
        }

        private static Color TranslateColor(string colorParam)
        {
            Color output;

            try
            {
                if(colorParam[0] == '#')
                {
                    switch(colorParam.Length)
                    {
                        case 4:
                        case 7:
                        case 9:
                            return TranslateHexCode(colorParam);
                        default:
                            throw new FormatException("Color hexcode out of format");
                    }
                }

                HSBColor color = HSBColor.FromName(colorParam);
                byte[] colorChannels = color.ToARGB();
                output = colorChannels.ToMediaColor();
            }
            catch(Exception e)
            {
                throw new NotValidColorException(colorParam, e);
            }

            return output;
        }

        private static Color TranslateHexCode(string hexCode)
        {
            hexCode = hexCode.Substring(1).ToUpper();
            if(hexCode.Length == 3)
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

            return Color.FromArgb(channel[0], channel[1], channel[2], channel[3]);
        }
    }
}