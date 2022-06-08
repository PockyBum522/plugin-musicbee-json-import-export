using System.Collections.Generic;

namespace MusicBeePlugin.PlaylistHelpers
{
    public class PlaylistHelper
    {
        private readonly Plugin.MusicBeeApiInterface _mbApi;

        public PlaylistHelper(Plugin.MusicBeeApiInterface mbApi)
        {
            _mbApi = mbApi;
        }
        
        public List<string> GetAllNonRadioPlayListUris()
        {
            var allPlaylistUris = new List<string>();

            _mbApi.Playlist_QueryPlaylists();

            var currentPlaylistUri = _mbApi.Playlist_QueryGetNextPlaylist(); 

            while (currentPlaylistUri != null)
            {
                var currentPlaylistType = _mbApi.Playlist_GetType(currentPlaylistUri);

                if (currentPlaylistType != Plugin.PlaylistFormat.Radio)
                {
                    allPlaylistUris.Add(currentPlaylistUri);
                }
                
                currentPlaylistUri = _mbApi.Playlist_QueryGetNextPlaylist();
            }

            return allPlaylistUris;
        }
    }
}