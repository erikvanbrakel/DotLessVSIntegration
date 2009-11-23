# This script was derived from the Rhino.Mocks buildscript written by Ayende Rahien.
include .\psake_ext.ps1

properties {
    $config = 'debug'
    $showtestresult = $FALSE
    $base_dir = resolve-path .
    $lib_dir = "$base_dir\lib"
    $build_dir = "$base_dir\build\" 
    $release_dir = "$base_dir\release\"
    $source_dir = "$base_dir\src"
	$version = Get-Git-Version
}

task default -depends Installer

task Clean {
    remove-item -force -recurse $build_dir -ErrorAction SilentlyContinue 
    remove-item -force -recurse $release_dir -ErrorAction SilentlyContinue 
}

task Installer -depends Merge {
    msbuild $source_dir\dotlessVS.WixSetup\dotlessVS.WixSetup.wixproj /p:OutDir=$build_dir/Installer/ /p:Configuration=$config
}

task Init -depends Clean {
    Write-Host $version
    Generate-Assembly-Info `
		-file "$source_dir\dotlessVS.Test\Properties\AssemblyInfo.cs" `
		-title "dotless Tests $version" `
		-description "Visual Studio integration for .Less" `
		-company "dotless project" `
		-product "dotless Visual Studio integration" `
		-version $version `
		-copyright "Copyright © dotless project 2009"
    Generate-Assembly-Info `
		-file "$source_dir\dotlessVS.Core\Properties\AssemblyInfo.cs" `
		-title "dotless Integration Package $version" `
		-description "Visual Studio integration for .Less" `
		-company "dotless project" `
		-product "dotless Visual Studio integration" `
		-version $version `
		-copyright "Copyright © dotless project 2009"
        
    new-item $build_dir -itemType directory
    new-item $release_dir -itemType directory
    
}

task Build -depends Init {
	msbuild $source_dir\dotlessVS.Core\DotLessIntegration.csproj /p:OutDir=$build_dir /p:Configuration=$config
}

task Test -depends Build {
    msbuild $source_dir\dotlessVS.Test\dotlessVS.Test.csproj /p:OutDir=$build_dir /p:Configuration=$config
    if ($lastExitCode -ne 0) {
        throw "Error: Test compile failed"
    }
    $old = pwd
    cd $build_dir
    & $lib_dir\NUnit\nunit-console-x86.exe $build_dir\dotlessVS.Test.dll 
    if ($lastExitCode -ne 0) {
        throw "Error: Failed to execute tests"
        if ($showtestresult)
        {
            start $build_dir\TestResult.xml
        }
    }
    cd $old
}

task Merge -depends Build {
    $old = pwd
    cd $build_dir
    $filename = "DotLessIntegration.dll"
    Remove-Item $filename-partial.dll -ErrorAction SilentlyContinue
    Rename-Item $filename $filename-partial.dll
    write-host "Executing ILMerge"
    & $lib_dir\ilmerge\ILMerge.exe $filename-partial.dll `
        PegBase.dll `
        Interop.tom.dll `
        /out:$filename `
        /internalize `
        /t:library
    if ($lastExitCode -ne 0) {
        throw "Error: Failed to merge assemblies"
    }
    cd $old
}

task Release-NoTest -depends Merge {
    $commit = Get-Git-Commit
    $filename = "dotless.core"
    & $lib_dir\7zip\7za.exe a $release_dir\dotless-$commit.zip `
    $build_dir\$filename.dll `
    $build_dir\$filename.pdb `
    $build_dir\dotless.compiler.exe `
	acknowledgements.txt `
    license.txt `
    #$build_dir\Testresult.xml `
    
    
    Write-Host -ForegroundColor Yellow "Please note that no tests where run during release process!"
    Write-host "-----------------------------"
    Write-Host "dotless $version was successfully compiled and packaged."
    Write-Host "The release bits can be found in ""$release_dir"""
    Write-Host -ForegroundColor Cyan "Thank you for using dotless!"
}

task Release -depends Test, Merge {
    $commit = Get-Git-Commit
    $filename = "dotless.core"
    & $lib_dir\7zip\7za.exe a $release_dir\dotless-$commit.zip `
    $build_dir\$filename.dll `
    $build_dir\$filename.pdb `
    $build_dir\Testresult.xml `
    $build_dir\dotless.compiler.exe `
    acknowledgements.txt `
    license.txt `
    
    
    Write-host "-----------------------------"
    Write-Host "dotless $version was successfully compiled and packaged."
    Write-Host "The release bits can be found in ""$release_dir"""
    Write-Host -ForegroundColor Cyan "Thank you for using dotless!"
}