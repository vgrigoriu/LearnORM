using System;
using NHibernate;
using Xunit.Sdk;

namespace ORM.NHibernate.Tests.TestingInfrastructure
{
    public class DatabaseCommand : TestCommand
    {
        private readonly ISessionFactory sessionFactory;
        private readonly string databaseName;

        public DatabaseCommand(IMethodInfo method, ISessionFactory sessionFactory, string databaseName)
            : base(method, MethodUtility.GetDisplayName(method), MethodUtility.GetTimeoutParameter(method))
        {
            this.sessionFactory = sessionFactory;
            this.databaseName = databaseName;
        }

        public override MethodResult Execute(object testClass)
        {
            try
            {
                this.testMethod.Invoke(testClass, new object[] {sessionFactory});
            }
            catch (ParameterCountMismatchException)
            {
                throw new InvalidOperationException("Fact method " + this.TypeName + "." + this.MethodName + " cannot have parameters other than ISessionFactory");
            }
            return new PassedResult(this.testMethod, this.DisplayName + " (" + databaseName + ")");
        }
    }
}
