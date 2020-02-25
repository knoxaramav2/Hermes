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
        public static void LoadConfigData()
        {
            var viewData = LoadConfigFile("localenv.config");
        }

        private static string LoadConfigFile(string fileName)
        {
            var asm = Assembly.GetExecutingAssembly();
            var content = "";

            var rscPath = asm.GetManifestResourceNames().Single(str => str.EndsWith(fileName));

            using (Stream stream = asm.GetManifestResourceStream(rscPath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
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
            return Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.ToString();
                //.Parent + "\\" + dir;
        }
    }
}
