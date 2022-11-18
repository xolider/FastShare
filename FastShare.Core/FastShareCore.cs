using FastShare.Core.Api;
using FastShare.Core.Model;
using FastShare.Net.Protocol;
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

        public static string DEFAULT_OUTPUT_PATH { get; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

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

        public async Task<bool> ReceiveFile(string outPath = null)
        {
            if (!_hasRequestedCode) throw new Exception("You must call RequestCode() before receiving a file");
            try
            {
                await Task.Run(() =>
                {
                    var net = NetProtocolFactory.CreateReceiver();

                    var fileInfoNet = net.ReceiveFileInfos();
                    var fileInfo = new FastShareFileInfo
                    {
                        Title = fileInfoNet.Key,
                        Length = fileInfoNet.Value
                    };

                    DownloadStarted?.Invoke(fileInfo);

                    net.ReceiveFile(fileInfo.Length, Path.Combine(outPath == null ? DEFAULT_OUTPUT_PATH : outPath, fileInfo.Title), DownloadProgress);

                    net.Shutdown();
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

                    var net = NetProtocolFactory.CreateSender(address, 45789);

                    SendStarted?.Invoke();
                    var fi = new FileInfo(filePath);

                    net.SendFileInfos(fi.Name, fi.Length);

                    net.SendFile(filePath, SendProgress);

                    net.Shutdown();
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
