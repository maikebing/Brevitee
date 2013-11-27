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
copy /Y .\BuildOutput\%VER%\KLGates.Core.Logging.dll KLGates.Core.Logging\lib\%LIB%\KLGates.Core.Logging.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Logging.xml KLGates.Core.Logging\lib\%LIB%\KLGates.Core.Logging.xml
GOTO %NEXT%

:END


