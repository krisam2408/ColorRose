using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace ColorRoseWPF.Models
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            ObservableCollection<T> output = new();

            foreach (T t in list)
                output.Add(t);

            return output;
        }

        public static Color ToMediaColor(this byte[] colorChannels)
        {
            return Color.FromArgb(colorChannels[0], colorChannels[1], colorChannels[2], colorChannels[3]);
        }

        public static byte[] ToChannelsBytes(this Color color)
        {
            return new byte[] { color.A, color.R, color.G, color.B };
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
    }
}
