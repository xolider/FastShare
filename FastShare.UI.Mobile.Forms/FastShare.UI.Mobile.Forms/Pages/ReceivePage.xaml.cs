using FastShare.UI.Shared.Interfaces;
using FastShare.UI.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastShare.UI.Mobile.Forms.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceivePage : ContentPage
    {
        private ReceiveViewModel _vm = new ReceiveViewModel(App.Current as IApp);

        public ReceivePage()
        {
            InitializeComponent();
            BindingContext = _vm;
        }
    }
}