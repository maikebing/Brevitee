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
copy /Y .\BuildOutput\%VER%\KLGates.Platform.Clients.dll KLGates.Platform.Clients\lib\%LIB%\KLGates.Platform.Clients.dll
copy /Y .\BuildOutput\%VER%\KLGates.Platform.Clients.xml KLGates.Platform.Clients\lib\%LIB%\KLGates.Platform.Clients.xml
copy /Y .\BuildOutput\%VER%\KLGates.Platform.Contracts.dll KLGates.Platform.Clients\lib\%LIB%\KLGates.Platform.Contracts.dll
copy /Y .\BuildOutput\%VER%\KLGates.Platform.Contracts.xml KLGates.Platform.Clients\lib\%LIB%\KLGates.Platform.Contracts.xml
GOTO %NEXT%

:END


