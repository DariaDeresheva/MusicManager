using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicManager.Controllers;
using MusicManager.Controllers.Injections;
using MusicManager.BusinessLogic.Services;
using MusicManager.Models;
using MusicManager.DataBase.Entities;

namespace MusicManager.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		[TestMethod]
		public void Index()
		{
			// Arrange
			var service = A.Fake<IService>();
			HomeController controller = new HomeController(service);

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}

		
	}
}
