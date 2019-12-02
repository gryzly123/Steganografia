using System;
using System.Net;
using System.Text;

namespace OnionCore
{
    public abstract class Command
    {
        public static Command DetectCommand(byte[] InputData)
        {
            throw new NotImplementedException();
        }

        public abstract bool ParseInput(byte[] inData);

        public abstract void Execute(out byte[] outData);
    }

    public class RelayMessage : Command
    {
        public OnionEndpoint targetEndpoint;
        public byte[] targetMessage;

        public override bool ParseInput(byte[] inData)
        {
            throw new NotImplementedException();
        }

        public override void Execute(out byte[] outData)
        {

            throw new NotImplementedException();
        }
    }

    public class DownloadFile : Command
    {
        public string targetUrl;

        public override bool ParseInput(byte[] inData)
        {
            targetUrl = Encoding.UTF8.GetString(inData);
            Uri.TryCreate(targetUrl, UriKind.Absolute, out Uri uriResult);
            bool bIsValid = uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            return bIsValid;
        }

        public override void Execute(out byte[] outData)
        {
            using (var client = new WebClient())
            {
                outData = client.DownloadData(targetUrl);
            }
        }
    }
}