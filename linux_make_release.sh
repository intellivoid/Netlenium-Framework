#gecko_linux="packages/Geckofx60.32.Linux.60.0.26/lib/net40"

echo "restoring nuget packages"
nuget restore "Netlenium Framework.sln"

echo "running msbuild"
msbuild /p:Configuration=LinuxRelease "Netlenium Framework.sln"

#echo "removing win32 geckofx-core"
#rm bin/LinuxRelease/Geckofx-Core.dll
#echo "removing win32 geckofx-winforms"
#rm bin/LinuxRelease/Geckofx-Winforms.dll

#echo "adding linux geckofx-core 60.0.26 32bit (from nuget)"
#cp $gecko_linux/Geckofx-Core.dll bin/LinuxRelease/Geckofx-Core.dll
#cp $gecko_linux/Geckofx-Core.dll.mdb bin/LinuxRelease/Geckofx-Core.dll.mdb

#echo "adding linux geckofx-winforms 60.0.26 32bit (from nuget)"
#cp $gecko_linux/Geckofx-Winforms.dll bin/LinuxRelease/Geckofx-Winforms.dll
#cp $gecko_linux/Geckofx-Winforms.dll.mdb bin/LinuxRelease/geckofx-Winforms.dll.mdb

echo "done"
