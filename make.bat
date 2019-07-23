@echo off
setlocal enabledelayedexpansion
set devenv="C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\devenv.exe"
set arg=%1
set full=0
:select
if [%arg%]==[] goto noinput
if %arg%==full goto full
if %arg%==clean goto clean
if %arg%==debug goto debug
if %arg%==release goto release
if %arg%==pull goto pull
if %arg%==push goto push
if %arg%==help goto help
echo Invalid.
:noinput
set /p arg="arg: "
goto select
:help
echo [full,clean,debug,release,pull,push,help]
goto noinput
:full
set full=1
echo Please wait a bit.
:clean
echo Cleaning...
set tmp=%cd%
if exist ".NETFramework,Version=v4.7.2.AssemblyAttributes.cs" del ".NETFramework,Version=v4.7.2.AssemblyAttributes.cs"
if exist "vs.mcj719337969" rmdir /s /q "vs.mcj719337969"
if exist "tmp" rmdir /s /q "tmp"
if not %full%==4 if exist "BUILD" rmdir /s /q "BUILD"
for /d %%i in ("%cd%\*") do (
    cd %%i
    if exist "bin" rmdir /s /q "bin"
    if exist "obj" rmdir /s /q "obj"
    cd %tmp%
)
if %full%==1 (
    set full=2
    goto debug
)
if %full%==10 (
    set full=11
    goto push
)
goto exit
:debug
echo Building Debug
%devenv% LaptopSimulator2015.sln /build Debug
if %full%==2 (
    set full=3
    goto release
)
goto :exit
:release
echo Building Release
%devenv% LaptopSimulator2015.sln /build Release
if %full%==3 (
    set full=4
    xcopy /s /i "%cd%\LaptopSimulator2015\bin" "%cd%\BUILD"
    goto clean
)
goto exit
:pull
git pull
goto exit
:push
if %full%==0 (
    set full=10
    goto clean
)
set /p inp1="Message: "
if %full%==11 (
    git add .
    git commit -m "%inp1%"
    git push -u origin master
)
:exit
echo Done
timeout /t 2 /nobreak >nul