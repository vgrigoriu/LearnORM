﻿using System.Linq;
using Entities.Quizes;
using NHibernate;
using NHibernate.Linq;
using Xunit;

namespace ORM.NHibernate.Tests
{
    public class QuestionQueryTests
    {
        [Fact]
        public void CanQueryQuestionsFromMsSql()
        {
            var sessionFactoryBuilder = new SessionFactoryBuilder();
            var sessionFactory = sessionFactoryBuilder.BuildForMsSql();

            CanQueryQuestions(sessionFactory);
        }

        [Fact]
        public void CanQueryQuestionsFromMySql()
        {
            var sessionFactoryBuilder = new SessionFactoryBuilder();
            var sessionFactory = sessionFactoryBuilder.BuildForMySql();

            CanQueryQuestions(sessionFactory);
        }

        private void CanQueryQuestions(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var questions = from q in session.Query<Question>().FetchMany(q => q.Answers)
                                where q.Answers.Any(a => a.IsCorrect)
                                select q;

                Assert.True(questions.First().Answers.Count > 0);

                transaction.Commit();
            }
        }
    }
}