using ColorRoseWPF.Models.Abstracts;
using ColorRoseWPF.Models;
using Color = System.Drawing.Color;
using System.Windows.Media;

namespace ColorRoseWPF.ViewModels
{
    public class SingleColorViewModel:BaseViewModel,INavegable
    {
        private Brush rectColor;
        public Brush RectColor { get { return rectColor; } set { SetValue(ref rectColor, value); } }

        public SingleColorViewModel()
        {
            RectColor = new SolidColorBrush(Color.Purple.ToMediaColor());
        }
    }
}
