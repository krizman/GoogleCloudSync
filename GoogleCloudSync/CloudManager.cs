using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloudSync
{
    public class CloudManager
    {
        private readonly FileSystemWatcher fileSystemWatcher;

        public CloudManager(
            string pathToWatch, 
            string fileFilter = null, 
            bool includeSubdirectories = false)
        {
            fileFilter = string.IsNullOrWhiteSpace(fileFilter) ? "*.*" : fileFilter;
            fileSystemWatcher = new FileSystemWatcher(pathToWatch, fileFilter);
            fileSystemWatcher.Created += FileSystemWatcher_Created;
            //fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            //fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.IncludeSubdirectories = includeSubdirectories;
            fileSystemWatcher.NotifyFilter =
                NotifyFilters.CreationTime
                | NotifyFilters.DirectoryName
                | NotifyFilters.FileName
                | NotifyFilters.LastAccess
                | NotifyFilters.LastWrite;

            fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"{nameof(FileSystemWatcher_Changed)} File {e.FullPath} was {e.ChangeType}");
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"{nameof(FileSystemWatcher_Renamed)} File {e.FullPath} was {e.ChangeType}");
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"{nameof(FileSystemWatcher_Created)} File {e.FullPath} was {e.ChangeType}");
        }
    }
}
