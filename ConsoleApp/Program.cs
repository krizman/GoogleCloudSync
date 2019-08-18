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
            string path = args.ElementAtOrDefault(0);
            string filter = args.ElementAtOrDefault(1);
            bool.TryParse(args.ElementAtOrDefault(2), out bool includeSubdir);
            new CloudManager(path, filter, includeSubdir);

            Console.WriteLine($"Listening for new files on path '{path}' (include subdirs={includeSubdir}) with filter '{filter}'");
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
        }
    }
}
