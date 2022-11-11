using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace FastShare.Core.Net
{
    internal abstract class AbstractFastShareNet
    {
        public Socket Socket;

        public void Shutdown()
        {
            Socket.Close();
        }
    }
}
