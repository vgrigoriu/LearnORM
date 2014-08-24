using FluentMigrator;

namespace DatabaseMigrations
{
    [Migration(201408241558, "Create table Question")]
    public class CreateTableQuestion : Migration
    {
        public override void Up()
        {
            Create.Table("Question").WithDescription("Represents a quiz question")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_Question")
                .WithColumn("Text").AsString();

            Insert.IntoTable("NextHighValues").Row(new
            {
                NextHigh = 0,
                EntityName = "question"
            });
        }

        public override void Down()
        {
            Delete.FromTable("NextHighValues").Row(new
            {
                EntityName = "question"
            });

            Delete.Table("Question");
        }
    }
}
