using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MusicManager.BusinessLogic.Services;
using MusicManager.DataBase.Entities;

namespace MusicManager.Controllers
{
	public class CreateController : Controller
	{
		private readonly IService _createService;

		public CreateController(IService createservice)
		{
			_createService = createservice;
		}

		public ActionResult Add()
		{
			return View("Add");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAlbum(Album model, HttpPostedFileBase coverfile)
		{
			if (model == null || !ModelState.IsValid) return Redirect("Error");
			if (coverfile != null)
			{
				string fileName = Path.GetFileName(coverfile.FileName);
				coverfile.SaveAs(Server.MapPath("~/Content/img/covers/" + fileName));
				model.AlbumCover = fileName;
			}
			await _createService.CreateThis(model);
			return RedirectToAction("ViewLibrary", "Library");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateArtist(Artist model)
		{
			if (model == null || !ModelState.IsValid) return Redirect("Error");
			await _createService.CreateThis(model);
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateGenre(Genre model)
		{
			if (model == null || !ModelState.IsValid) return Redirect("Error");
			await _createService.CreateThis(model);
			return RedirectToAction("Index", "Home");
		}

	}
}