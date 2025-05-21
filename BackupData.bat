@echo off
set BackupLocation="e:\Backups\"

for /f "delims=" %%# in ('powershell get-date -format "{dd-MM-yyyy-HH-mm-ss}"') do @set mydate=%%#
echo %mydate%
for %%* in (.) do set CurrDirName=%%~nx*
echo %CurrDirName%
echo %BackupLocation%
echo "%mydate%_%CurrDirName%".7z

if not exist %BackupLocation%%CurrDirName% mkdir %BackupLocation%%CurrDirName%

"c:\Program Files\7-Zip\7z.exe" a -r "%mydate%_%CurrDirName%".7z -mx0 -xr!bin -xr!obj -xr!.vs -xr!.git
move *.7z %BackupLocation%%CurrDirName%