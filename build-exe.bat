@echo off
setlocal
cd /d "%~dp0"

echo Building generic dist desktop launcher...
dotnet publish DistLauncher.csproj -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:EnableCompressionInSingleFile=true /p:DebugType=None /p:DebugSymbols=false

if errorlevel 1 (
  echo.
  echo Build failed. Please check the messages above.
  pause
  exit /b 1
)

if not exist "release" mkdir "release"
copy /Y "bin\Release\net8.0\win-x64\publish\DistDesktopLauncher.exe" "release\DistDesktopLauncher.exe" >nul
copy /Y "start-dist-launcher.bat" "release\start-dist-launcher.bat" >nul

echo.
echo Build complete.
echo Output folder: %CD%\release
pause
