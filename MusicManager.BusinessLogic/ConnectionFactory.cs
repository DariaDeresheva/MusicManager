using MusicManager.DataBase.Context;
using System.Data.Entity;


namespace MusicManager.BusinessLogic
{
	internal static class ConnectionFactory
	{
		public static DbContext GetConnection() => new MusicManagerDataBaseContext();
	}
}
