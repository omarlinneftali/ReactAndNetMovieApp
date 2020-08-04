namespace MoviesApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMovieGenref : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Movies", new[] { "MovieGenreID" });
            CreateIndex("dbo.Movies", "MovieGenreId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Movies", new[] { "MovieGenreId" });
            CreateIndex("dbo.Movies", "MovieGenreID");
        }
    }
}
