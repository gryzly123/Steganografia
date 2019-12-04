using OnionCore;
using System;
using System.Text;

namespace OnionRelay
{
    class Program
    {
        static void Main(string[] args)
        {
            UInt16 targetPort;
            try               { targetPort = UInt16.Parse(args[0]); }
            catch (Exception) { targetPort = 5555; }

            byte[] targetKey;
            try              { targetKey = Convert.FromBase64String(args[1]); }
            catch(Exception) { targetKey = Encoding.ASCII.GetBytes("default-bytes-from-ascii"); }

            OnionEndpoint thisRelay = new OnionEndpoint() {
                hostName = "127.0.0.1",
                port = targetPort,
                aesKey = targetKey
            };

            Console.WriteLine("Running OnionRelay at {0}:{1} with key {2}", thisRelay.hostName, thisRelay.port.ToString(), Convert.ToBase64String(thisRelay.aesKey));
            OnionTransport.RunSecureServer(thisRelay, OnionMessaging.ExecuteReceivedCommand);
        }
    }
}
