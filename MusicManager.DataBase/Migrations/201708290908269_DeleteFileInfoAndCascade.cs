namespace MusicManager.DataBase.Migrations
{
	using System.Data.Entity.Migrations;
	
	public partial class DeleteFileInfoAndCascade : DbMigration
	{
		public override void Up()
		{
			DropForeignKey("dbo.Songs", "FileInfo_FileInfoId", "dbo.FileInfoes");
			DropForeignKey("dbo.Songs", "Album_AlbumId", "dbo.Albums");
			DropForeignKey("dbo.ArtistLists", "Song_SongId", "dbo.Songs");
			DropIndex("dbo.Songs", new[] { "Album_AlbumId" });
			DropIndex("dbo.Songs", new[] { "FileInfo_FileInfoId" });
			DropIndex("dbo.ArtistLists", new[] { "Song_SongId" });
			AlterColumn("dbo.Songs", "Album_AlbumId", c => c.Int(nullable: false));
			AlterColumn("dbo.ArtistLists", "Song_SongId", c => c.Int(nullable: false));
			CreateIndex("dbo.Songs", "Album_AlbumId");
			CreateIndex("dbo.ArtistLists", "Song_SongId");
			AddForeignKey("dbo.Songs", "Album_AlbumId", "dbo.Albums", "AlbumId", cascadeDelete: true);
			AddForeignKey("dbo.ArtistLists", "Song_SongId", "dbo.Songs", "SongId", cascadeDelete: true);
			DropColumn("dbo.Songs", "FileInfo_FileInfoId");
			DropTable("dbo.FileInfoes");
		}
		
		public override void Down()
		{
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
			
			AddColumn("dbo.Songs", "FileInfo_FileInfoId", c => c.Int());
			DropForeignKey("dbo.ArtistLists", "Song_SongId", "dbo.Songs");
			DropForeignKey("dbo.Songs", "Album_AlbumId", "dbo.Albums");
			DropIndex("dbo.ArtistLists", new[] { "Song_SongId" });
			DropIndex("dbo.Songs", new[] { "Album_AlbumId" });
			AlterColumn("dbo.ArtistLists", "Song_SongId", c => c.Int());
			AlterColumn("dbo.Songs", "Album_AlbumId", c => c.Int());
			CreateIndex("dbo.ArtistLists", "Song_SongId");
			CreateIndex("dbo.Songs", "FileInfo_FileInfoId");
			CreateIndex("dbo.Songs", "Album_AlbumId");
			AddForeignKey("dbo.ArtistLists", "Song_SongId", "dbo.Songs", "SongId");
			AddForeignKey("dbo.Songs", "Album_AlbumId", "dbo.Albums", "AlbumId");
			AddForeignKey("dbo.Songs", "FileInfo_FileInfoId", "dbo.FileInfoes", "FileInfoId");
		}
	}
}
