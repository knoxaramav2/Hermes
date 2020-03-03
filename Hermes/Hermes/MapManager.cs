using Hermes.projects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hermes
{
    //Provides methods for interacting with map information
    //and disk operations
    public class MapManager
    {
        private Dictionary<uint, MapInfo> _maps;
        private MapInfo _currentMap;
        private uint _mapIdCounter;

        public MapManager()
        {
            _maps = new Dictionary<uint, MapInfo>();
            _currentMap = null;
            _mapIdCounter = 0;

            RefreshMapFiles();
        }

        private void RefreshMapFiles()
        {
            if (ProjectManager.GetProjectName() == null) { return; }

            _maps = new Dictionary<uint, MapInfo>();

            var dir = ProjectFileHandler.GetFormattedUserSpacePath("maps", true);
            if (dir == null) { return; }

            var dirInfo = new DirectoryInfo(dir);

            uint maxMapIndex = 0;

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                if (file.Extension.ToLower() != ".map") { continue; }

                var json = File.ReadAllText(file.FullName);
                var map = JsonConvert.DeserializeObject<MapInfo>(json);
                _maps.Add(map.MapId, map);
                
                if (map.MapId > maxMapIndex) { maxMapIndex = map.MapId; }

                _currentMap = map;
            }

            _mapIdCounter = maxMapIndex;
            ProjectManager.SetMapCounter(_mapIdCounter);
        }

        //TODO handle file not found
        public bool LoadMapData(uint mapId)
        {
            _maps.TryGetValue(mapId, out MapInfo map);

            if (map==null) { return false; }
            map.Load(map.MapId.ToString());
            return true;
        }

        public void SaveMapData()
        {
            if (_currentMap == null) { return; }
            _currentMap.Save();
        }

        public void SaveAllMapData()
        {
            if (_currentMap == null) { return; }

            var eMaps = _maps.GetEnumerator();
            while (eMaps.MoveNext())
            {
                eMaps.Current.Value.Save();
            }
        }

        public uint NewMap(string name, ushort width, ushort height)
        {

            var map = new MapInfo()
            {
                Name = name,
                Width = width,
                Height = height,
                MapId = _mapIdCounter++
            };

            map.Save();

            return map.MapId;
        }
        
        public bool DeleteMap(string id=null)
        {
            if (id == null && _currentMap == null)
            {
                return false;
            } else
            {

            }

            var path = ProjectFileHandler.GetFormattedUserSpacePath(@$"projects\\{id}\\maps");

            return true;
        }

        public MapInfo GetCurrentMap()
        {
            return _currentMap;
        }

        public bool SelectMap(uint id)
        {
            if (!_maps.TryGetValue(id, out MapInfo map))
            {
                return false;
            }

            _currentMap = map;

            return true;
        }

        public System.Collections.ObjectModel
            .ReadOnlyCollection<KeyValuePair<uint, MapInfo>>  GetMapsAsList()
        {
            var list = _maps.ToList();

            return list.AsReadOnly();
        }
    }

    //TODO hide public setters
    public class MapInfo : Serializeable
    {
        public string Name { get; set; }
        public uint MapId { get; set; }

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
            var path = GetMapPath(MapId.ToString());
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(path, json);
        }

        private string GetMapPath(string id)
        {
            return ProjectFileHandler.GetFormattedUserSpacePath($@"\maps\{id}.map", true);
        }
    }
}
