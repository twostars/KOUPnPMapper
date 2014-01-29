using System.ServiceProcess;

namespace KOUPnPMapper
{
    public partial class KOUPnPMapperService : ServiceBase
    {
        public KOUPnPMapperService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            KOUPnPMapper.LoadSettings();
            KOUPnPMapper.ForwardPorts();
        }

        protected override void OnStop()
        {
            KOUPnPMapper.UnforwardPorts();
        }
    }
}
