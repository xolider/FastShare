using FastShare.UI.Mobile.Forms.Interfaces;
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
    public partial class SendPage : ContentPage
    {
        private SendViewModel _vm = new SendViewModel(App.Current as IApp);

        public SendPage()
        {
            InitializeComponent();
            BindingContext = _vm;
        }

        private async void PickFileClicked(object sender, EventArgs e)
        {
            IDeviceFilePicker picker = DependencyService.Get<IDeviceFilePicker>();
            var result = await picker.PickFile();

            _vm.SelectedFile = result.FullPath;
        }
    }
}