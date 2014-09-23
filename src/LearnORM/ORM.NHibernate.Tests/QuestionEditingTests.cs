using System;
using System.Linq;
using Entities.Quizes;
using NHibernate;
using NHibernate.Linq;
using ORM.NHibernate.Tests.TestingInfrastructure;
using Xunit;

namespace ORM.NHibernate.Tests
{
    public class QuestionEditingTests
    {
        [DatabaseFact]
        public void CanAddAndRemoveAnswersToQuestion(ISessionFactory sessionFactory)
        {
            var originalIncorrectAnswerText = "The queen " + Guid.NewGuid();
            var questionId = sessionFactory.CreateQuestion(
                "Who's the fairest of them all?",
                "Snow White",
                originalIncorrectAnswerText);

            // edit back the question
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var question = session.Get<Question>(questionId);
                
                // remove all incorrect answers
                var wrongAnswers = question.Answers.Where(a => !a.IsCorrect).ToList();
                wrongAnswers.ForEach(wa => question.Answers.Remove(wa));

                // add new incorrect answers
                question.Answers.Add(new Answer { Text = "Grumpy", IsCorrect = false });
                question.Answers.Add(new Answer { Text = "Dopey", IsCorrect = false });

                session.Update(question);

                transaction.Commit();
            }

            // verify the edits
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var question = session.Get<Question>(questionId);

                Assert.Equal(3, question.Answers.Count);

                // the incorrect answer was deleted
                var originalIncorrectAnswer = session.Query<Answer>()
                    .SingleOrDefault(a => a.Text == originalIncorrectAnswerText);

                Assert.Null(originalIncorrectAnswer);

                transaction.Commit();
            }
        }
    }
}
