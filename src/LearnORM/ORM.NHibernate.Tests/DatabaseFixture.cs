using System;
using NHibernate;

namespace ORM.NHibernate.Tests
{
    public class DatabaseFixture
    {
        private readonly Lazy<ISessionFactory> mySqlSessionFactory = new Lazy<ISessionFactory>(
            () => new SessionFactoryBuilder().BuildForMySql());
        private readonly Lazy<ISessionFactory> msSqlSessionFactory = new Lazy<ISessionFactory>(
            () => new SessionFactoryBuilder().BuildForMsSql());

        public ISessionFactory MySqlSessionFactory
        {
            get
            {
                return mySqlSessionFactory.Value;
            }
        }

        public ISessionFactory MsSqlSessionFactory
        {
            get
            {
                return msSqlSessionFactory.Value;
            }
        }
    }
}