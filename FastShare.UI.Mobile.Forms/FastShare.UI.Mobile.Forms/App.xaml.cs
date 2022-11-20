using FastShare.UI.Mobile.Forms.Pages.Nav;
using FastShare.UI.Mobile.Forms.Storage;
using FastShare.UI.Shared.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastShare.UI.Mobile.Forms
{
    public partial class App : Application, IApp
    {
        IWindow IApp.CurrentWindow => MainPage as IWindow;

        public IStorage Storage => FSStorage.Instance;

        public App()
        {
            InitializeComponent();

            MainPage = new NavPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public void RunOnUIThread(Action action)
        {
            action();
        }
    }
}
