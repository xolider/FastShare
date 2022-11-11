using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FastShare.Core.Net
{
    internal class FastShareNetSender : AbstractFastShareNet
    {
        public FastShareNetSender(string address, int port = 45789)
        {
            Socket = new System.Net.Sockets.Socket(System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(address), port);

            Socket.Connect(endpoint);
        }

        public void SendFileInfo(string filePath)
        {
            var file = new FileInfo(filePath);
            var name = file.Name;
            var length = file.Length;

            var lengthBytes = BitConverter.GetBytes((long)length);

            Socket.Receive(lengthBytes, SocketFlags.None);

            var titleBytes = Encoding.UTF8.GetBytes(name);

            Socket.Send(titleBytes, SocketFlags.None);
        }

        public void SendFile(string fullPath, Action<int> progress)
        {
            byte[] buffer = new byte[2048];

            var stream = File.OpenRead(fullPath);

            int currentRead;
            int read = 0;

            while((currentRead = stream.Read(buffer, read, buffer.Length)) != -1)
            {

                Socket.Send(buffer, SocketFlags.None);

                read += currentRead;
                progress(read);
            }

            stream.Flush();
            stream.Close();
        }
    }
}
