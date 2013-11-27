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
copy /Y .\BuildOutput\%VER%\KLGates.Business.EnterpriseDataWarehouse.dll KLGates.Core.Full\lib\%LIB%\KLGates.Business.EnterpriseDataWarehouse.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Analytics.dll KLGates.Core.Full\lib\%LIB%\KLGates.Core.Analytics.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Distributed.dll KLGates.Core.Full\lib\%LIB%\KLGates.Core.Distributed.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Drawing.dll KLGates.Core.Full\lib\%LIB%\KLGates.Core.Drawing.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Dust.dll KLGates.Core.Full\lib\%LIB%\KLGates.Core.Dust.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Html.dll KLGates.Core.Full\lib\%LIB%\KLGates.Core.Html.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Javascript.dll KLGates.Core.Full\lib\%LIB%\KLGates.Core.Javascript.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Management.dll KLGates.Core.Full\lib\%LIB%\KLGates.Core.Management.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Net.dll KLGates.Core.Full\lib\%LIB%\KLGates.Core.Net.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Profiguration.dll KLGates.Core.Full\lib\%LIB%\KLGates.Core.Profiguration.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Schema.Org.dll KLGates.Core.Full\lib\%LIB%\KLGates.Core.Schema.Org.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Syndication.dll KLGates.Core.Full\lib\%LIB%\KLGates.Core.Syndication.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.System.dll KLGates.Core.Full\lib\%LIB%\KLGates.Core.System.dll
copy /Y .\BuildOutput\%VER%\KLGates.Core.Yaml.dll KLGates.Core.Full\lib\%LIB%\KLGates.Core.Yaml.dll
copy /Y .\BuildOutput\%VER%\KLGates.Business.EnterpriseDataWarehouse.xml KLGates.Core.Full\lib\%LIB%\KLGates.Business.EnterpriseDataWarehouse.xml
copy /Y .\BuildOutput\%VER%\KLGates.Core.Analytics.xml KLGates.Core.Full\lib\%LIB%\KLGates.Core.Analytics.xml
copy /Y .\BuildOutput\%VER%\KLGates.Core.Distributed.xml KLGates.Core.Full\lib\%LIB%\KLGates.Core.Distributed.xml
copy /Y .\BuildOutput\%VER%\KLGates.Core.Drawing.xml KLGates.Core.Full\lib\%LIB%\KLGates.Core.Drawing.xml
copy /Y .\BuildOutput\%VER%\KLGates.Core.Dust.xml KLGates.Core.Full\lib\%LIB%\KLGates.Core.Dust.xml
copy /Y .\BuildOutput\%VER%\KLGates.Core.Html.xml KLGates.Core.Full\lib\%LIB%\KLGates.Core.Html.xml
copy /Y .\BuildOutput\%VER%\KLGates.Core.Javascript.xml KLGates.Core.Full\lib\%LIB%\KLGates.Core.Javascript.xml
copy /Y .\BuildOutput\%VER%\KLGates.Core.Management.xml KLGates.Core.Full\lib\%LIB%\KLGates.Core.Management.xml
copy /Y .\BuildOutput\%VER%\KLGates.Core.Net.xml KLGates.Core.Full\lib\%LIB%\KLGates.Core.Net.xml
copy /Y .\BuildOutput\%VER%\KLGates.Core.Profiguration.xml KLGates.Core.Full\lib\%LIB%\KLGates.Core.Profiguration.xml
copy /Y .\BuildOutput\%VER%\KLGates.Core.Schema.Org.xml KLGates.Core.Full\lib\%LIB%\KLGates.Core.Schema.Org.xml
copy /Y .\BuildOutput\%VER%\KLGates.Core.Syndication.xml KLGates.Core.Full\lib\%LIB%\KLGates.Core.Syndication.xml
copy /Y .\BuildOutput\%VER%\KLGates.Core.System.xml KLGates.Core.Full\lib\%LIB%\KLGates.Core.System.xml
copy /Y .\BuildOutput\%VER%\KLGates.Core.Yaml.xml KLGates.Core.Full\lib\%LIB%\KLGates.Core.Yaml.xml
GOTO %NEXT%

:END


