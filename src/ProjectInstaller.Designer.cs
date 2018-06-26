namespace KOUPnPMapper
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.KOUPnPMapperProcessServiceInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.KOUPnPMapperServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // KOUPnPMapperProcessServiceInstaller
            // 
            this.KOUPnPMapperProcessServiceInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.KOUPnPMapperProcessServiceInstaller.Password = null;
            this.KOUPnPMapperProcessServiceInstaller.Username = null;
            // 
            // KOUPnPMapperServiceInstaller
            // 
            this.KOUPnPMapperServiceInstaller.Description = "Forwards the configured ports to either the configured machine\'s IP or the first " +
    "IPv4 address that is found on the system.";
            this.KOUPnPMapperServiceInstaller.DisplayName = "Knight Online UPnP port mapper (forwarder)";
            this.KOUPnPMapperServiceInstaller.ServiceName = "KOUPnPMapperService";
            this.KOUPnPMapperServiceInstaller.ServicesDependedOn = new string[] {
        "Netman"};
            this.KOUPnPMapperServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.KOUPnPMapperProcessServiceInstaller,
            this.KOUPnPMapperServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller KOUPnPMapperProcessServiceInstaller;
        private System.ServiceProcess.ServiceInstaller KOUPnPMapperServiceInstaller;
    }
}