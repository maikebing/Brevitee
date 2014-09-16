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
MD Brevitee.Server\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Server.dll Brevitee.Server\lib\%LIB%\Brevitee.Server.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Server.xml Brevitee.Server\lib\%LIB%\Brevitee.Server.xml
GOTO %NEXT%

:END


