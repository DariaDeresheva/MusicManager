using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicManager.Models
{
	public class SearchData
	{
		public string Name { get; set; }
		public bool InAlbum { get; set; }
		public bool InArtist { get; set; }
		public bool InSong { get; set; }
		public int Rating { get; set; }
		public int Year { get; set; }
		public int GenreId { get; set; }
	}
}