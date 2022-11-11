using FastShare.UI.Shared.Components;
using FastShare.UI.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FastShare.UI.Shared.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private CommandBase _receiveCommand;
        public ICommand ReceiveCommand => _receiveCommand;

        private CommandBase _sendCommand;
        public ICommand SendCommand => _sendCommand;

        public MainViewModel(IApp app) : base(app)
        {
            _receiveCommand = new CommandBase((args) => true, (args) =>
            {
                app.CurrentWindow.NavigateToPage(NavPage.RECEIVE);
            });
            _sendCommand = new CommandBase((args) => true, (args) =>
            {
                app.CurrentWindow.NavigateToPage(NavPage.SEND);
            });
        }
    }
}
