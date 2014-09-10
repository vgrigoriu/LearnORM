using System.Linq;
using Entities.Quizes;
using NHibernate;
using NHibernate.Linq;
using Xunit;

namespace ORM.NHibernate.Tests
{
    public class QuestionQueryTests : DatabaseTests
    {
        [Fact]
        public void CanQueryQuestionsFromMsSql()
        {
            var sessionFactory = SessionFactories.MsSqlSessionFactory;
            CanQueryQuestions(sessionFactory);
        }

        [Fact]
        public void CanQueryQuestionsFromMySql()
        {
            var sessionFactory = SessionFactories.MySqlSessionFactory;
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
