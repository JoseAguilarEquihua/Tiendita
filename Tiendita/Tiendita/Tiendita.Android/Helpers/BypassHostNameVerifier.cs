
using Javax.Net.Ssl;

namespace Tiendita.Droid.Helpers
{
    public class BypassHostnameVerifier : Java.Lang.Object, IHostnameVerifier
    {

        public bool Verify(string hostname, ISSLSession session)
        {
            return true;
        }
    }
}