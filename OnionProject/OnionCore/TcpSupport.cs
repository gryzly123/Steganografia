using System;
using System.Net;
using System.Net.Sockets;

namespace OnionCore
{
    public delegate void HandleTcpData(byte[] inBytes, out byte[] outBytes);

    public class TcpSupport
    {
        public static void SendRawData(OnionEndpoint targetRelay, byte[] inData, out byte[] outData)
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
                stream.Read(outData, 0, (Int32)bytes);

                //close client
                stream.Close();
                client.Close();
            }
        }

        public static void SendSecureData(OnionEndpoint targetRelay, byte[] inData, out byte[] outData)
        {
            //create client
            using (TcpClient client = new TcpClient(targetRelay.hostName, targetRelay.port))
            {
                NetworkStream stream = client.GetStream();

                //send data
                AesEncoder.EncryptAes(targetRelay.aesKey, inData, out byte[] aesInData);
                stream.Write(BitConverter.GetBytes((UInt32)aesInData.Length), 0, 4);
                stream.Write(aesInData, 0, aesInData.Length);

                //receive response
                byte[] targetLen = new byte[4];
                stream.Read(targetLen, 0, 4);
                UInt32 bytes = BitConverter.ToUInt32(targetLen, 0);
                byte[] aesOutData = new byte[bytes];
                stream.Read(aesOutData, 0, (Int32)bytes);
                AesEncoder.DecryptAes(targetRelay.aesKey, aesOutData, out outData);

                //close client
                stream.Close();
                client.Close();
            }
        }
        
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
                    stream.Read(aesInData, 0, (Int32)bytes);
                    AesEncoder.DecryptAes(thisRelay.aesKey, aesInData, out byte[] inData);

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
        
        /*public static void RunServer(OnionEndpoint thisRelay, HandleTcpData onDataReceived)
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
                    byte[] inData = new byte[bytes];
                    stream.Read(inData, 0, (Int32)bytes);

                    //handle data and send response
                    onDataReceived(inData, out byte[] outData);
                    stream.Write(BitConverter.GetBytes((UInt32)inData.Length), 0, 4);
                    stream.Write(outData, 0, outData.Length);

                    //close client
                    client.Close();
                }
            }
            finally
            {
                //close server
                if(server != null) server.Stop();
            }
        }*/

    }
}
