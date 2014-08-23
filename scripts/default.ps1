$sln = Join-Path $PSScriptRoot ..\src\LearnORM\LearnORM.sln


task default -depends Test

task Test -depends UpdateDatabase {
    "run tests"
}

task UpdateDatabase -depends Compile {
    "Update database"
}

task Compile -depends Clean {
    msbuild $sln
}

task Clean {
    msbuild $sln /t:clean
}

task ? -Description "Helper to display task info" {
    "Write-Documentation"
}
