using FastShare.UI.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastShare.UI.Shared.ViewModels
{
    internal class SettingsViewModel : ViewModelBase
    {
        private string _savePath;
        public string SavePath
        {
            get => _savePath;
            set
            {
                _savePath = value;
                NotifyPropertyChanged();
            }
        }

        public SettingsViewModel(IApp app) : base(app)
        {
            _savePath = app.Storage.Config.DefaultSavePath;
        }
    }
}
