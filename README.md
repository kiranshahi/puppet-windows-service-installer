puppet-windows-service-installer
================================

Installs a windows service using a skeleton (or fake double) service executable  that is to be substituted later during deployment with the real service executable.

In puppet, you can specify the filename of executable,  the target directory to deploy,  the windows service name, the windows service display name and description of the service.

Usage
======
Sample node configuration to install:

node 'mynode1' {
	class { 'windows_service_installer':
		binary_name => "DemoService.exe", 
		target_path => "C:\\windows_services\\demoservice",
		service_name => "MyDemoService",
		display_name => "My Demo Service",
		description => "My Demo Service description",  
	}
}

Pre-requisite:
===============
Assumes Microsoft.NET 4.0 is already installed.  This script uses .NET 4.0 windows service installer utility (installutil.exe).  To puppetize the Microsoft .NET 4.0 installation, please see this repository: https://github.com/opentable/puppet-application-dotnet40


WindowsServiceInstaller Project
===============================
The WindowsServiceInstaller directory contains the Visual Studio 2012 project that implements the ability to read the service name, service display name and service description which are passed as command shell arguments to installutil.exe.



