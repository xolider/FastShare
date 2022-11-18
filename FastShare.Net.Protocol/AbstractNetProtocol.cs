using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace FastShare.Net.Protocol
{
    public abstract class AbstractNetProtocol
    {
        protected Socket Socket;

        protected const int TITLE_BYTES_LENGTH = 8;
        protected const int FILE_BYTES_LENGTH = 8;
        protected const int FILE_BUFFER_SIZE = 2048;

        public KeyValuePair<string, long> ReceiveFileInfos()
        {
            var buffer = new byte[TITLE_BYTES_LENGTH];

            Socket.Receive(buffer);

            long length = BitConverter.ToInt64(buffer, 0);

            var titleLengthBytes = new byte[length];

            Socket.Receive(titleLengthBytes);

            string title = Encoding.UTF8.GetString(titleLengthBytes);

            buffer = new byte[FILE_BYTES_LENGTH];

            Socket.Receive(buffer);

            length = BitConverter.ToInt64(buffer, 0);

            return new KeyValuePair<string, long>(title, length);
        }

        public void SendFileInfos(string title, long length)
        {
            var titleBytes = Encoding.UTF8.GetBytes(title);

            long titleLength = titleBytes.Length;

            var titleLengthBytes = BitConverter.GetBytes(titleLength);

            Socket.Send(titleLengthBytes);

            Socket.Send(titleBytes);

            var lengthBytes = BitConverter.GetBytes(length);

            Socket.Send(lengthBytes);
        }

        public void ReceiveFile(long length, string outputPath, Action<int> progress)
        {
            var stream = File.OpenWrite(outputPath);

            var buffer = new byte[FILE_BUFFER_SIZE];

            int read = 0;
            int currentRead = -1;
            while (read < length)
            {
                currentRead = Socket.Receive(buffer);

                stream.Write(buffer, 0, buffer.Length);

                read += currentRead;

                progress(read);
            }

            stream.Close();
            stream.Dispose();
        }

        public void SendFile(string filePath, Action<int> progress)
        {
            var length = new FileInfo(filePath).Length;

            var stream = File.OpenRead(filePath);

            var buffer = new byte[FILE_BUFFER_SIZE];

            int read = 0;
            int currentRead = -1;
            while(read < length)
            {
                currentRead = stream.Read(buffer, 0, buffer.Length);
                Socket.Send(buffer);

                read += currentRead;
                progress(read);
            }

            stream.Close();
            stream.Dispose();
        }

        public virtual void Shutdown()
        {
            Socket.Close();
            Socket.Dispose();
        }
    }
}
