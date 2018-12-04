using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ColorRose.Views;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ColorRose
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public static Color Color;

        public MainPage()
        {
            this.InitializeComponent();
            WindowFrame.Navigate(typeof(ColorHSBPage));
        }

        public static string ColorHexCode()
        {
            return string.Format("#{0}{1}{2}", Color.R.ToString("x2"), Color.G.ToString("x2"), Color.B.ToString("x2"));
        }

        private void HamburguerButton_ContextCanceled(UIElement sender, RoutedEventArgs args)
        {
            SplitDiv.IsPaneOpen = !SplitDiv.IsPaneOpen;
        }

        private void HamburguerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CmdList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
