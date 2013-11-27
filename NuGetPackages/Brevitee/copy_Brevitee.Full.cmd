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
copy /Y .\BuildOutput\%VER%\Brevitee.Analytics.dll Brevitee.Full\lib\%LIB%\Brevitee.Analytics.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Distributed.dll Brevitee.Full\lib\%LIB%\Brevitee.Distributed.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Drawing.dll Brevitee.Full\lib\%LIB%\Brevitee.Drawing.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Dust.dll Brevitee.Full\lib\%LIB%\Brevitee.Dust.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Html.dll Brevitee.Full\lib\%LIB%\Brevitee.Html.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Javascript.dll Brevitee.Full\lib\%LIB%\Brevitee.Javascript.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Management.dll Brevitee.Full\lib\%LIB%\Brevitee.Management.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Net.dll Brevitee.Full\lib\%LIB%\Brevitee.Net.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Profiguration.dll Brevitee.Full\lib\%LIB%\Brevitee.Profiguration.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Schema.Org.dll Brevitee.Full\lib\%LIB%\Brevitee.Schema.Org.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Syndication.dll Brevitee.Full\lib\%LIB%\Brevitee.Syndication.dll
copy /Y .\BuildOutput\%VER%\Brevitee.System.dll Brevitee.Full\lib\%LIB%\Brevitee.System.dll
copy /Y .\BuildOutput\%VER%\Brevitee.Yaml.dll Brevitee.Full\lib\%LIB%\Brevitee.Yaml.dll

copy /Y .\BuildOutput\%VER%\Brevitee.Analytics.xml Brevitee.Full\lib\%LIB%\Brevitee.Analytics.xml
copy /Y .\BuildOutput\%VER%\Brevitee.Distributed.xml Brevitee.Full\lib\%LIB%\Brevitee.Distributed.xml
copy /Y .\BuildOutput\%VER%\Brevitee.Drawing.xml Brevitee.Full\lib\%LIB%\Brevitee.Drawing.xml
copy /Y .\BuildOutput\%VER%\Brevitee.Dust.xml Brevitee.Full\lib\%LIB%\Brevitee.Dust.xml
copy /Y .\BuildOutput\%VER%\Brevitee.Html.xml Brevitee.Full\lib\%LIB%\Brevitee.Html.xml
copy /Y .\BuildOutput\%VER%\Brevitee.Javascript.xml Brevitee.Full\lib\%LIB%\Brevitee.Javascript.xml
copy /Y .\BuildOutput\%VER%\Brevitee.Management.xml Brevitee.Full\lib\%LIB%\Brevitee.Management.xml
copy /Y .\BuildOutput\%VER%\Brevitee.Net.xml Brevitee.Full\lib\%LIB%\Brevitee.Net.xml
copy /Y .\BuildOutput\%VER%\Brevitee.Profiguration.xml Brevitee.Full\lib\%LIB%\Brevitee.Profiguration.xml
copy /Y .\BuildOutput\%VER%\Brevitee.Schema.Org.xml Brevitee.Full\lib\%LIB%\Brevitee.Schema.Org.xml
copy /Y .\BuildOutput\%VER%\Brevitee.Syndication.xml Brevitee.Full\lib\%LIB%\Brevitee.Syndication.xml
copy /Y .\BuildOutput\%VER%\Brevitee.System.xml Brevitee.Full\lib\%LIB%\Brevitee.System.xml
copy /Y .\BuildOutput\%VER%\Brevitee.Yaml.xml Brevitee.Full\lib\%LIB%\Brevitee.Yaml.xml
GOTO %NEXT%

:END


