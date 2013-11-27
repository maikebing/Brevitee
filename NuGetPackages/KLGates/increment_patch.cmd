@echo off
call remove_nuspec_ro.cmd
nuver /patch /path:KLGates.Core.Full\KLGates.Core.Full.nuspec
nuver /patch /path:KLGates.Core\KLGates.Core.nuspec
nuver /patch /path:KLGates.Core.CommandLine\KLGates.Core.CommandLine.nuspec
nuver /patch /path:KLGates.Core.Data\KLGates.Core.Data.nuspec
nuver /patch /path:KLGates.Core.Incubation\KLGates.Core.Incubation.nuspec
nuver /patch /path:KLGates.Core.Logging\KLGates.Core.Logging.nuspec
nuver /patch /path:KLGates.Core.Security\KLGates.Core.Security.nuspec
nuver /patch /path:KLGates.Core.ServiceProxy\KLGates.Core.ServiceProxy.nuspec
nuver /patch /path:KLGates.Core.Testing\KLGates.Core.Testing.nuspec
nuver /patch /path:KLGates.Platform.Clients\KLGates.Platform.Clients.nuspec
