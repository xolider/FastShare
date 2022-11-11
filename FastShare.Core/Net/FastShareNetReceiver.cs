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

        public void Listen()
        {
            Socket.Listen(1);
            _clientSocket = Socket.Accept();
        }

        public FastShareFileInfo GetFileInfo()
        {
            var info = new FastShareFileInfo();

            var lengthBytes = new byte[8];

            _clientSocket.Receive(lengthBytes, SocketFlags.None);

            int length = (int)BitConverter.ToInt64(lengthBytes, 0);

            var titleBytes = new byte[2048];

            _clientSocket.Receive(titleBytes, SocketFlags.None);

            string title = Encoding.UTF8.GetString(titleBytes);

            info.Title = title;
            info.Length = length;

            return info;
        }

        public void DownloadFile(string completePath, Action<int> progress)
        {
            File.Create(completePath);

            File.WriteAllText(completePath, "");
            var stream = File.OpenWrite(completePath);

            byte[] buffer = new byte[2048];

            int currentRead;
            int read = 0;
            while((currentRead = _clientSocket.Receive(buffer, SocketFlags.None)) != -1)
            {

                stream.Write(buffer, read, buffer.Length);
                read += currentRead;
                progress(read);
            }

            stream.Flush();
            stream.Close();
        }
    }
}
