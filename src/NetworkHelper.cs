using System.Net;

namespace KOUPnPMapper
{
    public static class NetworkHelper
    {
        private static string _LocalIPAddress = string.Empty;
        /// <summary>
        /// This returns a String representation of the Current Machines IP Address
        /// </summary>
        public static string LocalIPAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_LocalIPAddress))
                {
                    IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                    if (host.AddressList.Length > 0)
                    {
                        foreach (var addr in host.AddressList)
                        {
                            // Discard those that aren't IPv4
                            if (addr.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                                continue;

                            _LocalIPAddress = addr.ToString();
                            break;
                        }
                    }
                }
                return _LocalIPAddress;
            }
        }

    }
}
