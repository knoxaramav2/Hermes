using Hermes.projects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hermes
{
    //Provides methods for interacting with map information
    //and disk operations
    class MapManager
    {
        private List<MapInfo> _maps;
        private MapInfo _currentMap;
        private uint _mapIdCounter;

        public MapManager()
        {
            _maps = new List<MapInfo>();
            _currentMap = null;
        }

        //TODO handle file not found
        public void LoadMapData(string mapId)
        {
            foreach(var map in _maps)
            {
                if (map.MapId == mapId)
                {
                    map.Load(mapId);
                    return;
                }
            }
        }

        public void SaveMapData()
        {
            if (_currentMap == null) { return; }
            _currentMap.Save();
        }

        public void NewMap(string name, ushort width, ushort height)
        {
            var path = ProjectFileHandler.GetFormattedUserSpacePath(@$"projects\\{1}\\maps");
            var map = new MapInfo()
            {
                Name = name,
                Width = width,
                Height = height,
            };

            map.Save();
        }
        
        public void DeleteMap()
        {

        }
    }

    //TODO hide public setters
    class MapInfo : Serializeable
    {
        public string Name { get; set; }
        public string MapId { get; set; }

        public ushort Width { get; set; }
        public ushort Height { get; set; }

        public void Load(string id)
        {
            var path = GetMapPath(id);
            var json = File.ReadAllText(path);
            MapInfo info = JsonConvert.DeserializeObject<MapInfo>(json);

            Name = info.Name;
            MapId = info.MapId;
            Width = info.Width;
            Height = info.Height;
        }

        public void Save()
        {
            var path = GetMapPath(MapId);
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(path, json);
        }

        private string GetMapPath(string id)
        {
            return ProjectFileHandler.GetFormattedUserSpacePath($@"\\maps\\{id}.map");
        }
    }
}
