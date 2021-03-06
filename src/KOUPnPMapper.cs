﻿using KOUPnPMapper.Properties;

namespace KOUPnPMapper
{
    public static class KOUPnPMapper
    {
        public static readonly int[] DefaultPorts =
        {
            // Login ports
            15100, 15101, 15102, 15103, 15104, 15105, 15106, 15107, 15108, 15109, 15110, 15111, 15112, 15113, 15114,

            // Game port
            15001
        };
        public static int[] Ports;
        public static readonly string Protocol = "TCP";

        public static string LocalIP = null;

        public static void LoadSettings()
        {
            LocalIP = Settings.Default.LocalIP;
            if (string.IsNullOrEmpty(LocalIP))
                LocalIP = NetworkHelper.LocalIPAddress;
            Ports = Settings.Default.Ports ?? DefaultPorts;
        }

        #region Forward ports
        public static bool ForwardPorts()
        {
            return _ForwardPorts(Ports);
        }

        private static bool _ForwardPorts(params int[] ports)
        {
            foreach (var port in ports)
            {
                if (!ForwardPort(port))
                    return false;
            }

            return true;
        }

        private static bool ForwardPort(int port)
        {
            try
            {
                var mapping = UPnPNATHelper.Add(port, Protocol, port, LocalIP, true, port.ToString());
                return (mapping != null);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Unforward ports
        public static void UnforwardPorts()
        {
            _UnforwardPorts(Ports);
        }

        private static void _UnforwardPorts(params int[] ports)
        {
            foreach (var port in ports)
                UnforwardPort(port);
        }

        private static void UnforwardPort(int port)
        {
            try
            {
                UPnPNATHelper.Remove(port, Protocol);
            }
            catch
            {
            }
        }
        #endregion
    }
}
