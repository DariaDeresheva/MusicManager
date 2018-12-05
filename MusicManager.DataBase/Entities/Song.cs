using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.DataBase.Entities
{
	public class Song
	{
		//Primary Key
		public int SongId { get; set; }
		[MaxLength(50)]
		public string SongName { get; set; }
		[Range(0, 5)]
		public short SongRating { get; set; }
		[Range(1,100)]
		public short SongNumber { get; set; }
		public virtual List<ArtistList> Artists { get; set; }
		public virtual Genre Genre { get; set; }
		public virtual Album Album { get; set; }
	

	}
}