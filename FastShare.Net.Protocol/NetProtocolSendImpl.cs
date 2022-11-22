using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FastShare.Net.Protocol
{
    internal class NetProtocolSendImpl : AbstractNetProtocol
    {
        public NetProtocolSendImpl(string address, int port)
        {
            Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            var endpoint = new IPEndPoint(IPAddress.Parse(address), port);

            Socket.Connect(endpoint);
        }
    }
}
