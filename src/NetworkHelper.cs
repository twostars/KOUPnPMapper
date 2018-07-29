using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;

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

            var addressesByIndex = new SortedList<int, IPAddress>();
            foreach (var card in cards)
            {
                if (card.OperationalStatus != OperationalStatus.Up)
                    continue;

                var props = card.GetIPProperties();
                if (props == null)
                    continue;

                var ipv4Props = props.GetIPv4Properties();
                if (ipv4Props == null)
                    continue;

                var index = ipv4Props.Index;
                foreach (var ip in props.UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        addressesByIndex.Add(index, ip.Address);
                }
            }

            // Return the first. This is sorted by index.
            foreach (var kvp in addressesByIndex)
                return kvp.Value;

            return null;
        }
    }
}
