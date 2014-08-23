using FluentMigrator;

namespace DatabaseMigrations
{
    [Migration(201408231347, "Insert next hi value for Book")]
    public class InsertBookNextHighValue : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("NextHighValues").Row(new
            {
                NextHigh = 0,
                EntityName = "book"
            });
        }

        public override void Down()
        {
            Delete.FromTable("NextHighValues").Row(new
            {
                EntityName = "book"
            });
        }
    }
}
