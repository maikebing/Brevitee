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
MD Brevitee.Automation\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Automation.dll Brevitee.Automation\lib\%LIB%\Brevitee.Automation.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Automation.xml Brevitee.Automation\lib\%LIB%\Brevitee.Automation.xml
GOTO %NEXT%

:END


