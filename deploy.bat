@echo off
set PackagePath=%1
set TargetAddress=%2
set TargetUsername=%3
set TargetPassword=%4
set WebsiteName=%5

"c:\program files (x86)\IIS\Microsoft Web Deploy V2\msdeploy.exe" -verb:sync -source:package=%PackagePath% -dest:auto,computerName=%TargetAddress%,userName=%TargetUsername%,password=%TargetPassword% -allowUntrusted -setParam:"IIS Web Application Name"=%WebsiteName%