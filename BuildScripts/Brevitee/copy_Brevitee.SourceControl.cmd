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
MD Brevitee.SourceControl\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.SourceControl.dll Brevitee.SourceControl\lib\%LIB%\Brevitee.SourceControl.dll
copy /Y .\BuildOutput\%VER%\Brevitee.SourceControl.xml Brevitee.SourceControl\lib\%LIB%\Brevitee.SourceControl.xml
GOTO %NEXT%

:END


