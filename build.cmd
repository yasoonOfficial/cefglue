@echo off & setlocal enableextensions enabledelayedexpansion

call "build-setenv.cmd"
if "%errorlevel%" neq "0" goto errSetEnv

:: build
MSBuild.exe /p:CefGlueMono=false %* "build.proj"
if "%errorlevel%" neq "0" goto errBuild
goto :eof

:: errors
:errBuild
echo.Error: Build error.
exit /b 1

:errSetEnv
echo.Error: build-setenv error.
exit /b 1
