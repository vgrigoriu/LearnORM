using System;
using System.Linq;
using Entities.Quizes;
using NHibernate;
using Xunit;

namespace ORM.NHibernate.Tests
{
    public class QuestionSavingTests : DatabaseTests
    {
        [Fact]
        public void CanSaveQuestionsToMsSql()
        {
            var sessionFactory = SessionFactories.MsSqlSessionFactory;
            CanSaveQuestions(sessionFactory);
        }

        [Fact]
        public void CanSaveQuestionsToMySql()
        {
            var sessionFactory = SessionFactories.MySqlSessionFactory;
            CanSaveQuestions(sessionFactory);
        }

        private void CanSaveQuestions(ISessionFactory sessionFactory)
        {
            var questionText = "Can you touch this?_" + Guid.NewGuid();
            const string correctAnswerText = "No";
            const string incorrectAnswerText = "Yes";

            int questionId;

            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var question = new Question
                {
                    Text = questionText,
                    Answers = new[]
                    {
                        new Answer
                        {
                            Text = correctAnswerText,
                            IsCorrect = true
                        },
                        new Answer
                        {
                            Text = incorrectAnswerText,
                            IsCorrect = false
                        }
                    }
                };

                session.Save(question);

                transaction.Commit();

                questionId = question.Id;
            }

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
