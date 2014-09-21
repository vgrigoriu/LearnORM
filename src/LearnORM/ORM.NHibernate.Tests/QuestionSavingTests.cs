using System;
using System.Linq;
using Entities.Quizes;
using NHibernate;
using ORM.NHibernate.Tests.TestingInfrastructure;
using Xunit;

namespace ORM.NHibernate.Tests
{
    public class QuestionSavingTests
    {
        [DatabaseFact]
        public void CanSaveQuestions(ISessionFactory sessionFactory)
        {
            var questionText = "Can you touch this?_" + Guid.NewGuid();
            const string correctAnswerText = "No";
            const string incorrectAnswerText = "Yes";

            int questionId = sessionFactory.CreateQuestion(
                questionText,
                correctAnswerText,
                incorrectAnswerText);

            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var question = session.Get<Question>(questionId);

                var correctAnswer = question.Answers.Single(a => a.IsCorrect);
                var incorrectAnswer = question.Answers.Single(a => !a.IsCorrect);

                Assert.Equal(correctAnswerText, correctAnswer.Text);
                Assert.Equal(incorrectAnswerText, incorrectAnswer.Text);

                transaction.Commit();
            }
        }
    }
}
