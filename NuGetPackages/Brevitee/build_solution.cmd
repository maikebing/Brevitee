@echo off
SET OutputPath=C:\src\Brevitee\Brevitee\NuGetPackages\Brevitee\BuildOutput\

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
.\msbuild /t:Build /m /filelogger /p:AutoGenerateBindingRedirects=true;GenerateDocumentation=true;OutputPath=%OutputPath%%VER%;Configuration=Debug;Platform="Any CPU";CompilerVersion=%VER% c:\src\Brevitee\Brevitee\Brevitee.Nuget.sln

GOTO %NEXT%

:END