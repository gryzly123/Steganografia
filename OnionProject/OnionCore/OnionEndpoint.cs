using System;
using System.Net;

namespace OnionCore
{
    public class OnionEndpoint
    {
        public IPAddress targetIp;
        public UInt16 port;
        public byte[] aesKey;
    }
}
