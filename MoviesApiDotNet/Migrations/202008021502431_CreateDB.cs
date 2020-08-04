namespace MoviesApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        SexID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sexes", t => t.SexID, cascadeDelete: true)
                .Index(t => t.SexID);
            
            CreateTable(
                "dbo.Sexes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MovieGenres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        MovieGenreID = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MovieGenres", t => t.MovieGenreID, cascadeDelete: true)
                .Index(t => t.MovieGenreID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "MovieGenreID", "dbo.MovieGenres");
            DropForeignKey("dbo.Actors", "SexID", "dbo.Sexes");
            DropIndex("dbo.Movies", new[] { "MovieGenreID" });
            DropIndex("dbo.Actors", new[] { "SexID" });
            DropTable("dbo.Movies");
            DropTable("dbo.MovieGenres");
            DropTable("dbo.Sexes");
            DropTable("dbo.Actors");
        }
    }
}
