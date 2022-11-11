using FastShare.UI.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FastShare.UI.Mobile.Forms.Pages.Nav
{
    internal class NavPage : NavigationPage, IWindow
    {

        public NavPage(Page initialPage) : base(initialPage) { }

        public async void NavigateToPage(Shared.Components.NavPage page)
        {
            Page targetPage = null;
            switch(page)
            {
                case Shared.Components.NavPage.MAIN:
                    targetPage= new MainPage();
                    break;
                case Shared.Components.NavPage.RECEIVE:

                    break;
                case Shared.Components.NavPage.SEND:

                    break;
            }

            await PushAsync(targetPage);
        }
    }
}
