@echo off
SET OutputPath=C:\src\Brevitee\Brevitee\BuildScripts\Brevitee\BuildOutput\

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
.\MSBuild\MSBuild.exe /t:Build /m /filelogger /p:AutoGenerateBindingRedirects=true;GenerateDocumentation=true;OutputPath=%OutputPath%%VER%;Configuration=Release;Platform="Any CPU";CompilerVersion=%VER% c:\src\Brevitee\Brevitee\Brevitee.Nuget.sln

IF ERRORLEVEL 1 GOTO END

GOTO %NEXT%

:END
EXIT /b %ERRORLEVEL%