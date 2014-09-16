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
MD Brevitee.Analytics\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Analytics.dll Brevitee.Analytics\lib\%LIB%\Brevitee.Analytics.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Analytics.xml Brevitee.Analytics\lib\%LIB%\Brevitee.Analytics.xml
GOTO %NEXT%

:END


