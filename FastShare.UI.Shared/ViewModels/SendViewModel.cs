using FastShare.UI.Shared.Components;
using FastShare.UI.Shared.Interfaces;
using System.Windows.Input;

namespace FastShare.UI.Shared.ViewModels
{
    public class SendViewModel : ViewModelBase
    {
        private CommandBase _sendCommand;
        public ICommand SendCommand => _sendCommand;

        private string _code;
        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                _sendCommand.RaiseCanExecute();
                NotifyPropertyChanged();
            }
        }

        private string _selectedFile;
        public string SelectedFile
        {
            get => _selectedFile;
            set
            {
                _selectedFile = value;
                _sendCommand.RaiseCanExecute();
                NotifyPropertyChanged();
            }
        }

        public SendViewModel(IApp app) : base(app)
        {
            _sendCommand = new CommandBase((args) => Code != "" && Code != null && SelectedFile != "" && SelectedFile != null && Code.Length == 4, (args) =>
            {

            });
        }

    }
}
