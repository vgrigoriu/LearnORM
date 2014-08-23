$sln = Join-Path $PSScriptRoot ..\src\LearnORM\LearnORM.sln

task default -depends Test

task Test -depends UpdateDatabase {
	$xunit = Join-Path $PSScriptRoot ..\src\LearnORM\packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe
	$tests = Join-Path $PSScriptRoot ..\src\LearnORM\ORM.NHibernate.Tests\bin\Debug\ORM.NHibernate.Tests.dll
    exec {
		& $xunit $tests
	}
}

task UpdateDatabase -depends Compile {
    $migrate = Join-Path $PSScriptRoot ..\src\LearnORM\packages\FluentMigrator.1.2.1.0\tools\Migrate.exe
	$assembly = Join-Path $PSScriptRoot ..\src\LearnORM\DatabaseMigrations\bin\Debug\DatabaseMigrations.dll
	exec {
		& $migrate --target $assembly --dbType SqlServer --connection "Server=(localdb)\v11.0;Integrated Security=true;Database=LearnORM"
	}
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
