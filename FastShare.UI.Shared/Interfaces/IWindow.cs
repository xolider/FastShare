using System;
using System.Collections.Generic;
using System.Text;

namespace FastShare.UI.Shared.Interfaces
{
    internal interface IWindow
    {
        enum NavPage
        {
            MAIN,
            RECEIVE,
            SEND
        }

        void NavigateToPage(NavPage page);
    }
}
