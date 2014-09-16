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
MD Brevitee.Schema.Org\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Schema.Org.dll Brevitee.Schema.Org\lib\%LIB%\Brevitee.Schema.Org.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Schema.Org.xml Brevitee.Schema.Org\lib\%LIB%\Brevitee.Schema.Org.xml
GOTO %NEXT%

:END


