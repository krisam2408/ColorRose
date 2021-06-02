using System.Windows;
using System.Windows.Input;

namespace ColorRoseWPF.Core
{
    public class LayoutBehaviour
    {
        public static readonly DependencyProperty ResizeCommandProperty = DependencyProperty.RegisterAttached("ResizeCommand", typeof(ICommand), typeof(LayoutBehaviour), new FrameworkPropertyMetadata(new PropertyChangedCallback(ResizeCommandChanged)));
        public static readonly DependencyProperty LoadedCommandProperty = DependencyProperty.RegisterAttached("LoadedCommand", typeof(ICommand), typeof(LayoutBehaviour), new FrameworkPropertyMetadata(new PropertyChangedCallback(LoadedCommandChanged)));

        private static void ResizeCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            element.SizeChanged += new SizeChangedEventHandler(ResizeCommand);
        }

        private static void ResizeCommand(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            ICommand command = GetResizeCommand(element);
            command.Execute(e);
        }

        public static void SetResizeCommand(FrameworkElement element, ICommand command)
        {
            element.SetValue(ResizeCommandProperty, command);
        }

        public static ICommand GetResizeCommand(FrameworkElement element)
        {
            return (ICommand)element.GetValue(ResizeCommandProperty);
        }

        private static void LoadedCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            element.Loaded += new RoutedEventHandler(LoadedCommand);
        }

        private static void LoadedCommand(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            ICommand command = GetLoadedCommand(element);
            command.Execute(e);
        }


        public static void SetLoadedCommand(FrameworkElement element, ICommand command)
        {
            element.SetValue(LoadedCommandProperty, command);
        }

        public static ICommand GetLoadedCommand(FrameworkElement element)
        {
            return (ICommand)element.GetValue(LoadedCommandProperty);
        }

    }
}