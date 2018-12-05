using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.DataBase.Entities
{
	public class Artist
	{
		//Primary Key
		public int ArtistId { get; set; }
		[MaxLength(30)]
		public string ArtistName {get; set;}
		public virtual List<ArtistList> ArtistLists { get; set; }
	}
}