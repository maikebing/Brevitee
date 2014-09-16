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
MD Brevitee.Logging\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Logging.dll Brevitee.Logging\lib\%LIB%\Brevitee.Logging.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Logging.xml Brevitee.Logging\lib\%LIB%\Brevitee.Logging.xml
GOTO %NEXT%

:END


