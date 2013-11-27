@echo off
call remove_nuspec_ro.cmd
nuver /major /path:KLGates.Core.Full\KLGates.Core.Full.nuspec
nuver /major /path:KLGates.Core\KLGates.Core.nuspec
nuver /major /path:KLGates.Core.CommandLine\KLGates.Core.CommandLine.nuspec
nuver /major /path:KLGates.Core.Data\KLGates.Core.Data.nuspec
nuver /major /path:KLGates.Core.Incubation\KLGates.Core.Incubation.nuspec
nuver /major /path:KLGates.Core.Logging\KLGates.Core.Logging.nuspec
nuver /major /path:KLGates.Core.Security\KLGates.Core.Security.nuspec
nuver /major /path:KLGates.Core.ServiceProxy\KLGates.Core.ServiceProxy.nuspec
nuver /major /path:KLGates.Core.Testing\KLGates.Core.Testing.nuspec
nuver /major /path:KLGates.Platform.Clients\KLGates.Platform.Clients.nuspec
