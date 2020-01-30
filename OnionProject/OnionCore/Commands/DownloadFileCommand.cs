using System;
using System.Net;
using System.Text;

namespace OnionCore
{
    /* Command that downloads a file from the Internet and returns its contents.
     * Example message:
     *                  $DLF\nhttps://example.com/image.jpg
     */
    public class DownloadFileCommand : OnionCommand
    {
        //------------parameters

        public string targetUrl;

        //------------OnionCommand interface

        public override string CommandCode() { return "$DLF\n"; }

        public override bool DeserializeFromBytes(byte[] inData)
        {
            targetUrl = Encoding.UTF8.GetString(inData);
            Uri.TryCreate(targetUrl, UriKind.Absolute, out Uri uriResult);
            bool bIsValid = uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            return bIsValid;
        }

        public override byte[] SerializeToBytes()
        {
            string header = CommandCode() + targetUrl;
            return Encoding.ASCII.GetBytes(header);
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
