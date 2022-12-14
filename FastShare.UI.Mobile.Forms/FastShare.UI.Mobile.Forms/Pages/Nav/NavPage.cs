using FastShare.UI.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FastShare.UI.Mobile.Forms.Pages.Nav
{
    public class NavPage : NavigationPage, IWindow
    {

        public NavPage(Page initialPage) : base(initialPage) { }

        public async void NavigateToPage(Shared.Components.NavPage page)
        {
            Page targetPage = null;
            switch(page)
            {
                case Shared.Components.NavPage.MAIN:
                    targetPage = new MainPage();
                    break;
                case Shared.Components.NavPage.RECEIVE:
                    targetPage = new ReceivePage();
                    break;
                case Shared.Components.NavPage.SEND:
                    targetPage = new SendPage();
                    break;
            }

            await PushAsync(targetPage);
        }
    }
}
