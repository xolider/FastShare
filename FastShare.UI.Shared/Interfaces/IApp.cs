using System;
using System.Collections.Generic;
using System.Text;

namespace FastShare.UI.Shared.Interfaces
{
    public interface IApp
    {
        IWindow CurrentWindow { get; }
        void RunOnUIThread(Action action);
    }
}
