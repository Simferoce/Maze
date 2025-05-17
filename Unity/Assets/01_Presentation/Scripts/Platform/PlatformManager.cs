using System.Collections.Generic;
using System.IO;

namespace Game.Presentation
{
    public class PlatformManager : IService
    {
        public void Save(string path, byte[] data)
        {
            File.WriteAllBytes(path, data);
        }

        public byte[] Load(string path)
        {
            if (!FileExists(path))
                return null;

            return File.ReadAllBytes(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public IEnumerable<string> EnumerateFiles(string path)
        {
            return Directory.EnumerateFiles(path);
        }
    }
}
