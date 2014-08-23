using FluentMigrator;

namespace DatabaseMigrations
{
    [Migration(201408231208, "Create Book table")]
    public class CreateBookTable : Migration
    {
        public override void Up()
        {
            Create.Table("Book")
                .WithDescription("Represents a book")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_Book")
                .WithColumn("Title").AsString(255)
                .WithColumn("TitleForSorting").AsString(255)
                .WithColumn("OriginalTitle").AsString(255);
        }

        public override void Down()
        {
            Delete.Table("Book");
        }
    }
}
