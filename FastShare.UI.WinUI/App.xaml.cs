using FastShare.UI.Shared.Components;
using FastShare.UI.Shared.Interfaces;
using FastShare.UI.WinUI.Pages;
using FastShare.UI.WinUI.Storage;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FastShare.UI.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application, IApp
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            (m_window.Content as Frame).Navigate(typeof(MainPage));
            m_window.Activate();
        }

        /// <summary>
        /// Runs the code of this Action in the DispatcherQueue (desktop UI thread)
        /// </summary>
        /// <param name="action">Code to be executed on UI thread</param>
        public void RunOnUIThread(Action action)
        {
            m_window.DispatcherQueue.TryEnqueue(() => action());
        }

        private Window m_window;

        IWindow IApp.CurrentWindow => m_window as IWindow; //Implementation of the navigable window
        IStorage IApp.Storage => FSStorage.Instance;
    }
}
