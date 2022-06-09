using System;
using MusicBeePlugin.Models;
using TagLib;

namespace MusicBeePlugin.Logic
{
    public class TrackFetcher
    {
        public Track LoadTrackData(string trackFilePath)
        {
            var returnTrack = new Track();

            var trackTagFile = TagLib.File.Create(trackFilePath);

            // Album
            returnTrack.Album = trackTagFile.Tag.Album;
            
            // Artist
            returnTrack.Artist = trackTagFile.Tag.Performers[0];
            
            // Album Artist
            returnTrack.AlbumArtist = ReadAlbumArtistFromTag(trackTagFile);

            // Title
            returnTrack.Title = trackTagFile.Tag.Title;
            
            // Rating
            returnTrack.Rating = ReadRatingFromTag(trackTagFile);
            
            // File Path
            returnTrack.FilePath = trackFilePath;

            return returnTrack;
        }

        private string ReadAlbumArtistFromTag(File trackTagFile)
        {
            var artists = trackTagFile.Tag.AlbumArtists;

            if (artists.Length == 0) return "";
            
            return trackTagFile.Tag.AlbumArtists[0];
        }

        private static int ReadRatingFromTag(File trackTagFile)
        {
            var custom = (TagLib.Ogg.XiphComment)trackTagFile.GetTag(TagLib.TagTypes.Xiph);
            
            var ratingAsStrings = custom.GetField("RATING");

            if (ratingAsStrings.Length == 0)
            {
                return 0;
            }

            if (ratingAsStrings[0].Contains("."))
            {
                var parsedDouble = double.Parse(ratingAsStrings[0]);

                var convertedInt = (int)(parsedDouble * 10);
                
                return convertedInt;
            }
            
            return int.Parse(ratingAsStrings[0]);
        }
    }
}