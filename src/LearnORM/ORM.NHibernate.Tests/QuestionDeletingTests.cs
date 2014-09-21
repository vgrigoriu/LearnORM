using System;
using System.Linq;
using Entities.Quizes;
using NHibernate;
using NHibernate.Linq;
using ORM.NHibernate.Tests.TestingInfrastructure;
using Xunit;

namespace ORM.NHibernate.Tests
{
    public class QuestionDeletingTests
    {
        [DatabaseFact]
        public void CanDeleteQuestion(ISessionFactory sessionFactory)
        {
            var correctAnswerText = "I don't know " + Guid.NewGuid();
            var questionId = sessionFactory.CreateQuestion(
                "What is the meaning of life?",
                correctAnswerText,
                "Getting more toys");

            // delete
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var question = session.Get<Question>(questionId);
                session.Delete(question);

                transaction.Commit();
            }

            // was the question deleted?
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var question = session.Get<Question>(questionId);
                Assert.Null(question);

                transaction.Commit();
            }

            // were the answers deleted?
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var allAnswers = session.Query<Answer>().ToList();
                Assert.NotEmpty(allAnswers);

                var justDeletedAnswer = allAnswers.Where(a => a.Text == correctAnswerText);
                Assert.Empty(justDeletedAnswer);

                transaction.Commit();
            }
        }
    }
}
