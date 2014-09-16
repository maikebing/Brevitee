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
MD Brevitee.Dust\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Dust.dll Brevitee.Dust\lib\%LIB%\Brevitee.Dust.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Dust.xml Brevitee.Dust\lib\%LIB%\Brevitee.Dust.xml
GOTO %NEXT%

:END


