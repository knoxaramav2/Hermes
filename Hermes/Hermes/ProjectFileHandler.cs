using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hermes
{
    class ProjectFileHandler
    {
        //TODO check for duplicate, return bool
        public static void CreateProjectDirectories(string dir)
        {
            Directory.CreateDirectory(dir);
            Directory.CreateDirectory(dir + @"\maps");
            Directory.CreateDirectory(dir + @"\rsc");
            Directory.CreateDirectory(dir + @"\scripts");
            Directory.CreateDirectory(dir + @"\notes");
        }

        public static void DeleteProject(string path)
        {
            var dirInfo = new DirectoryInfo(path);

            //TODO validate directory as project

            foreach(FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
            foreach(DirectoryInfo dir in dirInfo.GetDirectories())
            {
                dir.Delete(true);
            }

            dirInfo.Delete();
        }

        public static void LoadConfigData()
        {
            var viewData = LoadConfigFile("localenv.config");
        }

        private static string LoadConfigFile(string fileName)
        {
            var asm = Assembly.GetExecutingAssembly();
            //var content = "";

            var rscPath = asm.GetManifestResourceNames().Single(str => str.EndsWith(fileName));

            using (Stream stream = asm.GetManifestResourceStream(rscPath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static string GetLocalPath()
        {
            string workingDirectory = Environment.CurrentDirectory;
            return Directory.GetParent(workingDirectory).Parent.FullName;
        }

        public static void ValidateFileStructure()
        {
            ValidateDirectory("..\\projects");


        }

        private static void ValidateDirectory(string path)
        {
            //var originDir = AppDomain.CurrentDomain.BaseDirectory + "\\";
            //Directory.CreateDirectory(originDir+path);
        }

        public static string GetFormattedUserSpacePath(string dir)
        {
            string workingDirectory = Environment.CurrentDirectory;
            return Directory.GetParent(workingDirectory).Parent.Parent.FullName+@$"\{dir}";
        }
    }
}
