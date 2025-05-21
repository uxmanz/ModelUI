@echo off
setlocal EnableDelayedExpansion

:: Prompt for new app name
set /p AppName=Enter new application name: 

:: Validate input
if "%AppName%"=="" (
    echo Application name cannot be empty.
    goto :eof
)

:: Variables
set "OldName=ModelUI"
set "NewName=%AppName%"
set "SolutionFile=%OldName%.sln"
set "ProjectFolder=%OldName%"
set "ProjectFile=%OldName%.csproj"

echo Renaming application from %OldName% to %NewName%...
echo.

:: Step 1: Replace and rename solution file
if exist "%SolutionFile%" (
    powershell -Command "(Get-Content '%SolutionFile%') -replace '%OldName%', '%NewName%' | Set-Content '%SolutionFile%'"
    ren "%SolutionFile%" "%NewName%.sln"
    echo Solution file updated and renamed.
) else (
    echo ERROR: Solution file not found!
)

:: Step 2: Replace and rename .csproj
if exist "%ProjectFolder%\%ProjectFile%" (
    powershell -Command "(Get-Content '%ProjectFolder%\%ProjectFile%') -replace '%OldName%', '%NewName%' | Set-Content '%ProjectFolder%\%ProjectFile%'"
    pushd "%ProjectFolder%"
    ren "%ProjectFile%" "%NewName%.csproj"
    popd
    echo Project file updated and renamed.
) else (
    echo ERROR: Project file not found!
)

:: Step 3: Update all .cs and .axaml files recursively
echo Updating .cs and .axaml files...
for /r "%ProjectFolder%" %%f in (*.cs *.axaml) do (
    powershell -Command "(Get-Content '%%f') -replace '%OldName%', '%NewName%' | Set-Content '%%f'"
)
echo All .cs and .axaml files updated.

:: Step 4: Rename folder
if exist "%ProjectFolder%" (
    ren "%ProjectFolder%" "%NewName%"
    echo Folder renamed to %NewName%.
) else (
    echo ERROR: Folder %ProjectFolder% not found!
)

echo.
echo ✅ Application renamed to %NewName% successfully.
pause
