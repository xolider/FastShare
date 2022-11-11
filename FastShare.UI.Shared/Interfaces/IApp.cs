using System;
using System.Collections.Generic;
using System.Text;

namespace FastShare.UI.Shared.Interfaces
{
    internal interface IApp
    {
        IWindow CurrentWindow { get; }
    }
}
