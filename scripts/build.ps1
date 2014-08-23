$psake = Join-Path $PSScriptRoot ..\src\LearnORM\packages\psake.4.3.2\tools\psake.ps1

$buildScript = Join-Path $PSScriptRoot default.ps1

& $psake $buildScript @args

