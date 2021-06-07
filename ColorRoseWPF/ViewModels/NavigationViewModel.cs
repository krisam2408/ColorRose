using ColorRoseWPF.Models;
using ColorRoseWPF.Core;
using ColorRoseWPF.Core.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MouseAction = ColorRoseWPF.Core.MouseAction;
using ColorRoseLib;

namespace ColorRoseWPF.ViewModels
{
    public class NavigationViewModel : BaseViewModel
    {
        private ObservableCollection<MenuItem> navDestinations;
        public ObservableCollection<MenuItem> NavDestinations { get { return navDestinations; }set { SetValue(ref navDestinations, value); } }

        private MenuItem destination;
        public MenuItem Destination
        {
            get { return destination; } 
            set
            {
                if (SetValue(ref destination, value))
                    Navigation();
            }
        }

        private bool IsWindowBeingDragged { get; set; }
        private Point WindowPosition { get; set; }

        public ICommand AppCloseCommand { get { return new RelayCommand(e => AppClose(e)); } }
        public ICommand AppMinimizeCommand { get { return new RelayCommand(e => AppMinimize(e)); } }
        public ICommand AppMaximizeCommand { get { return new RelayCommand(e => AppMaximize(e)); } }
        public ICommand AppMoveUpCommand { get { return new RelayCommand(e => AppMove(MouseAction.Up, (MouseEventArgs)e)); } }
        public ICommand AppMoveDownCommand { get { return new RelayCommand(e => AppMove(MouseAction.Down, (MouseEventArgs)e)); } }
        public ICommand AppMoveDragCommand { get { return new RelayCommand(e => AppMove(MouseAction.Drag, (MouseEventArgs)e)); } }
        public ICommand AppMoveLeaveCommand { get { return new RelayCommand(e => AppMove(MouseAction.Leave, (MouseEventArgs)e)); } }

        private string maximizeButtonIcon;
        public string MaximizeButtonIcon { get { return maximizeButtonIcon; } set { SetValue(ref maximizeButtonIcon, value); } }
        
        public NavigationViewModel()
        {
            MaximizeButtonIcon = "0";
            IsEnabled = SetNavMenu();
        }

        private void Navigation()
        {
            switch (Destination.Key)
            {
                case 0:
                    HSBColor startingColor = HSBColor.Purple;
                    startingColor.Darken(30);
                    Terminal.Instance.SingleColor = new(startingColor);
                    break;
                case 1:
                    MessageBox.Show("I'm not working");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Destination.Key), "Destination doesn't exist");
            }
        }

        private bool SetNavMenu()
        {
            List<MenuItem> output = new();

            Type type = typeof(Terminal);
            PropertyInfo[] terminalProps = type.GetProperties()
                .Where(p => p.PropertyType.GetInterfaces().Contains(typeof(INavegable)))
                .ToArray();

            int i = 0;
            foreach (PropertyInfo p in terminalProps)
            {
                LandingPageAttribute landingPage = (LandingPageAttribute)p.GetCustomAttributes(typeof(LandingPageAttribute)).FirstOrDefault();
                if(landingPage.IsEnabled)
                {
                    ButtonSettingsAttribute buttonSetting = (ButtonSettingsAttribute)p.GetCustomAttributes(typeof(ButtonSettingsAttribute)).FirstOrDefault();
                    MenuItem item = new(i, landingPage.Display, landingPage.IsSelected);
                    item.SetContet($"Views/{landingPage.Page}.xaml");
                    item.TextColor = new SolidColorBrush(buttonSetting.TextColor.ToMediaColor());
                    HSBColor middleColor = HSBColor.FromARGBColor(buttonSetting.BackgroundColor);
                    middleColor.Brighten(56);
                    item.BackgroundColor = new LinearGradientBrush(
                        new GradientStopCollection(new List<GradientStop>
                        {
                            new GradientStop(buttonSetting.BackgroundColor.ToMediaColor(), 0.5),
                            new GradientStop(middleColor.ToARGBColor().ToMediaColor(), 0.75),
                            new GradientStop(buttonSetting.BackgroundColor.ToMediaColor(), 1)
                        }),
                        new Point(0, 0),
                        new Point(0, 1)
                    );

                    output.Add(item);
                    i++;
                }
            }

            output.Reverse();
            NavDestinations = output.ToObservableCollection();

            if (NavDestinations.Count > 0)
            {
                Destination = NavDestinations.FirstOrDefault(m => m.DefaultSelected);
                if(Destination == null)
                    Destination = NavDestinations[0];
            }

            if (Destination != null)
                return true;

            return false;
        }

        private void AppClose(object parameter)
        {
            Application.Current.Shutdown();
        }

        private void AppMinimize(object parameter)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void AppMaximize(object parameter)
        {
            if(Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                MaximizeButtonIcon = "0";
                Application.Current.MainWindow.WindowState = WindowState.Normal;
                return;
            }

            MaximizeButtonIcon = "1";
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }

        private void AppMove(MouseAction action, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(Application.Current.MainWindow);
            if(action == MouseAction.Down)
            {
                IsWindowBeingDragged = true;
                WindowPosition = mousePosition;
            }

            if (action == MouseAction.Leave || action == MouseAction.Up)
                IsWindowBeingDragged = false;

            if(action == MouseAction.Drag && IsWindowBeingDragged)
            {
                Application.Current.MainWindow.Left += mousePosition.X - WindowPosition.X;
                Application.Current.MainWindow.Top += mousePosition.Y - WindowPosition.Y;
            }
        }


    }
}
