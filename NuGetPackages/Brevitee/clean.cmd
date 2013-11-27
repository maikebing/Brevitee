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
del /F /Q .\Brevitee\lib\%LIB%\*
del /F /Q .\Brevitee.CommandLine\lib\%LIB%\*
del /F /Q .\Brevitee.Data\lib\%LIB%\*
del /F /Q .\Brevitee.Full\lib\%LIB%\*
del /F /Q .\Brevitee.Incubation\lib\%LIB%\*
del /F /Q .\Brevitee.Logging\lib\%LIB%\*
del /F /Q .\Brevitee.ServiceProxy\lib\%LIB%\*
del /F /Q .\Brevitee.Testing\lib\%LIB%\*
del /F /Q .\Brevitee.Users\lib\%LIB%\*

GOTO %NEXT%

:END


