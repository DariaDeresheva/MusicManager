using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicManager.DataBase.Entities;

namespace MusicManager.Models
{
	public class FullAlbum
	{
		public Album Album { get; set; }
		public Song NewSong { get; set; }

		public FullAlbum(Album album)
		{
			Album = album;
			NewSong = new Song();
		}

		public FullAlbum()
		{
			
		}
	}
}