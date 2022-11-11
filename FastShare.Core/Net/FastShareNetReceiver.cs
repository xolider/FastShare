using FastShare.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FastShare.Core.Net
{
    internal class FastShareNetReceiver : AbstractFastShareNet
    {

        private Socket _clientSocket;

        public FastShareNetReceiver()
        {
            Socket = new System.Net.Sockets.Socket(System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
            EndPoint endpoint = new IPEndPoint(IPAddress.Any, 45789);

            Socket.Bind(endpoint);
        }

        public async Task Listen()
        {
            Socket.Listen(1);
            _clientSocket = await Socket.AcceptAsync();
        }

        public async Task<FastShareFileInfo> GetFileInfo()
        {
            var info = new FastShareFileInfo();

            var lengthBytes = new byte[8];

            await _clientSocket.ReceiveAsync(lengthBytes, SocketFlags.None);

            int length = (int)BitConverter.ToInt64(lengthBytes);

            var titleBytes = new byte[2048];

            await _clientSocket.ReceiveAsync(titleBytes, SocketFlags.None);

            string title = Encoding.UTF8.GetString(titleBytes);

            info.Title = title;
            info.Length = length;

            return info;
        }

        public async Task DownloadFile(string completePath, Action<int> progress)
        {
            File.Create(completePath);

            await File.WriteAllTextAsync(completePath, "");
            var stream = File.OpenWrite(completePath);

            byte[] buffer = new byte[2048];

            int currentRead;
            int read = 0;
            while((currentRead = await _clientSocket.ReceiveAsync(buffer, SocketFlags.None)) != -1)
            {
                read += currentRead;
                progress(read);

                await stream.WriteAsync(buffer);
            }

            await stream.FlushAsync();
            stream.Close();
        }
    }
}
