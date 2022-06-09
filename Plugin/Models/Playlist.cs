using System.Collections.Generic;

namespace MusicBeePlugin.Models
{
    public class Playlist
    {
        public string Name { get; set; }
        public string FileUri { get; set; }
        
        public List<Track> Tracks { get; set; }
    }
}