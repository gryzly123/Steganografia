using System;
using System.Collections.Generic;

namespace OnionCore
{
    public class OnionMessaging
    {
        /* Executes the specified command remotely using the provided relay route.
         * Last relay executes the actual command, previous relays execute RelayMessage commands. */
        public static void ExecutePackedCommand(List<OnionEndpoint> relayRoute, OnionCommand targetCommand, out byte[] outBytes)
        {
            //reverse the route for AES encryption iteration (packing into layers)
            relayRoute.Reverse();

            //first relay executes the actual command
            byte[] commandBytes = targetCommand.SerializeToBytes();
            AesEncoder.EncryptAes(relayRoute[0].aesKey, commandBytes, out byte[] secureCommandBytes);
            Console.WriteLine("Execute at {0}: {1}", relayRoute[0].ToString(false), Convert.ToBase64String(secureCommandBytes));

            //other relays execute RelayMessage
            for (int i = 1; i < relayRoute.Count; ++i)
            {
                RelayMessageCommand rm = new RelayMessageCommand() { targetEndpoint = relayRoute[i - 1], targetMessage = secureCommandBytes };
                AesEncoder.EncryptAes(relayRoute[i].aesKey, rm.SerializeToBytes(), out secureCommandBytes);
                Console.WriteLine("Execute at {0}: {1}", relayRoute[i].ToString(false), Convert.ToBase64String(secureCommandBytes));
            }

            //restore original route order and send the final message to first relay
            relayRoute.Reverse();
            OnionTransport.SendRawData(relayRoute[0], secureCommandBytes, out byte[] secureCommandResultBytes);

            //after receiving final answer from the first relay, unpack the command result from multiple AES layers
            for(int i = 0; i < relayRoute.Count; ++i)
            {
                AesEncoder.DecryptAes(relayRoute[i].aesKey, secureCommandResultBytes, out secureCommandResultBytes);
            }

            //return command result
            outBytes = secureCommandResultBytes;
        }

        /* Default handler method for received TCP data that should be deserialized
         * to a Command and executed. Returns empty array if deserialization fails. */
        public static void ExecuteReceivedCommand(byte[] inBytes, out byte[] outBytes)
        {
            OnionCommand c = OnionCommandFactory.SpawnCommand(inBytes);
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
