using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.DataBase.Entities
{
	public class Genre
	{
		//Primary Key
		public int GenreId { get; set; }
		[MaxLength(30)]
		public string GenreName { get; set; }
		public virtual List<Song> Songs { get; set; }
	}
}