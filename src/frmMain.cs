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
            var ports = string.Join(", ", Array.ConvertAll(KOUPnPMapper.Ports, x => x.ToString()));

            if (KOUPnPMapper.ForwardPorts())
                lblStatus.Text = string.Format("Ports {0} are forwarded to {1}.", 
                    ports, KOUPnPMapper.LocalIP);
            else
                lblStatus.Text = string.Format("Ports {0} could not be forwarded to {1}.\n\n" +
                                               "Please check that UPnP is enabled in your router.", 
                                               ports, KOUPnPMapper.LocalIP);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            KOUPnPMapper.UnforwardPorts();
        }
    }
}
