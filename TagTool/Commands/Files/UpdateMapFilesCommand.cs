using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagTool.Cache;
using TagTool.Common;
using TagTool.Tags.Definitions;
using TagTool.IO;
using TagTool.Serialization;
using TagTool.BlamFile;

namespace TagTool.Commands.Files
{
    class UpdateMapFilesCommand : Command
    {
        public GameCache Cache { get; }

        public UpdateMapFilesCommand(GameCache cache)
            : base(true,

                  "UpdateMapFiles",
                  "Updates the game's .map files to contain valid scenario indices.",

                  "UpdateMapFiles <MapInfo Directory> [forceupdate]",

                  "Updates the game's .map files to contain valid scenario indices.")
        {
            Cache = cache;
        }

        public override object Execute(List<string> args)
        {
            if (args.Count > 2)
            {
                return false;
            }

            bool forceUpdate = false;
            bool hasMapInfo = false;

            if (args.Count >= 1)
                hasMapInfo = true;
            if (args.Count == 2)
                if (args[1].ToLower() == "forceupdate")
                    forceUpdate = true;

            // Generate / update the map files
            foreach(var scenario in Cache.TagCache.FindAllInGroup("scnr"))
            {
                var name = scenario.Name.Split('\\').Last();
                var mapInfoName = $"{name}.mapinfo";
                var mapFileName = $"{name}.map";
                var targetPath = Path.Combine(Cache.Directory.FullName, mapFileName);

                MapFile map;
                Blf mapInfo = null;

                if(hasMapInfo)
                {
                    var mapInfoDir = new DirectoryInfo(args[0]);
                    var files = mapInfoDir.GetFiles(mapInfoName);
                    if(files.Length != 0)
                    {
                        var mapInfoFile = files[0];
                        using (var stream = mapInfoFile.Open(FileMode.Open, FileAccess.Read))
                        using (var reader = new EndianReader(stream))
                        {
                            CacheVersion version = CacheVersion.Halo3Retail;
                            switch (reader.Length)
                            {
                                case 0x4e91:
                                    version = CacheVersion.Halo3Retail;
                                    break;
                                case 0x9A01:
                                    version = CacheVersion.Halo3ODST;
                                    break;
                                default:
                                    throw new Exception("Unexpected map info file size");
                            }
                            mapInfo = new Blf(version);
                            mapInfo.Read(reader);
                            mapInfo.ConvertBlf(Cache.Version);
                        }
                    }
                }

                try
                {
                    var fileInfo = Cache.Directory.GetFiles(mapFileName)[0];
                    map = new MapFile();
                    using (var stream = fileInfo.Open(FileMode.Open, FileAccess.Read))
                    using (var reader = new EndianReader(stream))
                        map.Read(reader);
                    map.Header.ScenarioTagIndex = scenario.Index;

                    if(mapInfo != null)
                        if(forceUpdate || map.MapFileBlf == null)
                            map.MapFileBlf = mapInfo;
                                 
                }
                catch (Exception)
                {
                    map = GenerateMapFile(scenario, mapInfo);
                }

                var targetFile = new FileInfo(targetPath);
                using(var stream = targetFile.Create())
                using(var writer = new EndianWriter(stream))
                {
                    map.Write(writer);
                }

                if (mapInfo != null)
                    Console.WriteLine($"Scenario tag index for {name}: 0x{scenario.Index.ToString("X4")} (using map info)");
                else
                    Console.WriteLine($"Scenario tag index for {name}: 0x{scenario.Index.ToString("X4")} (WARNING: not using map info)");

            }
            Console.WriteLine("Done!");
            return true;
        }

        private MapFile GenerateMapFile(CachedTag scenarioTag, Blf mapInfo = null)
        {
            MapFile map = new MapFile();
            var header = new CacheFileHeader();
            Scenario scnr;
            using (var stream = Cache.OpenCacheRead())
            {
                scnr = Cache.Deserialize<Scenario>(stream, scenarioTag);
            }
                
            map.Version = Cache.Version;
            map.EndianFormat = EndianFormat.LittleEndian;
            map.MapVersion = CacheFileVersion.HaloOnline;

            header.HeaderSignature = new Tag("head");
            header.FooterSignature = new Tag("foot");
            header.FileVersion = map.MapVersion;
            header.Build = CacheVersionDetection.GetBuildName(Cache.Version);

            switch (scnr.MapType)
            {
                case ScenarioMapType.MainMenu:
                    header.CacheType = CacheFileType.MainMenu;
                    break;
                case ScenarioMapType.SinglePlayer:
                    header.CacheType = CacheFileType.Campaign;
                    break;
                case ScenarioMapType.Multiplayer:
                    header.CacheType = CacheFileType.Multiplayer;
                    break;
            }
            header.SharedType = CacheFileSharedType.None;

            header.MapId = scnr.MapId;
            header.ScenarioTagIndex = scenarioTag.Index;
            header.Name = scenarioTag.Name.Split('\\').Last();
            header.ScenarioPath = scenarioTag.Name;

            map.Header = header;

            header.FileLength = 0x3390;

            if(mapInfo != null)
            {
                if(mapInfo.ContentFlags.HasFlag(BlfFileContentFlags.StartOfFile) && mapInfo.ContentFlags.HasFlag(BlfFileContentFlags.EndOfFile) && mapInfo.ContentFlags.HasFlag(BlfFileContentFlags.Scenario))
                {
                    map.MapFileBlf = mapInfo;
                }
            }
            return map;
        }
    }
}