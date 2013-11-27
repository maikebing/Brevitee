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
copy /Y .\BuildOutput\%VER%\KLGates.Core.Data.dll KLGates.Core.Data\lib\%LIB%\KLGates.Core.Data.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Data.xml KLGates.Core.Data\lib\%LIB%\KLGates.Core.Data.xml
copy /Y .\BuildOutput\%VER%\KLGates.Core.Data.Model.dll KLGates.Core.Data\lib\%LIB%\KLGates.Core.Data.Model.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Data.MsSql.dll KLGates.Core.Data\lib\%LIB%\KLGates.Core.Data.MsSql.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Data.Schema.dll KLGates.Core.Data\lib\%LIB%\KLGates.Core.Data.Schema.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Data.SQLite.dll KLGates.Core.Data\lib\%LIB%\KLGates.Core.Data.SQLite.dll
GOTO %NEXT%

:END


