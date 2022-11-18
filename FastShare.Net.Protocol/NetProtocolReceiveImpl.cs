using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FastShare.Net.Protocol
{
    internal class NetProtocolReceiveImpl : AbstractNetProtocol
    {
        private Socket _server = new Socket(SocketType.Stream, ProtocolType.Tcp);

        public NetProtocolReceiveImpl()
        {
            var endpoint = new IPEndPoint(IPAddress.Any, 45789);
            _server.Bind(endpoint);

            _server.Listen(1);

            Socket = _server.Accept();
        }

        public override void Shutdown()
        {
            base.Shutdown();
            _server.Close();
            _server.Dispose();
        }
    }
}
