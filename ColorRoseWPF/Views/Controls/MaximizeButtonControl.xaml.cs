using ColorRoseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorRoseWPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for MaximizeButtonControl.xaml
    /// </summary>
    public partial class MaximizeButtonControl : UserControl
    {
        public MaximizeButtonControl()
        {
            InitializeComponent();
        }

        public SolidColorBrush DarkGruen { get { return SetDarkGruen(); } }

        public static DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(MaximizeButtonControl));
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private SolidColorBrush SetDarkGruen()
        {
            HSBColor activeColor = HSBColor.RoseGreen;
            activeColor.Darken(16);
            byte[] color = activeColor.ToARGB();
            Color brushColor = Color.FromArgb(color[0], color[1], color[2], color[3]);
            return new SolidColorBrush(brushColor);
        }
    }
}
