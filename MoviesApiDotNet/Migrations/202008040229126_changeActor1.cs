namespace MoviesApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeActor1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actors", "BirthDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Actors", "BirthDate", c => c.DateTime(nullable: false));
        }
    }
}
