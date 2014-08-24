$psake = Join-Path $PSScriptRoot ..\packages\psake.4.3.2\tools\psake.ps1

$buildScript = Join-Path $PSScriptRoot default.ps1

& $psake $buildScript @args

