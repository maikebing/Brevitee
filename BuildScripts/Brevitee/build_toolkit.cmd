rmdir ..\..\Products\BreviteeToolkit\BreviteeToolkit-cache /S /Q
rmdir ..\..\Products\BreviteeToolkit\BreviteeToolkit-SetupFiles /S /Q
"C:\Program Files (x86)\Caphyon\Advanced Installer 11.4.1\bin\x86\AdvancedInstaller.com" /build ..\..\Products\BreviteeToolkit\BreviteeToolkit.aip
MD .\BreviteeToolkit\lib\net45\
copy ..\..\Products\BreviteeToolkit\BreviteeToolkit-SetupFiles\BreviteeToolkit.msi .\BreviteeToolkit\lib\net45\
nuget pack BreviteeToolkit\BreviteeToolkit.nuspec
MD Packages
move /Y *.nupkg Packages\