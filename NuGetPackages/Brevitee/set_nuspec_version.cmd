echo off
SET VERSIONKIND=%1
if %VERSIONKIND%=="" GOTO END
echo %VERSIONKIND%

nuver %VERSIONKIND% /path:Brevitee.Full\Brevitee.Full.nuspec
nuver %VERSIONKIND% /path:Brevitee\Brevitee.nuspec
nuver %VERSIONKIND% /path:Brevitee.CommandLine\Brevitee.CommandLine.nuspec
nuver %VERSIONKIND% /path:Brevitee.Data\Brevitee.Data.nuspec
nuver %VERSIONKIND% /path:Brevitee.Incubation\Brevitee.Incubation.nuspec
nuver %VERSIONKIND% /path:Brevitee.Logging\Brevitee.Logging.nuspec
nuver %VERSIONKIND% /path:Brevitee.Users\Brevitee.Users.nuspec
nuver %VERSIONKIND% /path:Brevitee.ServiceProxy\Brevitee.ServiceProxy.nuspec
nuver %VERSIONKIND% /path:Brevitee.Testing\Brevitee.Testing.nuspec

:END