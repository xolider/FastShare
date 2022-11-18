using System;
using System.Collections.Generic;
using System.Text;

namespace FastShare.UI.Shared.Interfaces
{
    public interface IApp
    {
        IWindow CurrentWindow { get; } //Gets the current navigable window of the implemented project
        IStorage Storage { get; }
        void RunOnUIThread(Action action); //Runs code on the UI thread of the implemented project
    }
}
