using System.IO;

namespace CheckBoardGameVersion3.common.Helpers
{
    public static class NoltFolderManager
    {
        private static string _serverPath;

        private static string _files = "Files";

        public static void InitializeFolderManager(string serverPath)
        {
            _serverPath = serverPath;

            CreateFilesFolderIfNotExists();

         
        }
        private static void CreateFilesFolderIfNotExists()
        {
            if (!Directory.Exists(Path.Combine(_serverPath, _files)))
            {
                Directory.CreateDirectory(Path.Combine(_serverPath, _files));
            }
        }
     
        public static string GetFilesFolderPath() => Path.Combine(_serverPath, _files);
    }
}
