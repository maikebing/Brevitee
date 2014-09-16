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
MD Brevitee.Yaml\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Yaml.dll Brevitee.Yaml\lib\%LIB%\Brevitee.Yaml.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Yaml.xml Brevitee.Yaml\lib\%LIB%\Brevitee.Yaml.xml
GOTO %NEXT%

:END


