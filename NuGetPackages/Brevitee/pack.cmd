@echo off
set EnableNuGetPackageRestore=true
call clean.cmd

nuget pack Brevitee.Full\Brevitee.Full.nuspec
nuget pack Brevitee\Brevitee.nuspec
nuget pack Brevitee.CommandLine\Brevitee.CommandLine.nuspec
nuget pack Brevitee.Data\Brevitee.Data.nuspec
nuget pack Brevitee.Incubation\Brevitee.Incubation.nuspec
nuget pack Brevitee.Logging\Brevitee.Logging.nuspec
nuget pack Brevitee.Users\Brevitee.Users.nuspec
nuget pack Brevitee.ServiceProxy\Brevitee.ServiceProxy.nuspec
nuget pack Brevitee.Testing\Brevitee.Testing.nuspec

move /Y *.nupkg Packages\
