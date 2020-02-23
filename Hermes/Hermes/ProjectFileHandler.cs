using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hermes
{
    class ProjectFileHandler
    {
        public static void ValidateFileStructure()
        {
            ValidateDirectory("..\\projects");


        }

        private static void ValidateDirectory(string path)
        {
            var originDir = AppDomain.CurrentDomain.BaseDirectory + "\\";
            Directory.CreateDirectory(originDir+path);
        }

        public static string GetFormattedUserSpacePath(string dir)
        {
            return Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)
                .Parent + "\\" + dir;
        }
    }
}
