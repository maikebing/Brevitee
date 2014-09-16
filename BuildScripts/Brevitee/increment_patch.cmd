@echo off
call remove_nuspec_ro.cmd
nuver /patch /path:BreviteeToolkit\BreviteeToolkit.nuspec
nuver /patch /path:Brevitee\Brevitee.nuspec 
nuver /patch /path:Brevitee.Analytics\Brevitee.Analytics.nuspec
nuver /patch /path:Brevitee.Automation\Brevitee.Automation.nuspec
nuver /patch /path:Brevitee.CommandLine\Brevitee.CommandLine.nuspec 
nuver /patch /path:Brevitee.Data\Brevitee.Data.nuspec
nuver /patch /path:Brevitee.Distributed\Brevitee.Distributed.nuspec
nuver /patch /path:Brevitee.Drawing\Brevitee.Drawing.nuspec
nuver /patch /path:Brevitee.Dust\Brevitee.Dust.nuspec
nuver /patch /path:Brevitee.Encryption\Brevitee.Encryption.nuspec
nuver /patch /path:Brevitee.Html\Brevitee.Html.nuspec
nuver /patch /path:Brevitee.Incubation\Brevitee.Incubation.nuspec 
nuver /patch /path:Brevitee.Javascript\Brevitee.Javascript.nuspec
nuver /patch /path:Brevitee.Logging\Brevitee.Logging.nuspec 
nuver /patch /path:Brevitee.Management\Brevitee.Management.nuspec
nuver /patch /path:Brevitee.Messaging\Brevitee.Messaging.nuspec
nuver /patch /path:Brevitee.Net\Brevitee.Net.nuspec
nuver /patch /path:Brevitee.Profiguration\Brevitee.Profiguration.nuspec
nuver /patch /path:Brevitee.Schema.Org\Brevitee.Schema.Org.nuspec
nuver /patch /path:Brevitee.Server\Brevitee.Server.nuspec 
nuver /patch /path:Brevitee.ServiceProxy\Brevitee.ServiceProxy.nuspec 
nuver /patch /path:Brevitee.SourceControl\Brevitee.SourceControl.nuspec 
nuver /patch /path:Brevitee.Syndication\Brevitee.Syndication.nuspec 
nuver /patch /path:Brevitee.Testing\Brevitee.Testing.nuspec 
nuver /patch /path:Brevitee.UserAccounts\Brevitee.UserAccounts.nuspec
nuver /patch /path:Brevitee.Yaml\Brevitee.Yaml.nuspec