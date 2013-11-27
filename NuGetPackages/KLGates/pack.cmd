@echo off
nuget pack KLGates.Core.Full\KLGates.Core.Full.nuspec
nuget pack KLGates.Core\KLGates.Core.nuspec
nuget pack KLGates.Core.CommandLine\KLGates.Core.CommandLine.nuspec
nuget pack KLGates.Core.Data\KLGates.Core.Data.nuspec
nuget pack KLGates.Core.Incubation\KLGates.Core.Incubation.nuspec
nuget pack KLGates.Core.Logging\KLGates.Core.Logging.nuspec
nuget pack KLGates.Core.Security\KLGates.Core.Security.nuspec
nuget pack KLGates.Core.ServiceProxy\KLGates.Core.ServiceProxy.nuspec
nuget pack KLGates.Core.Testing\KLGates.Core.Testing.nuspec
nuget pack KLGates.Platform.Clients\KLGates.Platform.Clients.nuspec

move /Y *.nupkg Packages\
