﻿using ColorRoseLib;
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
    /// Interaction logic for MinimizeButtonControl.xaml
    /// </summary>
    public partial class MinimizeButtonControl : UserControl
    {
        public MinimizeButtonControl()
        {
            InitializeComponent();
        }

        public SolidColorBrush DarkOrange { get { return SetDarkOrange(); } }

        public static DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(MinimizeButtonControl));
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private SolidColorBrush SetDarkOrange()
        {
            HSBColor activeColor = HSBColor.RoseOrange;
            activeColor.Darken(16);
            byte[] color = activeColor.ToARGB();
            Color brushColor = Color.FromArgb(color[0], color[1], color[2], color[3]);
            return new SolidColorBrush(brushColor);
        }
    }
}
