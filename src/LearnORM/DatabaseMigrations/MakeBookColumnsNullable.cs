using FluentMigrator;

namespace DatabaseMigrations
{
    [Migration(201408231218, "Make Book columns nullable")]
    public class MakeBookColumnsNullable : Migration
    {
        public override void Up()
        {
            Alter.Column("TitleForSorting").OnTable("Book").InSchema("dbo")
                .AsString(255).Nullable();
            Alter.Column("OriginalTitle").OnTable("Book").InSchema("dbo")
                .AsString(255).Nullable();
        }

        public override void Down()
        {
            // do nothing, it's ok to leave the nullability as is
        }
    }
}
