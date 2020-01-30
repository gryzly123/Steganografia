using System;
using System.Net;
using System.Net.Sockets;

namespace OnionCore
{
    public delegate void HandleTcpData(byte[] inBytes, out byte[] outBytes);

    public static class OnionTransport
    {
        const int BufferSize = 256;

        /* Launches TCP client that sends specified data to the target relay.
         * It does not perform any AES operations, because inner messages for consequent relays
         * should already be ciphered. */
        public static void SendRawData(OnionEndpoint targetRelay, byte[] inData, out byte[] outData)
        {
            try
            {
                //create client
                using (TcpClient client = new TcpClient(targetRelay.hostName.ToString(), targetRelay.port))
                {
                    NetworkStream stream = client.GetStream();

                    //send data
                    stream.Write(BitConverter.GetBytes((UInt32)inData.Length), 0, 4);
                    stream.Write(inData, 0, inData.Length);

                    //receive response
                    byte[] targetLen = new byte[4];
                    stream.Read(targetLen, 0, 4);
                    UInt32 bytes = BitConverter.ToUInt32(targetLen, 0);
                    outData = new byte[bytes];

                    byte[] buffer = new byte[BufferSize];
                    int idx = 0;
                    while (idx < bytes)
                    {
                        int len = stream.Read(buffer, 0, BufferSize);
                        Array.Copy(buffer, 0, outData, idx, len);
                        idx += len;
                    }

                    //close client
                    stream.Close();
                    client.Close();
                }
            }
            catch(Exception)
            {
                outData = new byte[0];
            }
        }

        /* Launches TCP server that receives data from clients and other relays.
         * Received data is deciphered using relay's AES key and passed to the onDataReceived delegate.
         * Delegate's output is reciphered using the same AES key and sent as response.
         * @note: if you do not wish to perform additional operations on the received data, you can
         *        use a default method for parsing and executing commands (OnionMessage.ExecuteReceivedCommand). */
        public static void RunSecureServer(OnionEndpoint thisRelay, HandleTcpData onDataReceived)
        {
            TcpListener server = null;
            try
            {
                //create server
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, thisRelay.port);
                server.Start();

                //handle clients
                while (true)
                {
                    //receive client
                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();

                    //read data from client
                    byte[] targetLen = new byte[4];
                    stream.Read(targetLen, 0, 4);
                    UInt32 bytes = BitConverter.ToUInt32(targetLen, 0);
                    byte[] aesInData = new byte[bytes];

                    byte[] buffer = new byte[BufferSize];
                    int idx = 0;
                    while (idx < bytes)
                    {
                        int len = stream.Read(buffer, 0, BufferSize);
                        Array.Copy(buffer, 0, aesInData, idx, len);
                        idx += len;
                    }

                    AesEncoder.DecryptAes(thisRelay.aesKey, aesInData, out byte[] inData);
                    Console.WriteLine("Received {0}", Convert.ToBase64String(aesInData));

                    //handle data and generate response
                    onDataReceived(inData, out byte[] outData);
                    AesEncoder.EncryptAes(thisRelay.aesKey, outData, out byte[] aesOutData);

                    //send response
                    stream.Write(BitConverter.GetBytes((UInt32)aesOutData.Length), 0, 4);
                    stream.Write(aesOutData, 0, aesOutData.Length);

                    //close client
                    client.Close();
                }
            }
            finally
            {
                //close server
                if (server != null) server.Stop();
            }
        }
    }
}
