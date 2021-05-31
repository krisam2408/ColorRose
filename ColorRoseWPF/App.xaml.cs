﻿using ColorRoseWPF.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ColorRoseWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Terminal.Instance.Navigation = new();
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            base.OnStartup(e);
        }
    }
}
