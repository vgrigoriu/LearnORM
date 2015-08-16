using System;
using System.Linq;
using Entities;
using NHibernate;
using NHibernate.Linq;
using ORM.NHibernate.Tests.TestingInfrastructure;
using Xunit;

namespace ORM.NHibernate.Tests
{
    public class BookTests
    {
        [DatabaseFact]
        public void CanSaveAndQueryBook(ISessionFactory sessionFactory)
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

    public class SensorReadingTests
    {
        [DatabaseFact]
        public void CanSaveAndQuerySensorReading(ISessionFactory sessionFactory)
        {
            string sensorName = "Precipitation_" + Guid.NewGuid();
            double value = new Random().NextDouble();
            DateTimeOffset now = DateTimeOffset.Now;
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var reading = new SensorReading()
                {
                    SensorName = sensorName,
                    Value = value,
                    //ReadingDate = now
                };

                session.Save(reading);

                transaction.Commit();
            }

            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var readings = session.Query<SensorReading>().Where(reading => reading.SensorName == sensorName).ToList();

                Assert.Equal(1, readings.Count);
                Assert.Equal(now, readings[0].ReadingDate);

                transaction.Commit();
            }
        }
    }
}
