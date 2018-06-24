using System.Net;
using System.Net.NetworkInformation;

namespace KOUPnPMapper
{
    public static class NetworkHelper
    {
        private static string _LocalIPAddress = string.Empty;

        /// <summary>
        /// This returns a String representation of the current machine's IP Address
        /// </summary>
        public static string LocalIPAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_LocalIPAddress))
                {
                    var localIP = GetDefaultLocalIP();
                    if (localIP != null)
                        _LocalIPAddress = localIP.ToString();
                }

                return _LocalIPAddress;
            }
        }

        public static IPAddress GetDefaultLocalIP()
        {
            var cards = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var card in cards)
            {
                if (card.OperationalStatus != OperationalStatus.Up)
                    continue;

                var props = card.GetIPProperties();
                if (props == null)
                    continue;

                foreach (var ip in props.UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        return ip.Address;
                }
            }

            return null;
        }
    }
}
