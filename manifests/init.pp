define windows_service_installer($binary_name, $target_path, $service_name, $display_name, $description) {
  include 'param::powershell'
  
  $installUtil_filepath = "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\InstallUtil.exe"
    
  exec { "create_target_directory":
		command => "cmd /c mkdir ${target_path}",
		path    => "${param::powershell::path};${::path}",
		unless => "${param::powershell::command} -Command \"if (Test-Path '${target_path}') { exit 0 } else { exit 1 }\"",
  }

  file { 'skeleton-service-binary':
    ensure  => file,
    mode    => '0777',
    path    => "${target_path}\\${binary_name}",
    source  => "puppet:///modules/windows_service_installer/WindowsServiceInstaller.exe",
	replace => false,
	require => Exec['create_target_directory'],
  }

  exec { 'install-skeleton-service':
    command   => "\"${installUtil_filepath}\" /i  ${target_path}\\${binary_name} /servicename=\"${service_name}\" /displayname=\"${display_name}\" /description=\"${description}\" ",
	path      => "${param::powershell::path};C:\\Windows\\sysnative;${::path}",
	onlyif    => "${param::powershell::command} -Command \"if ((Get-Service \"${$service_name}\" -ErrorAction SilentlyContinue).DisplayName -eq \$NULL) { exit 0 } else { exit 1 }\"",
    logoutput => true,
    require   => File['skeleton-service-binary'],
  }   
}


