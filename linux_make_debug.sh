#gecko_linux="packages/Geckofx60.32.Linux.60.0.26/lib/net40"

echo "restoring nuget packages"
nuget restore "Netlenium Framework.sln"

echo "running msbuild"
msbuild /p:Configuration=LinuxDebug "Netlenium Framework.sln"

echo "changing current directory"
cd "bin/LinuxDebug/"

echo "running mkbundle on Netlenium Runtime"
mkbundle -o netlenium_re --simple netlenium_re.exe --config ../../mkbundle.config

echo "running mkbundle on Netlenium Package Builder"
mkbundle -o npbuild --simple npbuild.exe --config ../../mkbundle.config

echo "deleting win32 compiled binaries"
rm netlenium_re.exe
rm npbuild.exe

echo "done"
