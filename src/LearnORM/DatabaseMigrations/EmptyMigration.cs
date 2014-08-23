using FluentMigrator;

namespace DatabaseMigrations
{
    [Migration(201408231121, "Empty migration")]
    public class EmptyMigration : Migration
    {
        public override void Up()
        {
            // Empty migration, to be able to revert to an empty DB
        }

        public override void Down()
        {
            // Empty migration, to be able to revert to an empty DB
        }
    }
}
