using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace KOUPnPMapper
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            KOUPnPMapper.LoadSettings();

            // Generate friendly port list.
            var ports = getFriendlyPortList(KOUPnPMapper.Ports);

            if (KOUPnPMapper.ForwardPorts())
                lblStatus.Text = string.Format("Ports {0} are forwarded to {1}.", 
                    ports, KOUPnPMapper.LocalIP);
            else
                lblStatus.Text = string.Format("Ports {0} could not be forwarded to {1}.\n\n" +
                                               "Please check that UPnP is enabled in your router.", 
                                               ports, KOUPnPMapper.LocalIP);
        }

        private string getFriendlyPortList(int[] ports)
        {
            var sortedPorts = new List<int>(ports);
            sortedPorts.Sort();

            var result = "";
            int startPort = -1;
            int endPort = -1;
            foreach (var port in sortedPorts)
            {
                // For the first port in the list, we should indicate it's (potentially) the start of a range.
                if (endPort == -1)
                {
                    startPort = port;
                }
                // If the end port doesn't match the last checked port, finalise this entry.
                else if (port - 1 != endPort)
                {
                    appendPortRangeToString(ref result, startPort, endPort);
                    startPort = port;
                }

                endPort = port;
            }

            if (startPort >= 0)
                appendPortRangeToString(ref result, startPort, endPort);

            return result;
        }

        private string getPortRangeAsString(int startPort, int endPort)
        {
            var portRange = "";

            if (startPort == endPort)
                portRange = startPort.ToString();
            else
                portRange = string.Format("{0}-{1}", startPort, endPort);

            return portRange;
        }

        private void appendPortRangeToString(ref string result, int startPort, int endPort)
        {
            var portRange = getPortRangeAsString(startPort, endPort);
            if (result.Length > 0)
                result += ", ";

            result += portRange;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            KOUPnPMapper.UnforwardPorts();
        }
    }
}
