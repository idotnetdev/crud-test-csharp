@echo off
:start
cls

set /p Name=Enter Migration Name: 

echo.
echo Migration script has beed executed
echo.

dotnet build
dotnet ef migrations --startup-project ../Mc2.CrudTest.Presentation/Server add %Name% --output-dir DbMigrations --context Data.Context.ApplicationDbContext

:again
set /p choice=Do you want to exit? [Y]es, [N]o: 
if '%choice%' == 'y' goto quite
if '%choice%' == 'Y' goto quite
if '%choice%' == 'n' goto continue
if '%choice%' == 'N' goto continue
goto again

:quite
exit
pause > nul

:continue
goto start
pause > nul