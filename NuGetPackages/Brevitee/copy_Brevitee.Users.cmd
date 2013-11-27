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
copy /Y .\BuildOutput\%VER%\Brevitee.Users.dll Brevitee.Users\lib\%LIB%\Brevitee.Users.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Users.xml Brevitee.Users\lib\%LIB%\Brevitee.Users.xml
GOTO %NEXT%

:END


