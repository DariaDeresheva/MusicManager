using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicManager.DataBase.Entities;

namespace MusicManager.Tests.Services
{
	[TestClass]
	public class CommonServiceTest
	{
		[TestMethod]
		public async Task GetGenre()
		{
			var service = new BusinessLogic.Services.CommonService();
			var genre = await service.GetOneWhere<Genre>(g => g.GenreName == "Jazz");
			Assert.AreEqual(4, genre.GenreId);
		}

	}
}
