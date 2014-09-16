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
MD Brevitee.Profiguration\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Profiguration.dll Brevitee.Profiguration\lib\%LIB%\Brevitee.Profiguration.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Profiguration.xml Brevitee.Profiguration\lib\%LIB%\Brevitee.Profiguration.xml
GOTO %NEXT%

:END


