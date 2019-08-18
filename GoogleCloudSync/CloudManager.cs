using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
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
        // TODO: add to config or something
        private const string bucketName = "login5-agro-files-source-1566127485";

        public CloudManager(
            string pathToWatch, 
            string fileFilter = null, 
            bool includeSubdirectories = false)
        {
            fileFilter = string.IsNullOrWhiteSpace(fileFilter) ? "*.*" : fileFilter;

            fileSystemWatcher = new FileSystemWatcher(pathToWatch, fileFilter);
            fileSystemWatcher.IncludeSubdirectories = includeSubdirectories;
            fileSystemWatcher.NotifyFilter =
                NotifyFilters.CreationTime
                | NotifyFilters.DirectoryName
                | NotifyFilters.FileName
                | NotifyFilters.LastAccess
                | NotifyFilters.LastWrite;

            fileSystemWatcher.Created += FileSystemWatcher_Created;

            fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"{nameof(FileSystemWatcher_Created)} File {e.FullPath} was {e.ChangeType}");

            UploadToCloud(e.FullPath);
        }

        private void UploadToCloud(string filePath)
        { 

            // Explicitly use service account credentials by specifying the private key file.
            // The service account should have Object Manage permissions for the bucket.
            GoogleCredential credential = GoogleCredential.FromFile("credentials.json");

            StorageClient storageClient = StorageClient.Create(credential);

            // TODO: handle file errors and GC upload errors
            using (var fileStream = new FileStream(
                filePath, 
                FileMode.Open,
                FileAccess.Read, 
                FileShare.Read))
            {
                Console.WriteLine($"Uploading file '{filePath}' to bucket '{bucketName}' ...");

                storageClient.UploadObject(bucketName, Path.GetFileName(filePath), "text/csv", fileStream);

                Console.WriteLine("Upload complete.");
            }
        }
    }
}
