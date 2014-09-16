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
MD Brevitee.Distributed\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Distributed.dll Brevitee.Distributed\lib\%LIB%\Brevitee.Distributed.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Distributed.xml Brevitee.Distributed\lib\%LIB%\Brevitee.Distributed.xml
GOTO %NEXT%

:END


