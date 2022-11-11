using FastShare.UI.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FastShare.UI.Shared.ViewModels
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected IApp app;

        protected ViewModelBase(IApp app)
        {
            this.app = app;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string memberName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }
    }
}
