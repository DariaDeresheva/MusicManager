namespace MusicManager.DataBase.Migrations
{
	using System.Data.Entity.Migrations;
	
	public partial class InitialCreate : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Albums",
				c => new
					{
						AlbumId = c.Int(nullable: false, identity: true),
						AlbumName = c.String(maxLength: 50),
						AlbumRating = c.Short(nullable: false),
						AlbumYear = c.Short(nullable: false),
						AlbumCover = c.String(),
					})
				.PrimaryKey(t => t.AlbumId);
			
			CreateTable(
				"dbo.Songs",
				c => new
					{
						SongId = c.Int(nullable: false, identity: true),
						SongName = c.String(maxLength: 50),
						SongRating = c.Short(nullable: false),
						SongNumber = c.Short(nullable: false),
						Album_AlbumId = c.Int(),
						FileInfo_FileInfoId = c.Int(),
						Genre_GenreId = c.Int(),
					})
				.PrimaryKey(t => t.SongId)
				.ForeignKey("dbo.Albums", t => t.Album_AlbumId)
				.ForeignKey("dbo.FileInfoes", t => t.FileInfo_FileInfoId)
				.ForeignKey("dbo.Genres", t => t.Genre_GenreId)
				.Index(t => t.Album_AlbumId)
				.Index(t => t.FileInfo_FileInfoId)
				.Index(t => t.Genre_GenreId);
			
			CreateTable(
				"dbo.ArtistLists",
				c => new
					{
						ArtistListId = c.Int(nullable: false, identity: true),
						Artist_ArtistId = c.Int(),
						Song_SongId = c.Int(),
					})
				.PrimaryKey(t => t.ArtistListId)
				.ForeignKey("dbo.Artists", t => t.Artist_ArtistId)
				.ForeignKey("dbo.Songs", t => t.Song_SongId)
				.Index(t => t.Artist_ArtistId)
				.Index(t => t.Song_SongId);
			
			CreateTable(
				"dbo.Artists",
				c => new
					{
						ArtistId = c.Int(nullable: false, identity: true),
						ArtistName = c.String(maxLength: 30),
					})
				.PrimaryKey(t => t.ArtistId);
			
			CreateTable(
				"dbo.FileInfoes",
				c => new
					{
						FileInfoId = c.Int(nullable: false, identity: true),
						FileName = c.String(maxLength: 50),
						FilePath = c.String(maxLength: 100),
						FileSize = c.Int(nullable: false),
						FileBitRate = c.Short(nullable: false),
					})
				.PrimaryKey(t => t.FileInfoId);
			
			CreateTable(
				"dbo.Genres",
				c => new
					{
						GenreId = c.Int(nullable: false, identity: true),
						GenreName = c.String(maxLength: 30),
					})
				.PrimaryKey(t => t.GenreId);
			
		}
		
		public override void Down()
		{
			DropForeignKey("dbo.Songs", "Genre_GenreId", "dbo.Genres");
			DropForeignKey("dbo.Songs", "FileInfo_FileInfoId", "dbo.FileInfoes");
			DropForeignKey("dbo.ArtistLists", "Song_SongId", "dbo.Songs");
			DropForeignKey("dbo.ArtistLists", "Artist_ArtistId", "dbo.Artists");
			DropForeignKey("dbo.Songs", "Album_AlbumId", "dbo.Albums");
			DropIndex("dbo.ArtistLists", new[] { "Song_SongId" });
			DropIndex("dbo.ArtistLists", new[] { "Artist_ArtistId" });
			DropIndex("dbo.Songs", new[] { "Genre_GenreId" });
			DropIndex("dbo.Songs", new[] { "FileInfo_FileInfoId" });
			DropIndex("dbo.Songs", new[] { "Album_AlbumId" });
			DropTable("dbo.Genres");
			DropTable("dbo.FileInfoes");
			DropTable("dbo.Artists");
			DropTable("dbo.ArtistLists");
			DropTable("dbo.Songs");
			DropTable("dbo.Albums");
		}
	}
}
