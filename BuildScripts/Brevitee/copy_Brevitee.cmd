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
MD Brevitee\lib\%LIB%\
copy /Y /D .\BuildOutput\%VER%\Brevitee.dll Brevitee\lib\%LIB%\Brevitee.dll
copy /Y /D .\BuildOutput\%VER%\Brevitee.xml Brevitee\lib\%LIB%\Brevitee.xml
GOTO %NEXT%

:END


