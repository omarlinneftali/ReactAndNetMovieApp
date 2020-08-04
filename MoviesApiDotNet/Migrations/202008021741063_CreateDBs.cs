namespace MoviesApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDBs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Actors", "SexID", "dbo.Sexes");
            DropIndex("dbo.Actors", new[] { "SexID" });
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        Movie_ID = c.Int(nullable: false),
                        Actor_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_ID, t.Actor_ID })
                .ForeignKey("dbo.Movies", t => t.Movie_ID, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.Actor_ID, cascadeDelete: true)
                .Index(t => t.Movie_ID)
                .Index(t => t.Actor_ID);
            
            AddColumn("dbo.Actors", "Sex", c => c.Boolean(nullable: false));
            DropColumn("dbo.Actors", "SexID");
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
            
            AddColumn("dbo.Actors", "SexID", c => c.Int(nullable: false));
            DropForeignKey("dbo.MovieActors", "Actor_ID", "dbo.Actors");
            DropForeignKey("dbo.MovieActors", "Movie_ID", "dbo.Movies");
            DropIndex("dbo.MovieActors", new[] { "Actor_ID" });
            DropIndex("dbo.MovieActors", new[] { "Movie_ID" });
            DropColumn("dbo.Actors", "Sex");
            DropTable("dbo.MovieActors");
            CreateIndex("dbo.Actors", "SexID");
            AddForeignKey("dbo.Actors", "SexID", "dbo.Sexes", "ID", cascadeDelete: true);
        }
    }
}
