using System;
using System.Collections.Generic;

namespace OnionCore
{
    public class OnionMessage
    {
        public static void ExecutePackedCommand(List<OnionEndpoint> relayRoute, Command targetCommand, out byte[] outBytes)
        {
            relayRoute.Reverse();

            byte[] commandBytes = targetCommand.GenerateInput();
            AesEncoder.EncryptAes(relayRoute[0].aesKey, commandBytes, out byte[] secureCommandBytes);

            for(int i = 1; i < relayRoute.Count; ++i)
            {
                RelayMessage rm = new RelayMessage() { targetEndpoint = relayRoute[i - 1], targetMessage = secureCommandBytes };
                AesEncoder.EncryptAes(relayRoute[i].aesKey, rm.GenerateInput(), out secureCommandBytes);
            }

            relayRoute.Reverse();
            TcpSupport.SendRawData(relayRoute[0], secureCommandBytes, out byte[] secureCommandResultBytes);

            for(int i = 0; i < relayRoute.Count; ++i)
            {
                AesEncoder.DecryptAes(relayRoute[i].aesKey, secureCommandResultBytes, out secureCommandResultBytes);
            }

            outBytes = secureCommandResultBytes;
        }

        public static void ExecuteReceivedCommand(byte[] inBytes, out byte[] outBytes)
        {
            Command c = Command.DetectCommand(inBytes);
            if (c == null)
            {
                outBytes = new byte[0];
            }
            else
            {
                c.Execute(out outBytes);
            }
        }
    }
}
