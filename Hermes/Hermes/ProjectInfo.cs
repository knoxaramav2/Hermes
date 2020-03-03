using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hermes.projects
{
    public class ProjectManager
    {
        private static ProjectInfo _project;

        public ProjectManager()
        {
            _project = null;
        }

        public string GetProjectTitle()
        {
            if (_project == null) return "";
            return _project.ProjectName;
        }

        public void NewProject(string name)
        {            
            ProjectFileHandler.CreateProjectDirectories(name);
            ProjectInfo project = new ProjectInfo()
            {
                ProjectName = Path.GetFileNameWithoutExtension(name),
                MapCounter = 0
            };

            project.Save();
            _project = project;
        }

        public bool LoadProject(string name)
        {
            //var json = File.ReadAllText(name);
            var stream = new StreamReader(name);
            var jtr = new JsonTextReader(stream);
            var jsr = new JsonSerializer();
            var info = jsr.Deserialize<ProjectInfo>(jtr);

            if (info == null) { return false; }
            _project = info;

            return true;
        }

        public bool SaveProject()
        {
            if (_project == null) { return false; }
            _project.Save();
            return true;
        }

        public void IncrementMapCounter()
        {
            ++_project.MapCounter;
        }

        public void SetMapCounter(uint val)
        {
            if (_project == null) { return; }

            _project.MapCounter = val;
        }

        public static string GetProjectName()
        {
            return _project == null ? null : _project.ProjectName;
        }

        public static uint? GetMapCounter()
        {
            return _project == null ? 0 : _project.MapCounter;
        }
    }

    //Represents high level info about projects
    class ProjectInfo : Serializeable
    {
        
        public string ProjectName { get; set; }
        public uint MapCounter {get; set; }

        public void Load(string name)
        {
            var path = GetProjectPath(name);

            var json = File.ReadAllText(path);
            ProjectInfo info = JsonConvert.DeserializeObject<ProjectInfo>(json);

            ProjectName = info.ProjectName;
            MapCounter = info.MapCounter;
        }

        public void Save()
        {
            var path = GetProjectPath(ProjectName);
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(path, json);
        }

        private string GetProjectPath(string name)
        {
            return ProjectFileHandler.GetFormattedUserSpacePath($@"\\projects\\{name}\\{name}.hrms");
        }
    }
}
