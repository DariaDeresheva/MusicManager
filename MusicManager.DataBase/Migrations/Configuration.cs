namespace MusicManager.DataBase.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<MusicManager.DataBase.Context.MusicManagerDataBaseContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			ContextKey = "MusicManager.DataBase.Context.MusicManagerDataBaseContext";
		}

		protected override void Seed(MusicManager.DataBase.Context.MusicManagerDataBaseContext context)
		{

		}
	}
}
