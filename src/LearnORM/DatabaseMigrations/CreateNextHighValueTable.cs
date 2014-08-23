﻿using FluentMigrator;

namespace DatabaseMigrations
{
    [Migration(201408231122)]
    public class CreateNextHighValueTable : Migration
    {
        public override void Up()
        {
            Create.Table("NextHighVaues")
                .WithDescription("Holds the next values for the HiLo id generator")
                .InSchema("dbo")
                .WithColumn("NextHigh").AsInt32()
                .WithColumn("EntityName").AsString(255);
        }

        public override void Down()
        {
            Delete.Table("NextHighVaues").InSchema("dbo");
        }
    }
}