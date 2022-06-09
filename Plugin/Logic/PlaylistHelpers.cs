using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MusicBeePlugin.Logic
{
    public class PlaylistHelpers
    {
        private readonly Plugin.MusicBeeApiInterface _mbApi;

        public PlaylistHelpers(Plugin.MusicBeeApiInterface mbApi)
        {
            _mbApi = mbApi;
        }
        
        public List<string> GetAllNonRadioPlaylistUris()
        {
            var allPlaylistUris = new List<string>();

            _mbApi.Playlist_QueryPlaylists();

            var currentPlaylistUri = _mbApi.Playlist_QueryGetNextPlaylist(); 

            while (currentPlaylistUri != null)
            {
                Debug.WriteLine($"Now loading playlist: {Path.GetFileNameWithoutExtension(currentPlaylistUri)}");
                
                var currentPlaylistType = _mbApi.Playlist_GetType(currentPlaylistUri);

                if (currentPlaylistType != Plugin.PlaylistFormat.Radio)
                {
                    allPlaylistUris.Add(currentPlaylistUri);
                }
                
                currentPlaylistUri = _mbApi.Playlist_QueryGetNextPlaylist();
            }

            return allPlaylistUris;
        }
        
        public List<string> GetAllFilePathsIn(string playlistUri)
        {
            var allPlaylistTracks = new List<string>();

            _mbApi.Playlist_QueryFiles(playlistUri);

            var currentPlaylistTrack = _mbApi.Playlist_QueryGetNextFile(); 

            while (currentPlaylistTrack != null)
            {
                allPlaylistTracks.Add(currentPlaylistTrack);
                
                currentPlaylistTrack = _mbApi.Playlist_QueryGetNextFile();
            }

            return allPlaylistTracks;
        }
    }
}