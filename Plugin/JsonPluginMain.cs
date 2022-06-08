using System;
using MusicBeePlugin.PlaylistHelpers;

namespace MusicBeePlugin
{
    public class JsonPluginMain
    {
        private readonly Plugin.MusicBeeApiInterface _mbApi;

        public JsonPluginMain(Plugin.MusicBeeApiInterface mbApi)
        {
            _mbApi = mbApi;
        }

        public void Main(Plugin.NotificationType mbEventType)
        {
            if (InvalidEventType(mbEventType)) return;
            
            // Otherwise:
            var playlistHelpers = new PlaylistHelper(_mbApi);

            var playlists = playlistHelpers.GetAllNonRadioPlayListUris();

            Console.WriteLine();
        }

        private bool InvalidEventType(Plugin.NotificationType mbEventType)
        {
            if (mbEventType != Plugin.NotificationType.PluginStartup &&
                mbEventType != Plugin.NotificationType.PlaylistUpdated &&
                mbEventType != Plugin.NotificationType.PlaylistCreated &&
                mbEventType != Plugin.NotificationType.PlaylistDeleted
               ) return false;

            return true;
        }
    }
}