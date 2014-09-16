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
MD Brevitee.Syndication\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Syndication.dll Brevitee.Syndication\lib\%LIB%\Brevitee.Syndication.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Syndication.xml Brevitee.Syndication\lib\%LIB%\Brevitee.Syndication.xml
GOTO %NEXT%

:END


