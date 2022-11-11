using FastShare.Core;
using FastShare.UI.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastShare.UI.Shared.ViewModels
{
    internal class ReceiveViewModel : ViewModelBase
    {
        private int _code;
        public int Code
        {
            get => _code;
            set
            {
                _code = value;
                NotifyPropertyChanged();
            }
        }

        public ReceiveViewModel(IApp app) : base(app)
        {
            RequestCode();
        }

        private async void RequestCode()
        {
            var code = await FastShareCore.Instance.RequestCode();
            Code = code;
        }
    }
}
