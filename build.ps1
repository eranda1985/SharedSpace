$ProjectDir='.'
$SolutionDir='.'
$msBuild="C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe"
$Nuget="$ProjectDir\nuget.exe"

$CoreProj="$ProjectDir\SharedSpace\SharedSpace.csproj"
$DroidLibvProj="$ProjectDir\SharedSpace.Droid.Lib\SharedSpace.Droid.Lib.csproj"
$DroidLibvProjPath="$ProjectDir\SharedSpace.Droid.Lib\"
$IoSLibProj="$ProjectDir\SharedSpace.iOS.Lib\SharedSpace.iOS.Lib.csproj"
$IoSLibProjPath="$ProjectDir\SharedSpace.iOS.Lib\"

Write-Host "Cleaning project path $CoreProj..." -foregroundcolor green
& dotnet msbuild $CoreProj /t:Clean /m

& cd $DroidLibvProjPath
Write-Host "Cleaning project path $DroidLibvProj..." -foregroundcolor green
& $msBuild /t:Clean /m /p:Configuration=Release
& cd ..

Write-Host "Cleaning project path $IoSLibProj..." -foregroundcolor green
& cd $IoSLibProjPath
& $msBuild /t:Clean /m /p:Configuration=Release
& cd ..

$OutputDir = "$ProjectDir\Build\"
$items = (Get-ChildItem $OutputDir -Recurse)

ForEach ($i in $items) {
	Remove-Item $i.FullName -Force -Recurse -ErrorAction SilentlyContinue
	Write-Host "Deleted " $i.FullName
}

& cd $DroidLibvProjPath
& $msBuild /t:Restore 
& $msBuild /t:Build /p:SolutionDir=..\ /p:Configuration=Release
& cd ..
& cd $IoSLibProjPath
& $msBuild /t:Restore
& $msBuild /t:Build /p:SolutionDir=..\ /p:Configuration=Release 
& cd ..

# Packaging with Nuget.
& $Nuget pack Package.nuspec -OutputDirectory "$ProjectDir\Build\nuget\"

Write-Host "Done..." -foregroundcolor cyan