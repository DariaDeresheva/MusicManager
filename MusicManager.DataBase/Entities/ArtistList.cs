namespace MusicManager.DataBase.Entities
{
    public class ArtistList
    {
        //Primary Key
        public int ArtistListId { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Song Song { get; set; }
    }
}