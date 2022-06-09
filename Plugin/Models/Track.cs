namespace MusicBeePlugin.Models
{
    public class Track
    {
        public string FilePath { get; set; }
        
        public string Title { get; set; }
        public string Artist { get; set; }
        public string AlbumArtist { get; set; }
        public string Album { get; set; }
        public int Rating { get; set; }
    }
}