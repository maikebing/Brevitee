@echo off
call remove_nuspec_ro.cmd
nuver /o:%1 /path:Brevitee.nuspec 
nuver /o:%1 /path:Brevitee.Analytics.nuspec
nuver /o:%1 /path:Brevitee.Automation.nuspec
nuver /o:%1 /path:Brevitee.CommandLine.nuspec 
nuver /o:%1 /path:Brevitee.Data.nuspec
nuver /o:%1 /path:Brevitee.Distributed.nuspec
nuver /o:%1 /path:Brevitee.Drawing.nuspec
nuver /o:%1 /path:Brevitee.Dust.nuspec
nuver /o:%1 /path:Brevitee.Encryption.nuspec
nuver /o:%1 /path:Brevitee.Html.nuspec
nuver /o:%1 /path:Brevitee.Incubation.nuspec 
nuver /o:%1 /path:Brevitee.Javascript.nuspec
nuver /o:%1 /path:Brevitee.Logging.nuspec 
nuver /o:%1 /path:Brevitee.Management.nuspec
nuver /o:%1 /path:Brevitee.Messaging.nuspec
nuver /o:%1 /path:Brevitee.Net.nuspec
nuver /o:%1 /path:Brevitee.Profiguration.nuspec
nuver /o:%1 /path:Brevitee.Schema.Org.nuspec
nuver /o:%1 /path:Brevitee.Server.nuspec 
nuver /o:%1 /path:Brevitee.ServiceProxy.nuspec 
nuver /o:%1 /path:Brevitee.SourceControl.nuspec 
nuver /o:%1 /path:Brevitee.Syndication.nuspec 
nuver /o:%1 /path:Brevitee.Testing.nuspec 
nuver /o:%1 /path:Brevitee.UserAccounts.nuspec
nuver /o:%1 /path:Brevitee.Yaml.nuspec