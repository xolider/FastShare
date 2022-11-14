using FastShare.UI.Shared.Interfaces;
using FastShare.UI.Shared.ViewModels;
using FastShare.UI.WPF.Interfaces;
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
    /// Logique d'interaction pour MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, INavigablePage
    {
        private MainViewModel _vm = new MainViewModel((IApp)App.Current);

        public MainPage()
        {
            InitializeComponent();
            DataContext = _vm;
        }

        public void NavigatedTo()
        {
            
        }
    }
}
