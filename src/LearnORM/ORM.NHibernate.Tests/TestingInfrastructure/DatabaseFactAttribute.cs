using System.Collections.Generic;
using Xunit;
using Xunit.Sdk;

namespace ORM.NHibernate.Tests.TestingInfrastructure
{
    public class DatabaseFactAttribute : FactAttribute
    {
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            yield return new DatabaseCommand(method, new SessionFactoryBuilder().BuildForMySql(), "MySQL");
            yield return new DatabaseCommand(method, new SessionFactoryBuilder().BuildForMsSql(), "Microsoft SQL");
        }
    }
}
