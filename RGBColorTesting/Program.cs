using ColorRoseLib;
using System;

namespace RGBColorTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            int step = 0; // 0 -> 11
            int range = 30;
            int iHue = step*range;

            int saturation = 100;
            int brightness = 0;

            for(int i = iHue; i <= (iHue + range); i++)
            {
                HSBColor color = new HSBColor(i, saturation, brightness);
                string num = i < 10 ? $"0{i}º" : $"{i}º";
                Console.WriteLine($"{num} => {color.RGBHexCode()}");
            }

            Console.ReadKey();
        }
    }
}
