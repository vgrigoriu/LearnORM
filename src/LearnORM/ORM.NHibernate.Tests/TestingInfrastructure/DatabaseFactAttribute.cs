using System;
using System.Collections.Generic;
using NHibernate;
using Xunit;
using Xunit.Sdk;

namespace ORM.NHibernate.Tests.TestingInfrastructure
{
    public class DatabaseFactAttribute : FactAttribute
    {
        private static readonly SessionFactoryBuilder SessionFactoryBuilder = new SessionFactoryBuilder();

        private readonly Lazy<ISessionFactory> mySqlSessionFactory = new Lazy<ISessionFactory>(
            () => SessionFactoryBuilder.BuildForMySql());
        private readonly Lazy<ISessionFactory> msSqlSessionFactory = new Lazy<ISessionFactory>(
            () => SessionFactoryBuilder.BuildForMsSql());

        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            yield return new DatabaseCommand(method, mySqlSessionFactory.Value, "MySQL");
            yield return new DatabaseCommand(method, msSqlSessionFactory.Value, "Microsoft SQL");
        }
    }
}
