using FastShare.UI.Shared.Interfaces;
using FastShare.UI.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FastShare.UI.Mobile.Forms
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel _vm = new MainViewModel(App.Current as IApp);

        public MainPage()
        {
            InitializeComponent();
            BindingContext = _vm;
        }
    }
}
