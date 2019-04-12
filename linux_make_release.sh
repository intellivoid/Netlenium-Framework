echo "restoring nuget packages"
nuget restore "Netlenium Framework.sln"

echo "deleting old build"
rm -rf "bin/LinuxRelease/"

echo "running msbuild"
msbuild /p:Configuration=LinuxRelease /v:d "Netlenium Framework.sln"

echo "changing current directory"
cd "bin/LinuxRelease/"

echo "running mkbundle on Netlenium Runtime"
mkbundle --simple --static --deps -v -o netlenium_re --config /etc/mono/config --machine-config /etc/mono/4.5/machine.config netlenium_re.exe

echo "running mkbundle on Netlenium Package Builder"
mkbundle --simple --static --deps -v -o npbuild --config /etc/mono/config --machine-config /etc/mono/4.5/machine.config npbuild.exe

echo "running mkbundle on Netlenium Server"
mkbundle --simple --static --deps -v -o npbuild --config /etc/mono/config --machine-config /etc/mono/4.5/machine.config netlenium_server.exe

echo "deleting win32 compiled binaries"
rm netlenium_re.exe
rm npbuild.exe
rm netlenium_server.exe

echo "done"
