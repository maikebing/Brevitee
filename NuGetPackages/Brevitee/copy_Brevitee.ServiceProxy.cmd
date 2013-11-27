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
copy /Y .\BuildOutput\%VER%\Brevitee.ServiceProxy.dll Brevitee.ServiceProxy\lib\%LIB%\Brevitee.ServiceProxy.dll
copy /Y .\BuildOutput\%VER%\Brevitee.ServiceProxy.xml Brevitee.ServiceProxy\lib\%LIB%\Brevitee.ServiceProxy.xml
GOTO %NEXT%

:END


