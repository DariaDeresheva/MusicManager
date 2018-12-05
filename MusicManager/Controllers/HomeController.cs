using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MusicManager.BusinessLogic.Services;
using MusicManager.DataBase.Entities;
using MusicManager.Models;

namespace MusicManager.Controllers
{
	public class HomeController : Controller
	{
		private readonly IService _getService;

		public HomeController(IService service)
		{
			_getService = service;
		}
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			


			return View();
		}

		public async Task<ActionResult> Search()
		{
			ViewBag.Genres = await _getService.GetAll<Genre>();
			return View("Search");
		}

		[HttpPost]
		public async Task<ActionResult> SearchResult(SearchData data)
		{
			
			var allalbums = await _getService.GetAll<Album>();
			if (data.Rating != 0) allalbums = allalbums.Where(a => a.AlbumRating == data.Rating).ToList();
			if (data.Year != 0) allalbums = allalbums.Where(a => a.AlbumYear == data.Year).ToList();
			foreach (var album in allalbums.ToList())
			{
				int nameFlag = 0;
				var albumData = new AlbumData(await _getService.GetAlbumWhere(a => a.AlbumId == album.AlbumId));
				if (data.GenreId != 0)
					if (albumData.Album.Songs.Where(s => s.Genre.GenreId == data.GenreId).ToList().Count == 0)
						allalbums.Remove(album);
				if (data.Name != null && (data.InAlbum || data.InArtist || data.InSong))
				{
					if (data.InAlbum && album.AlbumName.IndexOf(data.Name, StringComparison.Ordinal) != -1) nameFlag++;
					if (data.InArtist && albumData.Artists.Where(art => art.ArtistName.IndexOf(data.Name, StringComparison.Ordinal) != -1).ToList().Count != 0) nameFlag++;
					if (data.InSong && albumData.Album.Songs.Where(art => art.SongName.IndexOf(data.Name, StringComparison.Ordinal) != -1).ToList().Count != 0) nameFlag++;
					if (nameFlag == 0) allalbums.Remove(album);
				}
				
			}
			return View("SearchResult", allalbums);
		}

	}
}