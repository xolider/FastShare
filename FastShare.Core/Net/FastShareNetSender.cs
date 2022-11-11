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

        public async Task SendFileInfo(string filePath)
        {
            var file = new FileInfo(filePath);
            var name = file.Name;
            var length = file.Length;

            var lengthBytes = BitConverter.GetBytes((long)length);

            await Socket.SendAsync(lengthBytes, SocketFlags.None);

            var titleBytes = Encoding.UTF8.GetBytes(name);

            await Socket.SendAsync(titleBytes, SocketFlags.None);
        }

        public async Task SendFile(string fullPath, Action<int> progress)
        {
            byte[] buffer = new byte[2048];

            var stream = File.OpenRead(fullPath);

            int currentRead;
            int read = 0;

            while((currentRead = await stream.ReadAsync(buffer)) != -1)
            {
                read += currentRead;
                progress(read);

                await Socket.SendAsync(buffer, SocketFlags.None);
            }

            await stream.FlushAsync();
            stream.Close();
        }
    }
}
