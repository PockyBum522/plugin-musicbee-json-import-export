using System;
using System.IO;
using MusicBeePlugin.Logic;
using Newtonsoft.Json;

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
            SerializePlaylistsToFile(@"D:\Dropbox\Documents\Desktop\playlists.json");
        }

        private void SerializePlaylistsToFile(string outputPath)
        {
            var modelLoaders = new ModelLoaders(_mbApi);

            var playlists = modelLoaders.LoadAllPlaylists();

            var serializedPlaylists = JsonConvert.SerializeObject(playlists, Formatting.Indented);

            File.WriteAllText(outputPath, serializedPlaylists);
        }

        private bool InvalidEventType(Plugin.NotificationType mbEventType)
        {
            if (mbEventType != Plugin.NotificationType.PluginStartup) return false;

            return true;
        }
    }
}