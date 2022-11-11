using FastShare.UI.Shared.Interfaces;
using FastShare.UI.WinUI.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FastShare.UI.WinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window, IWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
            Resize(650, 450);
            this.Title = "FastShare";
            LoadIcon();
        }

        void IWindow.NavigateToPage(IWindow.NavPage page)
        {
            Type targetPage = null;
            switch(page)
            {
                case IWindow.NavPage.MAIN:
                    targetPage = typeof(MainPage);
                    break;
                case IWindow.NavPage.RECEIVE:
                    targetPage = typeof(ReceivePage);
                    break;
            }

            rootFrame.Navigate(targetPage);

        }

        private void Resize(int width, int height)
        {
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

            appWindow.Resize(new Windows.Graphics.SizeInt32 { Width = width, Height = height });
        }

        private void LoadIcon()
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

            var assembly = Assembly.GetEntryAssembly();
            var rName = assembly.GetManifestResourceNames().FirstOrDefault(s => s.EndsWith("fastshare.ico", StringComparison.InvariantCulture));
            var icon = new Icon(assembly.GetManifestResourceStream(rName));

            appWindow.SetIcon(Microsoft.UI.Win32Interop.GetIconIdFromIcon(icon.Handle));
        }
    }
}
