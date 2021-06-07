using System.Windows;
using System.Windows.Input;

namespace ColorRoseWPF.Core
{
    public class KeyboardBehaviour
    {
        public static readonly DependencyProperty UpKeyCommandProperty = DependencyProperty.RegisterAttached("UpKeyCommand", typeof(ICommand), typeof(KeyboardBehaviour), new FrameworkPropertyMetadata(new PropertyChangedCallback(UpKeyCommandChanged)));
        public static readonly DependencyProperty DownKeyCommandProperty = DependencyProperty.RegisterAttached("DownKeyCommand", typeof(ICommand), typeof(KeyboardBehaviour), new FrameworkPropertyMetadata(new PropertyChangedCallback(DownKeyCommandChanged)));

        private static void UpKeyCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            element.KeyDown += new KeyEventHandler(UpKeyCommand);
        }

        private static void UpKeyCommand(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.W)
            {
                FrameworkElement element = (FrameworkElement)sender;
                ICommand command = GetUpKeyCommand(element);
                command.Execute(e);
            }
        }

        public static void SetUpKeyCommand(FrameworkElement element, ICommand command)
        {
            element.SetValue(UpKeyCommandProperty, command);
        }

        public static ICommand GetUpKeyCommand(FrameworkElement element)
        {
            return (ICommand)element.GetValue(UpKeyCommandProperty);
        }

        private static void DownKeyCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            element.KeyUp += new KeyEventHandler(DownKeyCommand);
        }

        private static void DownKeyCommand(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Down || e.Key == Key.S)
            {
                FrameworkElement element = (FrameworkElement)sender;
                ICommand command = GetDownKeyCommand(element);
                command.Execute(e);
            }
        }

        public static void SetDownKeyCommand(FrameworkElement element, ICommand command)
        {
            element.SetValue(DownKeyCommandProperty, command);
        }

        public static ICommand GetDownKeyCommand(FrameworkElement element)
        {
            return (ICommand)element.GetValue(DownKeyCommandProperty);
        }
    }
}
