rem @echo off

SET LIB=net40
SET VER=v4.0
SET NEXT=NEXT
GOTO COPY

:NEXT
SET LIB=net45
SET VER=v4.5
SET NEXT=END
GOTO COPY

:COPY
del /F /Q .\Brevitee\lib\%LIB%\*
del /F /Q .\Brevitee.Analytics\lib\%LIB%\*
del /F /Q .\Brevitee.Automation\lib\%LIB%\*
del /F /Q .\Brevitee.CommandLine\lib\%LIB%\*
del /F /Q .\Brevitee.Data\lib\%LIB%\*
del /F /Q .\Brevitee.Distributed\lib\%LIB%\*
del /F /Q .\Brevitee.Drawing\lib\%LIB%\*
del /F /Q .\Brevitee.Dust\lib\%LIB%\*
del /F /Q .\Brevitee.Encryption\lib\%LIB%\*
del /F /Q .\Brevitee.Html\lib\%LIB%\Brevitee.Html.*
del /F /Q .\Brevitee.Incubation\lib\%LIB%\*
del /F /Q .\Brevitee.Javascript\lib\%LIB%\*
del /F /Q .\Brevitee.Logging\lib\%LIB%\*
del /F /Q .\Brevitee.Management\lib\%LIB%\*
del /F /Q .\Brevitee.Messaging\lib\%LIB%\*
del /F /Q .\Brevitee.Net\lib\%LIB%\*
del /F /Q .\Brevitee.Profiguration\lib\%LIB%\*
del /F /Q .\Brevitee.Schema.Org\lib\%LIB%\*
del /F /Q .\Brevitee.Server\lib\%LIB%\*
del /F /Q .\Brevitee.ServiceProxy\lib\%LIB%\*
del /F /Q .\Brevitee.SourceControl\lib\%LIB%\*
del /F /Q .\Brevitee.Syndication\lib\%LIB%\*
del /F /Q .\Brevitee.Testing\lib\%LIB%\*
del /F /Q .\Brevitee.UserAccounts\lib\%LIB%\*
del /F /Q .\Brevitee.Yaml\lib\%LIB%\*
del /F /Q .\BreviteeToolkit\lib\%LIB%\*

GOTO %NEXT%

:END

rmdir /S /Q .\BuildOutput
rmdir /S /Q ..\..\Products\BUILD

RMDIR /S /Q ..\..\Brevitee\obj\
RMDIR /S /Q ..\..\Brevitee.Analytics\obj\
RMDIR /S /Q ..\..\Brevitee.Automation\obj\
RMDIR /S /Q ..\..\Brevitee.CommandLine\obj\
RMDIR /S /Q ..\..\Brevitee.Data\obj\
RMDIR /S /Q ..\..\Brevitee.Distributed\obj\
RMDIR /S /Q ..\..\Brevitee.Drawing\obj\
RMDIR /S /Q ..\..\Brevitee.Dust\obj\
RMDIR /S /Q ..\..\Brevitee.Encryption\obj\
RMDIR /S /Q ..\..\Brevitee.Html\obj\
RMDIR /S /Q ..\..\Brevitee.Incubation\obj\
RMDIR /S /Q ..\..\Brevitee.Javascript\obj\
RMDIR /S /Q ..\..\Brevitee.Logging\obj\
RMDIR /S /Q ..\..\Brevitee.Management\obj\
RMDIR /S /Q ..\..\Brevitee.Messaging\obj\
RMDIR /S /Q ..\..\Brevitee.Net\obj\
RMDIR /S /Q ..\..\Brevitee.Profiguration\obj\
RMDIR /S /Q ..\..\Brevitee.Schema.Org\obj\
RMDIR /S /Q ..\..\Brevitee.Server\obj\
RMDIR /S /Q ..\..\Brevitee.ServiceProxy\obj\
RMDIR /S /Q ..\..\Brevitee.SourceControl\obj\
RMDIR /S /Q ..\..\Brevitee.Syndication\obj\
RMDIR /S /Q ..\..\Brevitee.Testing\obj\
RMDIR /S /Q ..\..\Brevitee.UserAccounts\obj\
RMDIR /S /Q ..\..\Brevitee.Yaml\obj\

