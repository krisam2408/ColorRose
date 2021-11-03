using ColorRoseLib;
using System;

namespace RGBColorTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            int step = 2; // 0 -> 11
            int range = 30;
            int iHue = step*range;

            int saturation = 33;
            int brightness = 67;

            for(int i = iHue; i <= (iHue + range); i++)
            {
                HSBColor color = new HSBColor(i, saturation, brightness);
                string num = i < 10 ? $"00{i}º" : i < 100  ? $"0{i}º" : $"{i}º";
                Console.WriteLine($"{num} => {color.RGBHexCode()}");
            }

            Console.ReadKey();
        }
    }
}
