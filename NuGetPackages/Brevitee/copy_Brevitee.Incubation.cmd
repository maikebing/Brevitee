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
copy /Y .\BuildOutput\%VER%\Brevitee.Incubation.dll Brevitee.Incubation\lib\%LIB%\Brevitee.Incubation.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Incubation.xml Brevitee.Incubation\lib\%LIB%\Brevitee.Incubation.xml
GOTO %NEXT%

:END


