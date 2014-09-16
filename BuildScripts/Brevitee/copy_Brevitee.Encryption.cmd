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
MD Brevitee.Encryption\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Encryption.dll Brevitee.Encryption\lib\%LIB%\Brevitee.Encryption.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Encryption.xml Brevitee.Encryption\lib\%LIB%\Brevitee.Encryption.xml
GOTO %NEXT%

:END


