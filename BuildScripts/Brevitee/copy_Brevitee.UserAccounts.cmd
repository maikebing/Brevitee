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
MD Brevitee.UserAccounts\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.UserAccounts.dll Brevitee.UserAccounts\lib\%LIB%\Brevitee.UserAccounts.dll
copy /Y .\BuildOutput\%VER%\Brevitee.UserAccounts.xml Brevitee.UserAccounts\lib\%LIB%\Brevitee.UserAccounts.xml
GOTO %NEXT%

:END


