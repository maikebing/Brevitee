@echo off
SET OutputPath=C:\src\tfs\KLGates.Core\Latest\NuGetPackages\KLGates\BuildOutput\

SET VER=v4.0
SET NEXT=NEXT
GOTO BUILD

:NEXT
SET VER=v4.5
SET NEXT=END
GOTO BUILD

:BUILD

rmdir /Q /S %OutputPath%%VER%
mkdir %OutputPath%%VER%
.\msbuild /t:Build /m /p:GenerateDocumentation=true /p:OutputPath=%OutputPath%%VER% /p:Configuration=Debug /p:Platform="Any CPU" /p:CompilerVersion=%VER% ..\..\KLGates.Core.sln

GOTO %NEXT%

:END