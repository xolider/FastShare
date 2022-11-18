using FastShare.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastShare.UI.Shared.Interfaces
{
    public interface IStorage
    {
        FSConfig Config { get; }
        void Save();
    }
}
