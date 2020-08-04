namespace MoviesApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeActor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Actors", "FullName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Actors", "FullName", c => c.String(nullable: false));
            DropColumn("dbo.Actors", "Name");
        }
    }
}
