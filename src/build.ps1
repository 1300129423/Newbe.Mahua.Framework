﻿Framework "4.6"
properties {
    $rootNow = Resolve-Path .
    $nugetexe = "$rootNow\buildTools\NuGet.exe"
    $deployMode = "Debug"
    $releaseDir = "$rootNow\build\"
}

Task Default -depends NugetPushLocal

Task Clean -Description "清理上一次编译结果" {
    Remove-Item $releaseDir -Force -Recurse -ErrorAction SilentlyContinue
}

Task Init -depends Clean -Description "初始化参数" {

}

Task Nuget -depends Init -Description "nuget restore" {
    dotnet restore Newbe.Mahua.sln
}

Task Build -depends Nuget -Description "编译所有解决方案" {
    Exec {
        msbuild /m /t:"Clean;Rebuild" /p:Configuration=$deployMode /v:minimal /nologo  Newbe.Mahua.sln
    }
}
Task Pack -depends Build -Description "打包" {
    dotnet pack Newbe.Mahua.Pack.sln -c $deployMode --no-build
}
Task NugetPushLocal -depends Pack -Description "推送nuget包到本地" {
    Write-Output "构建完毕，当前时间为 $(Get-Date)"
}

Task NugetPushNuget -depends Pack -Description "推送nuget包到nuget.org" {
    Get-ChildItem $releaseDir *.nupkg | ForEach-Object {
        cmd /c "$nugetexe push $releaseDir$_ -Source https://www.nuget.org/api/v2/package"
    }
    Write-Output "构建完毕，当前时间为 $(Get-Date)"
}

Task ReconfigDllExport -description "初始化DllExport" {
    Exec {
        .\DllExport.bat  -action Restore -sln-file Newbe.Mahua.sln
    }
}