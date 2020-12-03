using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AutoFileBackupper_dotnet5.Models;

namespace AutoFileBackupper_dotnet5
{
    class Program
    {
        const string BckpDirectoryName = "old";

        static void Main(string[] args)
        {
            string file_path = "C:\\Users\\shimane\\Desktop\\my.txt";

            // Set variable for "File Name", "File Extention", "Source Directory" 
            string file_name = Path.GetFileNameWithoutExtension(file_path);
            string file_ext = Path.GetExtension(file_path);
            string source_dir = Path.GetDirectoryName(file_path);

            CheckBckpFile(source_dir);

            // Do file copy operation.
            //File.Copy(file_path, "New.txt");

            Console.WriteLine("Enter a key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Check Backup directory and if not, create as new.
        /// Then, create new backup file.
        /// </summary>
        /// <param name="source_dir"></param>
        /// <returns></returns>
        static string CheckBckpFile(string source_dir)
        {
            // Create Backup path
            string bckp_dir = source_dir + "\\" + BckpDirectoryName;

            if (Directory.Exists(bckp_dir)){
                foreach (var file in Directory.GetFiles(bckp_dir))
                {
                    string file_name = Path.GetFileNameWithoutExtension(file);
                    var version = VersionManager.ToVerionInfoArray(Regex.Match(file_name, VersionManager.GetVersionFormat()).Value);
                    foreach(int x in version)
                    {
                        Console.WriteLine("value = {0}", x);
                    }
                    
                }
            }
            else
            {
                // Couldn't find a backup folder, create a new one.
                Directory.CreateDirectory(bckp_dir);
            }
            return "";
        }
        

    }
}
