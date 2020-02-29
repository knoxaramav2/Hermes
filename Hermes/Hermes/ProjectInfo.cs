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
            if (_project == null)
            {
                ++_project.MapCounter;
                _project.Save();
            } else
            {
                return;
            }
        }


        public string GetProjectName()
        {
            return _project == null ? null : _project.ProjectName;
        }

        public uint GetMapCounter()
        {
            return _project == null ? 0 : _project.MapCounter;
        }
    }

    //Represents high level info about projects
    class ProjectInfo : Serializeable
    {
        
        public string ProjectName { get; internal set; }
        public uint MapCounter {get; internal set; }

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
