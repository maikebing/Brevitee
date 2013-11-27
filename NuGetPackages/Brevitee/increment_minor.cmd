@echo off
call remove_nuspec_ro.cmd
nuver /minor /path:Brevitee.Full\Brevitee.Full.nuspec
nuver /minor /path:Brevitee\Brevitee.nuspec
nuver /minor /path:Brevitee.CommandLine\Brevitee.CommandLine.nuspec
nuver /minor /path:Brevitee.Data\Brevitee.Data.nuspec
nuver /minor /path:Brevitee.Incubation\Brevitee.Incubation.nuspec
nuver /minor /path:Brevitee.Logging\Brevitee.Logging.nuspec
nuver /minor /path:Brevitee.Users\Brevitee.Users.nuspec
nuver /minor /path:Brevitee.ServiceProxy\Brevitee.ServiceProxy.nuspec
nuver /minor /path:Brevitee.Testing\Brevitee.Testing.nuspec
