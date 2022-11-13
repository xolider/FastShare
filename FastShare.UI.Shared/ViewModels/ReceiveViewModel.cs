using FastShare.Core;
using FastShare.Core.Model;
using FastShare.UI.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FastShare.UI.Shared.ViewModels
{
    internal class ReceiveViewModel : ViewModelBase
    {
        private int _code = -1;
        public int Code
        {
            get => _code;
            set
            {
                _code = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isWaitingForTransfert = true;
        public bool IsWaitingForTransfert
        {
            get => _isWaitingForTransfert;
            set
            {
                _isWaitingForTransfert = value;
                NotifyPropertyChanged();
            }
        }

        private string _status = "Waiting for file...";
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                NotifyPropertyChanged();
            }
        }

        private int _progressPercent = 0;
        public int ProgressPercent
        {
            get => _progressPercent;
            set
            {
                _progressPercent = value;
                NotifyPropertyChanged();
            }
        }

        private FastShareFileInfo _info = null;

        public ReceiveViewModel(IApp app) : base(app)
        {
            FastShareCore.Instance.DownloadStarted += Instance_DownloadStarted;
            FastShareCore.Instance.DownloadProgress += Instance_DownloadProgress;

            RequestCode();
        }

        /*
         * Event delegates have to be run on UI thread since they are called from another thread pool from another assembly.
         * Take a look at App.xaml.cs to see the implementation
         */

        private void Instance_DownloadProgress(int obj)
        {
            app.RunOnUIThread(() =>
            {
                ProgressPercent = obj * 100 / (int)_info.Length;
            });
        }

        private void Instance_DownloadStarted(Core.Model.FastShareFileInfo obj)
        {
            app.RunOnUIThread(() =>
            {
                IsWaitingForTransfert = false;
                Status = "Receiving file: \"" + obj.Title + "\"...";
                _info = obj;
            });
        }

        public async void RequestCode()
        {
            var code = await FastShareCore.Instance.RequestCode();
            Code = code;
        }

        public async void ReceiveFile(string outputPath = null)
        {
            var success = await FastShareCore.Instance.ReceiveFile(outputPath);
        }
    }
}
