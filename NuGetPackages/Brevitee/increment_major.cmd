@echo off
call remove_nuspec_ro.cmd
nuver /major /path:Brevitee.Full\Brevitee.Full.nuspec
nuver /major /path:Brevitee\Brevitee.nuspec
nuver /major /path:Brevitee.CommandLine\Brevitee.CommandLine.nuspec
nuver /major /path:Brevitee.Data\Brevitee.Data.nuspec
nuver /major /path:Brevitee.Incubation\Brevitee.Incubation.nuspec
nuver /major /path:Brevitee.Logging\Brevitee.Logging.nuspec
nuver /major /path:Brevitee.Users\Brevitee.Users.nuspec
nuver /major /path:Brevitee.ServiceProxy\Brevitee.ServiceProxy.nuspec
nuver /major /path:Brevitee.Testing\Brevitee.Testing.nuspec
