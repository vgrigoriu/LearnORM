using Entities.Quizes;
using NHibernate;

namespace ORM.NHibernate.Tests
{
    static internal class SessionFactoryExtensions
    {
        public static int CreateQuestion(this ISessionFactory sessionFactory, string questionText, string correctAnswerText,
            string incorrectAnswerText)
        {
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
            return questionId;
        }
    }
}