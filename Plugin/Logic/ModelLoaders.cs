using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using MusicBeePlugin.Models;

namespace MusicBeePlugin.Logic
{
    public class ModelLoaders
    {
        private readonly Plugin.MusicBeeApiInterface _mbApi;

        public ModelLoaders(Plugin.MusicBeeApiInterface mbApi)
        {
            _mbApi = mbApi;
        }

        public IEnumerable<Playlist> LoadAllPlaylists()
        {
            var playlistUris = new PlaylistHelpers(_mbApi).GetAllNonRadioPlaylistUris();

            var playlists = new List<Playlist>();

            foreach (var playlistUri in playlistUris)
            {
                // These are massive on my computer, so I'm skipping them while testing.
                if (playlistUri.Contains("Need Rating")) continue;
                if (playlistUri.Contains("Recently Added")) continue;
                
                playlists.Add(
                    new Playlist()
                    {
                        FileUri = playlistUri,
                        Name = Path.GetFileNameWithoutExtension(playlistUri),
                        Tracks = LoadPlaylistTracks(playlistUri)
                    });
            }

            return playlists;
        }

        private List<Track> LoadPlaylistTracks(string playlistUri)
        {
            Debug.WriteLine($"Starting to load playlist: {Path.GetFileNameWithoutExtension(playlistUri)}");
            
            var playlistTrackFilePaths = new PlaylistHelpers(_mbApi).GetAllFilePathsIn(playlistUri);

            var tracks = new List<Track>();
            
            var trackFetcher = new TrackFetcher();
            
            foreach (var trackFilePath in playlistTrackFilePaths)
            {
                var track = trackFetcher.LoadTrackData(trackFilePath);
                
                tracks.Add(track);
            }

            Debug.WriteLine($"Loaded playlist: {Path.GetFileNameWithoutExtension(playlistUri)}");
            
            return tracks;
        }
    }
}