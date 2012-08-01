@ECHO OFF
CLS

SET Root=%~dp0\..
SET Run=%Root%\run
SET Src=%Root%\src
SET Bin=%Root%\bin
SET Powershell=%Run%\powershell
pushd %Run%

:MENU
ECHO.
ECHO   1. Build all solutions
ECHO   2. Configure your environment
ECHO   3. Rebase from Upstream
ECHO   4. Run Wonga.QA.Tests
ECHO   5. Run Meta and Core tests
ECHO   6. Run Wonga.QA.Generators
ECHO   7. Service test configuration
ECHO   0. Exit
ECHO.

CHOICE /C 12345670 /M "But if you already know, how can I make a choice?" /N

IF ERRORLEVEL 8 GOTO EOF
IF ERRORLEVEL 7 GOTO 7
IF ERRORLEVEL 6 GOTO 6
IF ERRORLEVEL 5 GOTO 5
IF ERRORLEVEL 4 GOTO 4
IF ERRORLEVEL 3 GOTO 3
IF ERRORLEVEL 2 GOTO 2
IF ERRORLEVEL 1 GOTO 1
GOTO EOF

:1
	rake build
GOTO MENU

:2
	SET /P TestingTarget=Enter your testing target(v3 [deployto] flag):
	SETX QAFTestTarget %TestingTarget%
GOTO MENU

:3
	CALL git remote add upstream git@github.com:QuickbridgeLtd/v3QA.git 2> NUL
	CALL git pull --rebase upstream master
	IF ERRORLEVEL 1 EXIT /B
GOTO MENU

:4
	SET include=
	SET exclude=
	SET filter=
	SET /P include=Projects to include(colon-separated)(e.g. Tests.Meta:Tests.Api:DataTests.*): 
	SET /P exclude=Projects to exclude(colon-separated, empty for no exclusions)(e.g. Tests.Core): 
	SET /P filter=Filter to use(colon-separated, empty for no filter)(e.g. Tests.Core): 
	rake test include=%include% exclude=%exclude% filter=%filter%|| PAUSE
GOTO MENU

:5
	rake sanity_test || PAUSE
GOTO MENU

:6
	SET /P Origin=Path to v3 [..\v3]: 
	CHOICE /C ACM /M "Api, Cs or Msmq"	
	IF ERRORLEVEL 3 CALL :GENERATE Msmq
	IF ERRORLEVEL 2 CALL :GENERATE Cs
	IF ERRORLEVEL 1 CALL :GENERATE Api
GOTO MENU

:7
 
ECHO.
ECHO   1. Configure service test
ECHO   2. Restore orginal configuration
ECHO   0. Menu
ECHO.

CHOICE /C 120 /M "But if you already know, how can I make a choice?" /N
IF ERRORLEVEL 3 GOTO MENU
IF ERRORLEVEL 2 GOTO 72
IF ERRORLEVEL 1 GOTO 71

GOTO EOF

:71 
	SET /P ServiceTest=Enter your server name (e.g. risk):
	REM SETX QAFTestTarget %TestingTarget%	
	powershell -command "& {. %Powershell%\EditEndPointConfig.ps1; configservice %ServiceTest% }"
 GOTO MENU
 
:72
	SET /P ServiceTest=Enter your server name to restore(e.g.risk):
	REM SETX QAFTestTarget %TestingTarget%
	powershell -command "& {. %Powershell%\EditEndPointConfig.ps1; configservice_undo %ServiceTest% }"
 GOTO MENU

:GENERATE
	rake generators || PAUSE
	%Bin%\Wonga.QA.Generators.%1.exe %Origin%
GOTO EOF

:EOF