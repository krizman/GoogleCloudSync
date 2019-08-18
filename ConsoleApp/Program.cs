using GoogleCloudSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2 
                || new[] { "-h", "--help" }.Contains(args.ElementAtOrDefault(0)))
            {
                Console.WriteLine("Program requires at least the first two arguments from the following list:");
                Console.WriteLine("[0]: Google Cloud bucket name to upload files to");
                Console.WriteLine("[1]: Absolute file path to the directory where new files will be inserted");
                Console.WriteLine("[2]: Filter for files to monitor");
                Console.WriteLine("[3]: Boolean true/false whether to also monitor subdirectories");

                // sample cmd arguments: login5-agro-files-source-1566127485 C:\Users\urosk\Downloads\LD_A2302895_20190201_20190301.csv\test\ *.*
            }
            string bucket = args.ElementAtOrDefault(0);
            string path = args.ElementAtOrDefault(1);
            string filter = args.ElementAtOrDefault(2);
            bool.TryParse(args.ElementAtOrDefault(3), out bool includeSubdir);

            CloudManager cloudManager = new CloudManager(bucket, path, filter, includeSubdir);
            cloudManager.StartWatchingFiles();

            Console.WriteLine($"Listening for new files on path '{path}' (include subdirs={includeSubdir}) with filter '{filter}'");
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
        }
    }
}
