using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceProcess;

namespace WindowsServiceInstaller
{
	[RunInstaller(true)]
	public partial class ProjectInstaller : System.Configuration.Install.Installer
	{
		public ProjectInstaller()
		{
			var serviceProcessInstaller = new ServiceProcessInstaller();
			var serviceInstaller = new ServiceInstaller();

			SetServiceParameters(serviceInstaller);

			serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
			serviceInstaller.StartType = ServiceStartMode.Automatic;
			Installers.Add(serviceProcessInstaller);
			Installers.Add(serviceInstaller);
		}

		private void SetServiceParameters(ServiceInstaller serviceInstaller)
		{
			serviceInstaller.ServiceName = GetParam("/servicename", true);
			serviceInstaller.DisplayName = GetParam("/displayname", true);
			serviceInstaller.Description = GetParam("/description", false);
		}

		private string GetParam(string argument, bool isRequired)
		{
			string[] args = Environment.GetCommandLineArgs();

			string arg = (from s in args where s.StartsWith(argument) select s).FirstOrDefault();

			if (isRequired && string.IsNullOrEmpty(arg))
				throw new ArgumentException(string.Format("Argument '{0}' is missing", argument));

			if (string.IsNullOrEmpty(arg))
				return null;

			if (arg.Split('=').Length != 2)
				throw new ArgumentException(string.Format("Invalid Argument '{0}'", argument));

			return arg.Split('=')[1];
		}
	}
}
