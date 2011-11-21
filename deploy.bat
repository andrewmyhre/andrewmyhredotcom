@echo off
set PackagePath=%1
set TargetAddress=%2
set TargetUsername=%3
set TargetPassword=%4
set WebsiteName=%5

msdeploy -verb:sync -source:package=%PackagePath% -dest:auto,computerName=%TargetAddress%,userName=%TargetUsername%,password=%TargetPassword% -allowUntrusted -setParam:"IIS Web Application Name"=%WebsiteName%