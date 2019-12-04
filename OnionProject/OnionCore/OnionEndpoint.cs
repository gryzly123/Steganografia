using System;

namespace OnionCore
{
    public class OnionEndpoint
    {
        public string hostName;
        public UInt16 port;
        public byte[] aesKey;

        public bool   IsKeyKnown              => (aesKey != null);
        public string AesKeyString            => Convert.ToBase64String(aesKey);
        public string ToString(bool bWithKey) => string.Format("{0}:{1}:{2}", hostName, port.ToString(), bWithKey ? AesKeyString : "");
        
        public static OnionEndpoint FromString(string str, bool bWithKey)
        {
            OnionEndpoint result = null;
            try
            {
                string[] elements = str.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                result = new OnionEndpoint() {
                    hostName = elements[0],
                    port = UInt16.Parse(elements[1]),
                    aesKey = bWithKey ? Convert.FromBase64String(elements[2]) : null
                };
            }
            catch (Exception) { result = null; }
            return result;
        }
    }
}
