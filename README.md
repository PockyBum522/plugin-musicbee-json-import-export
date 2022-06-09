# MusicBeeJsonExportImportPlugin
Simple plugin to export MusicBee playlists as JSON

The plugin currently loads on MusicBee startup and outputs all playlists in MusicBee to a JSON file.

Currently the models are:

![image](https://user-images.githubusercontent.com/1970959/172743514-6e28d2a6-0ebf-47ee-9786-ee57c5e5522f.png)

In the JSON file it exports, you can see that it is an array of playlists. Each playlist has an array of the Track model shown above. An example of a track is:

"Title": "Witches Burn",
"Artist": "The Pretty Reckless",
"AlbumArtist": "",
"Album": "Death By Rock And Roll",
"Rating": 100

(AlbumArtist is intentionally blank in my tags, but I know many people use it, so I included it.)

And then of course the tracks in said playlist continue until the next playlist in the JSON file.
