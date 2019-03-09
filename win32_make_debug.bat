@ECHO off

echo removing linux geckofx-core
rm bin\Win32Debug\Geckofx-Core.dll
echo removing linux geckofx-winforms
rm bin\Win32Debug\Geckofx-Winforms.dll

echo adding win32 geckofx-core 60.0.26 32bit (from nuget)
cp packages\Geckofx60.32.60.0.26\lib\net45\Geckofx-Core.dll bin\Win32Debug\Geckofx-Core.dll

echo adding win32 geckofx-winforms 60.0.26 32bit (from nuget)
cp packages\Geckofx60.32.60.0.26\lib\net45\Geckofx-Winforms.dll bin\Win32Debug\Geckofx-Winforms.dll

echo done