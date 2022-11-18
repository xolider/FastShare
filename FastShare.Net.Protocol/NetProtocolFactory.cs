using System;
using System.Collections.Generic;
using System.Text;

namespace FastShare.Net.Protocol
{
    public class NetProtocolFactory
    {
        public static AbstractNetProtocol CreateReceiver()
        {
            return new NetProtocolReceiveImpl();
        }

        public static AbstractNetProtocol CreateSender(string ip, int port)
        {
            return new NetProtocolSendImpl(ip, port);
        }
    }
}
