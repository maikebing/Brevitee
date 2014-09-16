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
MD Brevitee.Drawing\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Drawing.dll Brevitee.Drawing\lib\%LIB%\Brevitee.Drawing.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Drawing.xml Brevitee.Drawing\lib\%LIB%\Brevitee.Drawing.xml
GOTO %NEXT%

:END


