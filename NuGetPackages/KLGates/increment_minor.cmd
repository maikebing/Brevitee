@echo off
call remove_nuspec_ro.cmd
nuver /minor /path:KLGates.Core.Full\KLGates.Core.Full.nuspec
nuver /minor /path:KLGates.Core\KLGates.Core.nuspec
nuver /minor /path:KLGates.Core.CommandLine\KLGates.Core.CommandLine.nuspec
nuver /minor /path:KLGates.Core.Data\KLGates.Core.Data.nuspec
nuver /minor /path:KLGates.Core.Incubation\KLGates.Core.Incubation.nuspec
nuver /minor /path:KLGates.Core.Logging\KLGates.Core.Logging.nuspec
nuver /minor /path:KLGates.Core.Security\KLGates.Core.Security.nuspec
nuver /minor /path:KLGates.Core.ServiceProxy\KLGates.Core.ServiceProxy.nuspec
nuver /minor /path:KLGates.Core.Testing\KLGates.Core.Testing.nuspec
nuver /minor /path:KLGates.Platform.Clients\KLGates.Platform.Clients.nuspec
