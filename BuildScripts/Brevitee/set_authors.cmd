@echo off
call remove_nuspec_ro.cmd
nuver /a:%1 /path:Brevitee.nuspec 
nuver /a:%1 /path:Brevitee.Analytics.nuspec
nuver /a:%1 /path:Brevitee.Automation.nuspec
nuver /a:%1 /path:Brevitee.CommandLine.nuspec 
nuver /a:%1 /path:Brevitee.Data.nuspec
nuver /a:%1 /path:Brevitee.Distributed.nuspec
nuver /a:%1 /path:Brevitee.Drawing.nuspec
nuver /a:%1 /path:Brevitee.Dust.nuspec
nuver /a:%1 /path:Brevitee.Encryption.nuspec
nuver /a:%1 /path:Brevitee.Html.nuspec
nuver /a:%1 /path:Brevitee.Incubation.nuspec 
nuver /a:%1 /path:Brevitee.Javascript.nuspec
nuver /a:%1 /path:Brevitee.Logging.nuspec 
nuver /a:%1 /path:Brevitee.Management.nuspec
nuver /a:%1 /path:Brevitee.Messaging.nuspec
nuver /a:%1 /path:Brevitee.Net.nuspec
nuver /a:%1 /path:Brevitee.Profiguration.nuspec
nuver /a:%1 /path:Brevitee.Schema.Org.nuspec
nuver /a:%1 /path:Brevitee.Server.nuspec 
nuver /a:%1 /path:Brevitee.ServiceProxy.nuspec 
nuver /a:%1 /path:Brevitee.SourceControl.nuspec 
nuver /a:%1 /path:Brevitee.Syndication.nuspec 
nuver /a:%1 /path:Brevitee.Testing.nuspec 
nuver /a:%1 /path:Brevitee.UserAccounts.nuspec
nuver /a:%1 /path:Brevitee.Yaml.nuspec