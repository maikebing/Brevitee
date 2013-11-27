echo off
SET VERSIONKIND=%1
if %VERSIONKIND%=="" GOTO END
echo %VERSIONKIND%

nuver %VERSIONKIND% /path:KLGates.Core.Full\KLGates.Core.Full.nuspec
nuver %VERSIONKIND% /path:KLGates.Core\KLGates.Core.nuspec
nuver %VERSIONKIND% /path:KLGates.Core.CommandLine\KLGates.Core.CommandLine.nuspec
nuver %VERSIONKIND% /path:KLGates.Core.Data\KLGates.Core.Data.nuspec
nuver %VERSIONKIND% /path:KLGates.Core.Incubation\KLGates.Core.Incubation.nuspec
nuver %VERSIONKIND% /path:KLGates.Core.Logging\KLGates.Core.Logging.nuspec
nuver %VERSIONKIND% /path:KLGates.Core.Security\KLGates.Core.Security.nuspec
nuver %VERSIONKIND% /path:KLGates.Core.ServiceProxy\KLGates.Core.ServiceProxy.nuspec
nuver %VERSIONKIND% /path:KLGates.Core.Testing\KLGates.Core.Testing.nuspec
nuver %VERSIONKIND% /path:KLGates.Platform.Clients\KLGates.Platform.Clients.nuspec

:END