using FastShare.UI.Shared.Components;
using FastShare.UI.Shared.Interfaces;
using FastShare.UI.WPF.Interfaces;
using FastShare.UI.WPF.Pages;
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

namespace FastShare.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigateToPage(NavPage.MAIN);
        }

        public void NavigateToPage(NavPage page)
        {
            Page targetPage = null;
            switch(page)
            {
                case NavPage.RECEIVE:
                    targetPage = new ReceivePage();
                    break;
                case NavPage.SEND:
                    targetPage = new MainPage();
                    break;
                case NavPage.MAIN:
                    targetPage = new MainPage();
                    break;
            }

            rootFrame.Navigate(targetPage);
            ((INavigablePage)targetPage!).NavigatedTo();
        }
    }
}
