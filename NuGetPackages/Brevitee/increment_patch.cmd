@echo off
call remove_nuspec_ro.cmd
nuver /patch /path:Brevitee.Full\Brevitee.Full.nuspec
nuver /patch /path:Brevitee\Brevitee.nuspec
nuver /patch /path:Brevitee.CommandLine\Brevitee.CommandLine.nuspec
nuver /patch /path:Brevitee.Data\Brevitee.Data.nuspec
nuver /patch /path:Brevitee.Incubation\Brevitee.Incubation.nuspec
nuver /patch /path:Brevitee.Logging\Brevitee.Logging.nuspec
nuver /patch /path:Brevitee.Users\Brevitee.Users.nuspec
nuver /patch /path:Brevitee.ServiceProxy\Brevitee.ServiceProxy.nuspec
nuver /patch /path:Brevitee.Testing\Brevitee.Testing.nuspec
