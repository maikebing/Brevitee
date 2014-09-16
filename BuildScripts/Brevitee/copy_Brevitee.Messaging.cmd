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
MD Brevitee.Messaging\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Messaging.dll Brevitee.Messaging\lib\%LIB%\Brevitee.Messaging.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Messaging.xml Brevitee.Messaging\lib\%LIB%\Brevitee.Messaging.xml
GOTO %NEXT%

:END


