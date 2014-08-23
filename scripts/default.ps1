$sln = Join-Path $PSScriptRoot ..\src\LearnORM\LearnORM.sln
$migrate = Join-Path $PSScriptRoot ..\src\LearnORM\packages\FluentMigrator.1.2.1.0\tools\Migrate.exe
$assembly = Join-Path $PSScriptRoot ..\src\LearnORM\DatabaseMigrations\bin\Debug\DatabaseMigrations.dll

task default -depends Test

task Test -depends UpdateDatabase {
	$xunit = Join-Path $PSScriptRoot ..\src\LearnORM\packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe
	$tests = Join-Path $PSScriptRoot ..\src\LearnORM\ORM.NHibernate.Tests\bin\Debug\ORM.NHibernate.Tests.dll
    exec {
		& $xunit $tests
	}
}

task UpdateDatabase -depends UpdateMSSQLDatabase, UpdateMySQLDatabase;

task UpdateMSSQLDatabase -depends Compile {
	exec {
		& $migrate --target $assembly --dbType SqlServer --connection "Server=(localdb)\v11.0;Integrated Security=true;Database=LearnORM"
	}
}

task UpdateMySQLDatabase -depends Compile {
	exec {
		& $migrate --target $assembly --dbType MySql --connection "Server=localhost;Database=LearnORM;Uid=vgrigoriu;Pwd=12345;"
	}
}

task Compile -depends Clean {
	exec {
		msbuild $sln
	}
}

task Clean {
	exec {
		msbuild $sln /t:clean
	}
}

task RevertDatabase -depends RevertMSSQLDatabase, RevertMySQLDatabase;

task RevertMSSQLDatabase -depends Compile {
	exec {
		& $migrate --target $assembly --task rollback:all --dbType SqlServer --connection "Server=(localdb)\v11.0;Integrated Security=true;Database=LearnORM"
	}
}

task RevertMySQLDatabase -depends Compile {
	exec {
		& $migrate --target $assembly --task rollback:all --dbType MySql --connection "Server=localhost;Database=LearnORM;Uid=vgrigoriu;Pwd=12345;"
	}
}

task ? -Description "Helper to display task info" {
    "Write-Documentation"
}
