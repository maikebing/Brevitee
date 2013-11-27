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
copy /Y .\BuildOutput\%VER%\KLGates.Core.ServiceProxy.dll KLGates.Core.ServiceProxy\lib\%LIB%\KLGates.Core.ServiceProxy.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.ServiceProxy.xml KLGates.Core.ServiceProxy\lib\%LIB%\KLGates.Core.ServiceProxy.xml
GOTO %NEXT%

:END


