using Xunit;

namespace ORM.NHibernate.Tests
{
    public class TrivialTests
    {
        [Fact]
        public void SanityCheck()
        {
            Assert.Equal(2, 1 + 1);
        } 
    }
}
