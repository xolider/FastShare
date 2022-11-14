using FastShare.UI.Shared.Interfaces;
using FastShare.UI.Shared.ViewModels;
using FastShare.UI.WPF.Interfaces;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FastShare.UI.WPF.Pages
{
    /// <summary>
    /// Logique d'interaction pour ReceivePage.xaml
    /// </summary>
    public partial class ReceivePage : Page, INavigablePage
    {
        private ReceiveViewModel _vm = new ReceiveViewModel((IApp)App.Current);

        public ReceivePage()
        {
            InitializeComponent();
            DataContext = _vm;
        }

        public void NavigatedTo()
        {
            var picker = new CommonOpenFileDialog();
            picker.IsFolderPicker = true;
            var result = picker.ShowDialog();

            string? path = null;

            if (result == CommonFileDialogResult.Ok)
            {
                path = picker.FileName;
            }

            _vm.ReceiveFile(path);
        }

        private void TextBlock_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
