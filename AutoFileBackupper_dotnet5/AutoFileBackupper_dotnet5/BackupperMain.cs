#define DEV

using AutoFileBackupper_dotnet5.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoFileBackupper_dotnet5
{
    class Program
    {
        // Target backup foloder is named "old".
        const string BckpDirectoryName = "old";

        static void Main(string[] args)
        {
            string file_path;
#if DEV
            file_path = "C:\\Users\\shimane\\Desktop\\my.txt";
#else
            // Target file would be 1st of args parameter.
            file_path = args[0];
#endif
            // Set variable for "File Name", "File Extention", "Source Directory" 
            string file_name = Path.GetFileNameWithoutExtension(file_path);
            string file_ext = Path.GetExtension(file_path);
            string source_dir = Path.GetDirectoryName(file_path);

            string bckp_dir = source_dir + "\\" + BckpDirectoryName;

            string next_version = UpdateVersion(CheckBckpFile(bckp_dir), "major");

            try { 
            // Do file copy operation.
            File.Copy(file_path, bckp_dir + "\\" + file_name + "_" + next_version + file_ext);
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Enter a key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Check Backup directory and if not, create as new.
        /// Then, create new backup file.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        static string CheckBckpFile(string bckp_dir)
        {
            // Create Backup path
            var VersionList = new List<string>();

            if (Directory.Exists(bckp_dir)){
                foreach (var file in Directory.GetFiles(bckp_dir))
                {
                    string file_name = Path.GetFileNameWithoutExtension(file);
                    string version = VersionManager.ToVerionInfoArray(file_name);
                    VersionList.Add(version);
                }

                // Get the latest version string.
                VersionList.Reverse();

                // VersionList[0] must include the latest version string.
                return VersionList[0];
                
            }
            else
            {
                // Couldn't find a backup folder, create a new one.
                Directory.CreateDirectory(bckp_dir);
            }
            // Return 1st version info (0_0_0).
            return "0_0_0";
        }

        static string UpdateVersion(string current_ver, string type)
        {
            // Convert version string into version int array.
            var version_arr = Array.ConvertAll(Regex.Split(current_ver, "_"), s => int.Parse(s));

            // Case non sensitive, use "ToLower" method here.
            switch (type.ToLower())
            {
                case "major":
                    version_arr[0] = version_arr[0] + 1;
                    break;
                case "minor":
                    version_arr[1] = version_arr[1] + 1;
                    break;
                case "rev":
                    version_arr[2] = version_arr[2] + 1;
                    break;
                default:
                    break;
            }
            return string.Join("_", version_arr);
        }
        

    }
}
