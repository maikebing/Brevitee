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
copy /Y .\BuildOutput\%VER%\KLGates.Core.Security.dll KLGates.Core.Security\lib\%LIB%\KLGates.Core.Security.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Security.xml KLGates.Core.Security\lib\%LIB%\KLGates.Core.Security.xml
GOTO %NEXT%

:END


