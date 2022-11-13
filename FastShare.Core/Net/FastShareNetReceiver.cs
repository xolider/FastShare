using FastShare.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void Listen()
        {
            Socket.Listen(1);
            _clientSocket = Socket.Accept();
        }

        public FastShareFileInfo GetFileInfo()
        {
            var info = new FastShareFileInfo();

            var lengthBytes = new byte[8];

            _clientSocket.Receive(lengthBytes);

            long length = BitConverter.ToInt64(lengthBytes, 0);

            var titleLength = new byte[4];

            _clientSocket.Receive(titleLength);

            var titleBytes = new byte[BitConverter.ToInt32(titleLength, 0)];

            _clientSocket.Receive(titleBytes);

            string title = Encoding.UTF8.GetString(titleBytes);

            info.Title = title;
            info.Length = length;

            return info;
        }

        public void DownloadFile(string completePath, long length, Action<int> progress)
        {
            Debug.WriteLine("Writing to " + completePath);
            var stream = File.OpenWrite(completePath);

            byte[] buffer = new byte[2048];

            int currentRead = -1;
            int read = 0;
            while(read < length)
            {
                currentRead = _clientSocket.Receive(buffer);
                stream.Write(buffer, 0, buffer.Length);
                read += currentRead;
                progress(read);
            }

            stream.Flush();
            stream.Close();
        }

        internal void CloseClient()
        {
            _clientSocket.Close();
            _clientSocket.Dispose();
        }
    }
}
