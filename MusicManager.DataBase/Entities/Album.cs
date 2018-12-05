using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.DataBase.Entities
{
	public class Album
	{
		//Primary Key
		public int AlbumId { get; set; }
		[MaxLength(50)]
		public string AlbumName { get; set; }
		[Range(0,5)]
		public short AlbumRating { get; set; }
		public short AlbumYear { get; set; }
		//Album Cover File Name
		public string AlbumCover { get; set; }
		public virtual List<Song> Songs { get; set; }
	}
}