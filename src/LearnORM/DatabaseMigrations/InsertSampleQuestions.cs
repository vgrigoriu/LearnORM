using System;
using FluentMigrator;

namespace DatabaseMigrations
{
    [Migration(201408241606, "Insert sample questions")]
    public class InsertSampleQuestions : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("Question").Row(new
            {
                Id = -1,
                Text = "Who watches the watchers?"
            });
            Insert.IntoTable("Answer").Row(new
            {
                Id = -1,
                QuestionId = -1,
                Text = "Nobody",
                IsCorrect = true
            });
            Insert.IntoTable("Answer").Row(new
            {
                Id = -2,
                QuestionId = -1,
                Text = "They watch themselves",
                IsCorrect = false
            });
            Insert.IntoTable("Answer").Row(new
            {
                Id = -3,
                QuestionId = -1,
                Text = "The big bad wolf",
                IsCorrect = false
            });
            Insert.IntoTable("Question").Row(new
            {
                Id = -2,
                Text = "How much wood would a woodchuck chuck?"
            });
            Insert.IntoTable("Answer").Row(new
            {
                Id = -4,
                QuestionId = -2,
                Text = "1lb",
                IsCorrect = true
            });
            Insert.IntoTable("Answer").Row(new
            {
                Id = -5,
                QuestionId = -2,
                Text = "All of it",
                IsCorrect = true
            });
            Insert.IntoTable("Answer").Row(new
            {
                Id = -6,
                QuestionId = -2,
                Text = "Only the Great Woodchuck in the Sky knows",
                IsCorrect = true
            });
        }

        public override void Down()
        {
            Delete.FromTable("Answer").AllRows();
            Delete.FromTable("Question").AllRows();
        }
    }
}
