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
copy /Y .\BuildOutput\%VER%\KLGates.Core.Incubation.dll KLGates.Core.Incubation\lib\%LIB%\KLGates.Core.Incubation.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Incubation.xml KLGates.Core.Incubation\lib\%LIB%\KLGates.Core.Incubation.xml
GOTO %NEXT%

:END


