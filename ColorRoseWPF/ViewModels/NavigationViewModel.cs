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

        public ICommand AppCloseCommand { get { return new RelayCommand(e => AppClose()); } }
        public ICommand AppMinimizeCommand { get { return new RelayCommand(e => AppMinimize()); } }
        public ICommand AppMaximizeCommand { get { return new RelayCommand(e => AppMaximize()); } }
        public ICommand AppMoveDownCommand { get { return new RelayCommand(e => AppMove((MouseEventArgs)e)); } }

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
                    HSBColor startingColor = HSBColor.Red;
                    //startingColor.Darken(30);
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

                    item.TextColor = new SolidColorBrush(buttonSetting.TextColor);

                    byte[] bgcColorChannels = buttonSetting.BackgroundColor.ToChannelsBytes();
                    HSBColor middleColor = HSBColor.FromARGB(bgcColorChannels);
                    middleColor.Desaturate(56);
                   
                    item.BackgroundColor = new LinearGradientBrush(
                        new GradientStopCollection(new List<GradientStop>
                        {
                            new GradientStop(buttonSetting.BackgroundColor, 0.5),
                            new GradientStop(middleColor.ToARGB().ToMediaColor(), 0.75),
                            new GradientStop(buttonSetting.BackgroundColor, 1)
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

        private void AppClose()
        {
            Application.Current.Shutdown();
        }

        private void AppMinimize()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void AppMaximize()
        {
            if(Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
                return;
            }

            Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }

        private void AppMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                Application.Current.MainWindow.DragMove();
        }


    }
}
