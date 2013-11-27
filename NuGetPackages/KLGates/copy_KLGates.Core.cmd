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
copy /Y .\BuildOutput\%VER%\KLGates.Core.dll KLGates.Core\lib\%LIB%\KLGates.Core.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.xml KLGates.Core\lib\%LIB%\KLGates.Core.xml
GOTO %NEXT%

:END


