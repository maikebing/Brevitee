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
copy /Y .\BuildOutput\%VER%\Brevitee.CommandLine.dll Brevitee.CommandLine\lib\%LIB%\Brevitee.CommandLine.dll
copy /Y .\BuildOutput\%VER%\Brevitee.CommandLine.xml Brevitee.CommandLine\lib\%LIB%\Brevitee.CommandLine.xml
GOTO %NEXT%

:END


