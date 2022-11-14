using FastShare.Core;
using FastShare.Core.Model;
using FastShare.UI.Shared.Components;
using FastShare.UI.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

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

        private bool _showInExplorerVisible = false;
        public bool ShowInExplorerVisible
        {
            get => _showInExplorerVisible;
            set
            {
                _showInExplorerVisible = value;
                NotifyPropertyChanged();
            }
        }

        private CommandBase _showFileInExplorerCommand;
        public ICommand ShowFileInExplorerCommand => _showFileInExplorerCommand;

        private FastShareFileInfo _info = null;

        private string _folderPath = FastShareCore.DEFAULT_OUTPUT_PATH;

        public ReceiveViewModel(IApp app) : base(app)
        {
            _showFileInExplorerCommand = new CommandBase((args) => true, (args) =>
            {
                Process.Start("explorer.exe", _folderPath);
            });
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
                if(_progressPercent == 100)
                {
                    ShowInExplorerVisible = true;
                }
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
            if (outputPath != null) _folderPath = outputPath;
            var success = await FastShareCore.Instance.ReceiveFile(outputPath);
        }
    }
}
