using FastShare.UI.Shared.Interfaces;
using FastShare.UI.Shared.ViewModels;
using FastShare.UI.WinUI.Dialogs;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FastShare.UI.WinUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainViewModel vm = new MainViewModel(App.Current as IApp);

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        private async void SettingsClicked(object sender, RoutedEventArgs e)
        {
            var dialog = new SettingsDialog();
            dialog.XamlRoot = this.XamlRoot;
            var result = await dialog.ShowAsync();
        }
    }
}
