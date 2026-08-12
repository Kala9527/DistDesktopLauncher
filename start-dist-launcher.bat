@echo off
cd /d "%~dp0"

if not exist "DistDesktopLauncher.exe" (
  echo DistDesktopLauncher.exe was not found.
  echo Please keep this script in the same folder as the exe.
  pause
  exit /b 1
)

start "Dist Desktop Launcher" DistDesktopLauncher.exe
