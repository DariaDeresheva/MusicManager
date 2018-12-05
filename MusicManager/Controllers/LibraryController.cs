using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MusicManager.BusinessLogic.Services;
using MusicManager.Models;
using MusicManager.DataBase.Entities;


namespace MusicManager.Controllers
{
	public class LibraryController : Controller
	{
		private readonly IService _commonService;
		public LibraryController(IService service)
		{
			_commonService = service;
		}

		public async Task<ActionResult> ViewLibrary()
		{
			return View("Library", await _commonService.GetAll<Album>());
		}

		public async Task<ActionResult> ViewAlbum(int? id)
		{
			var album = await _commonService.GetAlbumWhere(newalbum => newalbum.AlbumId == id);

			if (album != null)
			{
				return View("Album", new AlbumData(album));
			}
			return HttpNotFound();
		}


		[HttpGet]
		public async Task<ActionResult> JsonAlbumDetails(int? albumId)
		{
			var album = await _commonService.GetAlbumWhere(newalbum => newalbum.AlbumId == albumId);

			if (album != null)
			{
				var albumData = new AlbumData(album);
				string result="<strong>Artists: </strong>";
				foreach (var artist in albumData.Artists)
				{
					result = result + artist.ArtistName;
					if (artist != albumData.Artists.Last()) result = result + ", ";
				}
				result = result + "<br /> <strong>Number of songs: </strong>" + album.Songs.Count;
				var response = new JsonResponseObject("ok", result);



				return Json(response, JsonRequestBehavior.AllowGet);
			}
			return null;
		}

		[HttpGet]
		public async Task<ActionResult> EditFullView(int id)
		{
			ViewBag.Genres = await _commonService.GetAll<Genre>();
			ViewBag.Artists = await _commonService.GetAll<Artist>();
			var album = await _commonService.GetAlbumWhere(newalbum => newalbum.AlbumId == id);
			if (album != null)
				return View("EditFullView", new FullAlbum(album));
			return HttpNotFound();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditResult(FullAlbum model, HttpPostedFileBase coverfile)
		{
			if (model != null && ModelState.IsValid)
			{
				if (coverfile != null)
				{
					string fileName = System.IO.Path.GetFileName(coverfile.FileName);
					coverfile.SaveAs(Server.MapPath("~/Content/img/covers/" + fileName));
					model.Album.AlbumCover = fileName;
				}

				await _commonService.UpdateThis(model.Album);
				return RedirectToAction("ViewAlbum", new { id = model.Album.AlbumId });
			}
			return HttpNotFound();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateSong(FullAlbum model, List<int> artists)
		{
			if (model == null || !ModelState.IsValid) return Redirect("Error");
			model.NewSong.Album = model.Album;
			model.NewSong.SongId = await _commonService.CreateSong(model.NewSong);
			foreach (var artist in artists)
			{
				var artlist = new ArtistList
				{
					Artist = new Artist { ArtistId = artist },
					Song = model.NewSong
				};
				await _commonService.CreateArtistList(artlist);
			}
			return RedirectToAction("ViewAlbum", new { id = model.Album.AlbumId });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteAlbum(int id)
		{
			var model = await _commonService.GetAlbumWhere(newalbum => newalbum.AlbumId == id);
			if (model != null && ModelState.IsValid)
			{
				await _commonService.DeleteAlbum(model);
				return RedirectToAction("ViewLibrary");
			}
			return View("Error");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteSong(int songid, int albumid)
		{
			var model = await _commonService.GetSongWhere(song => song.SongId == songid);
			if (model != null && ModelState.IsValid)
			{
				await _commonService.DeleteSong(model);
				return RedirectToAction("ViewAlbum", new { id = albumid });
			}
			return View("Error");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditSong(Song model, List<string> selectedartists)
		{
			if (model != null && ModelState.IsValid)
			{
				List<int> sartists = selectedartists.Select(a => Convert.ToInt32(a)).ToList();
				var oldmodel = await _commonService.GetSongWhere(s => s.SongId == model.SongId);
				foreach (var artlist in oldmodel.Artists.ToList())
				{
					if (!sartists.Contains(artlist.Artist.ArtistId)) await _commonService.DeleteThis(artlist);
					else sartists.Remove(artlist.Artist.ArtistId);
				}
				foreach (var i in sartists)
				{
					await _commonService.CreateArtistList(new ArtistList { Artist = new Artist { ArtistId = i }, Song = model });
				}
				await _commonService.UpdateSong(model);
				return RedirectToAction("ViewAlbum", new { id = model.Album.AlbumId });
			}
			return View("Error");
		}

		[HttpGet]
		public async Task<ActionResult> EditSongView(int id)
		{
			ViewBag.Genres = await _commonService.GetAll<Genre>();
			var allartists = await _commonService.GetAll<Artist>();
			var sosong = await _commonService.GetSongWhere(song => song.SongId == id);
			List<SelectListItem> selart = new List<SelectListItem>();
			foreach (var artist in allartists)
			{
				var temp = new SelectListItem { Text = artist.ArtistName, Value = artist.ArtistId.ToString() };
				foreach (var artlist in sosong.Artists)
				{
					if (artist.ArtistId == artlist.Artist.ArtistId)
						temp.Selected = true;

				}
				selart.Add(temp);
			}
			ViewBag.selectedartists = selart;
			if (sosong != null)
				return View("EditSong", sosong);
			return HttpNotFound();
		}
	}
	public class JsonResponseObject
	{
		public string status;
		public string message;
		public string ErrorType;

		public JsonResponseObject(string Status, string Message)
		{
			status = Status;
			message = Message;
			ErrorType = "0";
		}
	}
}