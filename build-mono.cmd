@echo off & setlocal enableextensions enabledelayedexpansion

:: set mono environment
set path=%path%;"C:\Program Files\Mono-2.10.6\bin\"

:: build
xbuild /p:CefGlueMono=true %* "build.proj"
if "%errorlevel%" neq "0" goto errBuild
goto :eof

:: errors
:errBuild
echo.Error: Build error.
exit /b 1

:errSetEnv
echo.Error: build-setenv error.
exit /b 1
