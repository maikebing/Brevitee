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
MD Brevitee.Net\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Net.dll Brevitee.Net\lib\%LIB%\Brevitee.Net.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Net.xml Brevitee.Net\lib\%LIB%\Brevitee.Net.xml
GOTO %NEXT%

:END


