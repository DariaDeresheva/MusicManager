using System.Collections.Generic;
using MusicManager.DataBase.Entities;
using System.Linq;

namespace MusicManager.Models
{
	public class AlbumData
	{
		public List<Artist> Artists { get; set; }
		public Album Album { get; set; }
		//Constructor
		public AlbumData(List<Artist> artist, Album album)
		{
			Artists = artist;
			Album = album;
		}

		public AlbumData(Album album)
		{
			Artists = album.Songs.SelectMany(song => song.Artists.Select(list => list.Artist)).Distinct().ToList();
			Album = album;
		}
	}
}