task default -depends Test

task Test -depends UpdateDatabase {
    "run tests"
}

task UpdateDatabase -depends Compile {
    "Update database"
}

task Compile {
    "compile"
}

task ? -Description "Helper to display task info" {
    "Write-Documentation"
}
