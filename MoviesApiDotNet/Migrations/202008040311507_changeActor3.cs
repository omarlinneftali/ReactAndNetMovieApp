namespace MoviesApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeActor3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "SexId", c => c.Boolean(nullable: false));
            DropColumn("dbo.Actors", "Sex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Actors", "Sex", c => c.Boolean(nullable: false));
            DropColumn("dbo.Actors", "SexId");
        }
    }
}
