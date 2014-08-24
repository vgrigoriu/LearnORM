using FluentMigrator;

namespace DatabaseMigrations
{
    [Migration(201408241704, "Make Book.QuestionId nullable")]
    public class MakeQuestionIdNullable : Migration
    {
        public override void Up()
        {
            Alter.Column("QuestionId").OnTable("Answer")
                .AsInt32().Nullable();
        }

        public override void Down()
        {
            // do nothing, it's ok to leave the nullability as is
        }
    }
}
