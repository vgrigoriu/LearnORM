using FluentMigrator;

namespace DatabaseMigrations
{
    [Migration(201408231551, "Create table publisher")]
    public class CreatePublisherTable : Migration
    {
        public override void Up()
        {
            Create.Table("Publisher").WithDescription("Represents a book publisher")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_Publisher")
                .WithColumn("Name").AsString(255);

            Insert.IntoTable("NextHighValues").Row(new
            {
                NextHigh = 0,
                EntityName = "publisher"
            });

            Alter.Table("Book")
                .AddColumn("PublisherId").AsInt32().Nullable()
                .ForeignKey("FK_Book_Publisher", "Publisher", "Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Book_Publisher").OnTable("Book");

            Delete.FromTable("NextHighValues").Row(new
            {
                EntityName = "publisher"
            });

            Delete.Table("Publisher");
        }
    }
}
