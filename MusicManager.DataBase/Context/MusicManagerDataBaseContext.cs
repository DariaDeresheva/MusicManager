using System.Data.Entity;
using System.Reflection;
using MusicManager.DataBase.Entities;

namespace MusicManager.DataBase.Context
{
	public class MusicManagerDataBaseContext : DbContext
	{
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Artist> Artists { get; set; }
		public DbSet<Album> Albums { get; set; }
		public DbSet<ArtistList> ArtistLists { get; set; }
		public DbSet<Song> Songs { get; set; }

		//Configuration of creating database model
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Album>()
				.HasMany(a => a.Songs)
				.WithRequired(s => s.Album)
				.WillCascadeOnDelete(true);

			modelBuilder.Entity<Song>()
				.HasMany(al => al.Artists)
				.WithRequired(s => s.Song)
				.WillCascadeOnDelete(true);
		}

	}
}