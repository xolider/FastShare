using FastShare.Core.Api;
using FastShare.Core.Model;
using FastShare.Core.Net;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FastShare.Core
{
    public class FastShareCore
    {
        private static FastShareCore _instance;

        public static FastShareCore Instance
        {
            get
            {
                if( _instance == null ) _instance = new FastShareCore();
                return _instance;
            }
        }

        public static string DEFAULT_OUTPUT_PATH { get; private set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private FastShareApi _api = new FastShareApi();

        public event Action<FastShareFileInfo> DownloadStarted;
        public event Action<int> DownloadProgress;

        public event Action SendStarted;
        public event Action<int> SendProgress;

        private bool _hasRequestedCode = false;

        public async Task<int> RequestCode()
        {
            _hasRequestedCode = true;
            return await _api.GetCode();
        }

        private async Task<string> GetHostByCode(int code)
        {
            return await _api.GetAddress(code);
        }

        public async Task<bool> ReceiveFile(string outPath)
        {
            if (!_hasRequestedCode) throw new Exception("You must call RequestCode() before receiving a file");
            try
            {
                await Task.Run(() =>
                {
                    var net = new FastShareNetReceiver();
                    net.Listen();

                    var fileInfo = net.GetFileInfo();
                    DownloadStarted?.Invoke(fileInfo);

                    net.DownloadFile(Path.Combine(outPath, fileInfo.Title), DownloadProgress);
                });
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> SendFile(string filePath, int code)
        {
            try
            {
                await Task.Run(async () =>
                {
                    var address = await GetHostByCode(code);

                    var net = new FastShareNetSender(address);

                    SendStarted?.Invoke();
                    net.SendFileInfo(filePath);

                    net.SendFile(filePath, SendProgress);
                });

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
