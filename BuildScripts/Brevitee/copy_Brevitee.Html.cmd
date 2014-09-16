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
MD Brevitee.Html\lib\%LIB%
copy /Y .\BuildOutput\%VER%\Brevitee.Html.dll Brevitee.Html\lib\%LIB%\Brevitee.Html.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Html.xml Brevitee.Html\lib\%LIB%\Brevitee.Html.xml
copy /Y ..\..\Assemblies\wkhtmltopdf.exe Brevitee.Html\lib\%LIB%\wkhtmltopdf.exe
copy /Y ..\..\Assemblies\wkhtmltox.dll Brevitee.Html\lib\%LIB%\wkhtmltox.dll
GOTO %NEXT%

:END


