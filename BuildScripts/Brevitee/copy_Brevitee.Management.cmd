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
MD Brevitee.Management\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Management.dll Brevitee.Management\lib\%LIB%\Brevitee.Management.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Management.xml Brevitee.Management\lib\%LIB%\Brevitee.Management.xml
GOTO %NEXT%

:END


