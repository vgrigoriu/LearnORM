﻿using System;
using System.Linq;
using Entities;
using NHibernate;
using NHibernate.Linq;
using Xunit;

namespace ORM.NHibernate.Tests
{
    public class BookTests
    {
        [Fact]
        public void CanSaveAndQueryBookFromMsSql()
        {
            var sessionFactoryBuilder = new SessionFactoryBuilder();
            var sessionFactory = sessionFactoryBuilder.BuildForMsSql();

            CanSaveAndQueryBook(sessionFactory);
        }

        [Fact]
        public void CanSaveAndQueryBookFromMySql()
        {
            var sessionFactoryBuilder = new SessionFactoryBuilder();
            var sessionFactory = sessionFactoryBuilder.BuildForMySql();

            CanSaveAndQueryBook(sessionFactory);
        }

        private static void CanSaveAndQueryBook(ISessionFactory sessionFactory)
        {
            string title = "Dumbrava minunată" + Guid.NewGuid();
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var book = new Book {Title = title};

                session.Save(book);

                transaction.Commit();
            }

            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var books = session.Query<Book>().Where(book => book.Title == title).ToList();

                Assert.Equal(1, books.Count);

                transaction.Commit();
            }
        }
    }
}
