path=%path%;c:\windows\microsoft.net\framework\v4*
msbuild AndrewMyhre.com.2010.Web\AndrewMyhre.com.2010.Web.csproj /t:Clean;Build;Package /v:m /m:8
deploy "AndrewMyhre.com.2010.Web\obj\Debug\Package\AndrewMyhre.com.2010.Web.csproj.zip" "http://beta.andrewmyhre.com:8172/MsDeploy2/" %1 %2 "Default Web Site"
