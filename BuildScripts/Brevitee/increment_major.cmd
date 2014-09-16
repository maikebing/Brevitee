@echo off
call remove_nuspec_ro.cmd
nuver /major /path:Brevitee\Brevitee.nuspec 
nuver /major /path:Brevitee.Analytics\Brevitee.Analytics.nuspec
nuver /major /path:Brevitee.Automation\Brevitee.Automation.nuspec
nuver /major /path:Brevitee.CommandLine\Brevitee.CommandLine.nuspec 
nuver /major /path:Brevitee.Data\Brevitee.Data.nuspec
nuver /major /path:Brevitee.Distributed\Brevitee.Distributed.nuspec
nuver /major /path:Brevitee.Drawing\Brevitee.Drawing.nuspec
nuver /major /path:Brevitee.Dust\Brevitee.Dust.nuspec
nuver /major /path:Brevitee.Encryption\Brevitee.Encryption.nuspec
nuver /major /path:Brevitee.Html\Brevitee.Html.nuspec
nuver /major /path:Brevitee.Incubation\Brevitee.Incubation.nuspec 
nuver /major /path:Brevitee.Javascript\Brevitee.Javascript.nuspec
nuver /major /path:Brevitee.Logging\Brevitee.Logging.nuspec 
nuver /major /path:Brevitee.Management\Brevitee.Management.nuspec
nuver /major /path:Brevitee.Messaging\Brevitee.Messaging.nuspec
nuver /major /path:Brevitee.Net\Brevitee.Net.nuspec
nuver /major /path:Brevitee.Profiguration\Brevitee.Profiguration.nuspec
nuver /major /path:Brevitee.Schema.Org\Brevitee.Schema.Org.nuspec
nuver /major /path:Brevitee.Server\Brevitee.Server.nuspec 
nuver /major /path:Brevitee.ServiceProxy\Brevitee.ServiceProxy.nuspec 
nuver /major /path:Brevitee.SourceControl\Brevitee.SourceControl.nuspec 
nuver /major /path:Brevitee.Syndication\Brevitee.Syndication.nuspec 
nuver /major /path:Brevitee.Testing\Brevitee.Testing.nuspec 
nuver /major /path:Brevitee.UserAccounts\Brevitee.UserAccounts.nuspec
nuver /major /path:Brevitee.Yaml\Brevitee.Yaml.nuspec