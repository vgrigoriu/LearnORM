using System;
using System.Linq;
using Entities;
using NHibernate;
using NHibernate.Linq;
using ORM.NHibernate.Tests.TestingInfrastructure;
using Xunit;

namespace ORM.NHibernate.Tests
{
    public class BookWithPublisherTests : DatabaseTests
    {
        [DatabaseFact]
        private void CanAddBookWithPublisher(ISessionFactory sessionFactory)
        {
            string bookTitle = "Hild_" + Guid.NewGuid();
            const string publisherName = "Farrar, Straus and Giroux";

            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var publisher = session.Query<Publisher>()
                    .SingleOrDefault(pub => pub.Name == publisherName);

                if (publisher == null)
                {
                    publisher = new Publisher {Name = publisherName};
                    session.Save(publisher);
                }

                var book = new Book {Title = bookTitle, Publisher = publisher};
                session.Save(book);

                transaction.Commit();
            }

            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var books = session.Query<Book>().Where(book => book.Title == bookTitle).ToList();

                Assert.Equal(1, books.Count);
                Assert.Equal(publisherName, books.Single().Publisher.Name);

                transaction.Commit();
            }
        }
    }
}
