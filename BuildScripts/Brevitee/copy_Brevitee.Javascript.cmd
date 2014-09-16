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
MD Brevitee.Javascript\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Javascript.dll Brevitee.Javascript\lib\%LIB%\Brevitee.Javascript.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Javascript.xml Brevitee.Javascript\lib\%LIB%\Brevitee.Javascript.xml
GOTO %NEXT%

:END


