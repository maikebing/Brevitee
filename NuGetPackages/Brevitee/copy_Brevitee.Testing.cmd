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
copy /Y .\BuildOutput\%VER%\Brevitee.Testing.dll Brevitee.Testing\lib\%LIB%\Brevitee.Testing.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Testing.xml Brevitee.Testing\lib\%LIB%\Brevitee.Testing.xml
GOTO %NEXT%

:END


