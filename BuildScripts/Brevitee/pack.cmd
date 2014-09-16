@echo off
set EnableNuGetPackageRestore=true

nuget pack Brevitee\Brevitee.nuspec 
nuget pack Brevitee.Analytics\Brevitee.Analytics.nuspec
nuget pack Brevitee.Automation\Brevitee.Automation.nuspec
nuget pack Brevitee.CommandLine\Brevitee.CommandLine.nuspec 
nuget pack Brevitee.Data\Brevitee.Data.nuspec
nuget pack Brevitee.Distributed\Brevitee.Distributed.nuspec
nuget pack Brevitee.Drawing\Brevitee.Drawing.nuspec
nuget pack Brevitee.Dust\Brevitee.Dust.nuspec
nuget pack Brevitee.Encryption\Brevitee.Encryption.nuspec
nuget pack Brevitee.Html\Brevitee.Html.nuspec
nuget pack Brevitee.Incubation\Brevitee.Incubation.nuspec 
nuget pack Brevitee.Javascript\Brevitee.Javascript.nuspec
nuget pack Brevitee.Logging\Brevitee.Logging.nuspec 
nuget pack Brevitee.Management\Brevitee.Management.nuspec
nuget pack Brevitee.Messaging\Brevitee.Messaging.nuspec
nuget pack Brevitee.Net\Brevitee.Net.nuspec
nuget pack Brevitee.Profiguration\Brevitee.Profiguration.nuspec
nuget pack Brevitee.Schema.Org\Brevitee.Schema.Org.nuspec
nuget pack Brevitee.Server\Brevitee.Server.nuspec 
nuget pack Brevitee.ServiceProxy\Brevitee.ServiceProxy.nuspec 
nuget pack Brevitee.SourceControl\Brevitee.SourceControl.nuspec 
nuget pack Brevitee.Syndication\Brevitee.Syndication.nuspec 
nuget pack Brevitee.Testing\Brevitee.Testing.nuspec 
nuget pack Brevitee.UserAccounts\Brevitee.UserAccounts.nuspec
nuget pack Brevitee.Yaml\Brevitee.Yaml.nuspec

MD Packages
move /Y *.nupkg Packages\
