using FluentMigrator;

namespace DatabaseMigrations
{
    [Migration(201408241601, "Create table Answer")]
    public class CreateTableAnswer : Migration
    {
        public override void Up()
        {
            Create.Table("Answer").WithDescription("Represents a quiz answer")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_Answer")
                .WithColumn("Text").AsString()
                .WithColumn("IsCorrect").AsBoolean()
                .WithColumn("QuestionId").AsInt32().ForeignKey("FK_Answer_Question", "Question", "Id");

            Insert.IntoTable("NextHighValues").Row(new
            {
                NextHigh = 0,
                EntityName = "answer"
            });
        }

        public override void Down()
        {
            Delete.FromTable("NextHighValues").Row(new
            {
                EntityName = "answer"
            });

            Delete.Table("Answer");
        }
    }
}
