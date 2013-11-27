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
copy /Y .\BuildOutput\%VER%\KLGates.Core.Testing.dll KLGates.Core.Testing\lib\%LIB%\KLGates.Core.Testing.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Testing.xml KLGates.Core.Testing\lib\%LIB%\KLGates.Core.Testing.xml
GOTO %NEXT%

:END


