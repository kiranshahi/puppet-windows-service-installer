using System.ServiceProcess;

namespace WindowsServiceInstaller
{
	public partial class NullService : ServiceBase
	{
		public NullService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
		}

		protected override void OnStop()
		{
		}
	}
}
