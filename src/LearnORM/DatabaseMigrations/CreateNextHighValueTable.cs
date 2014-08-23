using FluentMigrator;

namespace DatabaseMigrations
{
    [Migration(201408231122)]
    public class CreateNextHighValueTable : Migration
    {
        public override void Up()
        {
            Create.Table("NextHighValues")
                .WithDescription("Holds the next values for the HiLo id generator")
                .WithColumn("NextHigh").AsInt32()
                .WithColumn("EntityName").AsString(255);
        }

        public override void Down()
        {
            Delete.Table("NextHighValues");
        }
    }
}
