using FluentMigrator;

namespace DatabaseMigrations
{
    [Migration(201508162252, "Create table SensorReading")]
    public class CreateTableSensorReading : Migration
    {
        public override void Up()
        {
            Create.Table("SensorReading").WithDescription("Represents a reading from a sensor")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_SensorReading")
                .WithColumn("SensorName").AsString()
                .WithColumn("Value").AsDouble()
                .WithColumn("Date").AsDateTimeOffset();

            Insert.IntoTable("NextHighValues").Row(new
            {
                NextHigh = 0,
                EntityName = "sensorreading"
            });
        }

        public override void Down()
        {
            Delete.Table("SensorReading");
        }
    }
}