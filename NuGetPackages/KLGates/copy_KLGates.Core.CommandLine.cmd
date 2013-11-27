@echo off

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
copy /Y .\BuildOutput\%VER%\KLGates.Core.CommandLine.dll KLGates.Core.CommandLine\lib\%LIB%\KLGates.Core.CommandLine.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.CommandLine.xml KLGates.Core.CommandLine\lib\%LIB%\KLGates.Core.CommandLine.xml
GOTO %NEXT%

:END


