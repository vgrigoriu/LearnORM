using Xunit;

namespace ORM.NHibernate.Tests
{
    public abstract class DatabaseTests : IUseFixture<DatabaseFixture>
    {
        protected DatabaseFixture SessionFactories;

        public void SetFixture(DatabaseFixture fixture)
        {
            SessionFactories = fixture;
        }
    }
}