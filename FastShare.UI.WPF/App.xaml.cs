using FastShare.UI.Shared.Components;
using FastShare.UI.Shared.Interfaces;
using FastShare.UI.WPF.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FastShare.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IApp
    {

        IWindow IApp.CurrentWindow => (IWindow)this.MainWindow;

        public App()
        {
            this.InitializeComponent();
        }

        public void RunOnUIThread(Action action)
        {
            action();
        }
    }
}
