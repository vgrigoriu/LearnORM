﻿using System;
using System.Linq;
using Entities;
using NHibernate;
using NHibernate.Linq;
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

    public class BookWithPublisherTests : DatabaseTests
    {
        [Fact]
        public void CanAddBookWithPublisherToMsSql()
        {
            var sessionFactory = SessionFactories.MsSqlSessionFactory;

            CanAddBookWithPublisher(sessionFactory);
        }

        [Fact]
        public void CanAddBookWithPublisherToMySql()
        {
            var sessionFactory = SessionFactories.MySqlSessionFactory;

            CanAddBookWithPublisher(sessionFactory);
        }

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
