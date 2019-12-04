using System;
using System.IO;
using System.Text;

namespace OnionCore
{
    /* Command that asks the endpoint to relay the message to another endpoint.
     * Example message:
     *                  $REL\n<hostname>:<port>:<optional-aes-key>\r\n<AES encoded message>
     */
    public class RelayMessageCommand : OnionCommand
    {
        //------------parameters

        public OnionEndpoint targetEndpoint;
        public byte[] targetMessage;

        //------------OnionCommand interface

        public override string CommandCode() { return "$REL\n"; }

        public override byte[] SerializeToBytes()
        {
            string header = CommandCode() + targetEndpoint.ToString(false) + "\r\n";
            byte[] result = new byte[header.Length + targetMessage.Length];
            Array.Copy(Encoding.ASCII.GetBytes(header), result, header.Length);
            Array.Copy(targetMessage, 0, result, header.Length, targetMessage.Length);
            return result;
        }

        public override bool DeserializeFromBytes(byte[] inData)
        {
            using (MemoryStream ms = new MemoryStream(inData))
            {
                //deserialize targetEndpoint
                string endpointString = "";
                using (StreamReader sr = new StreamReader(ms))
                {
                    endpointString = sr.ReadLine();
                }
                targetEndpoint = OnionEndpoint.FromString(endpointString, false);
                if (targetEndpoint == null) return false;

                //read message for the endpoint
                int memoryOffset = endpointString.Length + 2; // 2 = \r\n
                int messageLength = inData.Length - memoryOffset;
                if (messageLength == 0) return false;

                targetMessage = new byte[inData.Length - memoryOffset];
                Array.Copy(inData, memoryOffset, targetMessage, 0, messageLength);

                //everything was serialized
                return true;
            }
        }

        public override void Execute(out byte[] outData)
        {
            Console.WriteLine("Relaying to {0}: {1}", targetEndpoint.ToString(false), Convert.ToBase64String(targetMessage));
            OnionTransport.SendRawData(targetEndpoint, targetMessage, out outData);
        }
    }   
}
