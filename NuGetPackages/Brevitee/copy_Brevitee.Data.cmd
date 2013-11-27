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
copy /Y .\BuildOutput\%VER%\Brevitee.Data.dll Brevitee.Data\lib\%LIB%\Brevitee.Data.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Data.xml Brevitee.Data\lib\%LIB%\Brevitee.Data.xml
copy /Y .\BuildOutput\%VER%\Brevitee.Data.Model.dll Brevitee.Data\lib\%LIB%\Brevitee.Data.Model.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Data.MsSql.dll Brevitee.Data\lib\%LIB%\Brevitee.Data.MsSql.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Data.Schema.dll Brevitee.Data\lib\%LIB%\Brevitee.Data.Schema.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Data.SQLite.dll Brevitee.Data\lib\%LIB%\Brevitee.Data.SQLite.dll
GOTO %NEXT%

:END


