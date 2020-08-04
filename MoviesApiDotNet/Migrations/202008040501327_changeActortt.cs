﻿namespace MoviesApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeActortt : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Sexes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sexes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
