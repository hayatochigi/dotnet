using System;
using System.Text.RegularExpressions;

namespace AutoFileBackupper_dotnet5.Models
{
    internal class VersionManager
    {
        
        /// <summary>
        /// Convert Version string (ex, "1_2_3") to int array (ex, {1, 2, 3})
        /// </summary>
        /// <param name="file_name">Input version string "%d_%d_%d"</param>
        /// <returns>Converted version string to int[]</returns>
        internal static string ToVerionInfoArray(string file_name)
        {
            if (file_name == "")
            {
                // Return default version info.
                return "1_0_0";
            }
            else
            {
                // Search version string.
                return Regex.Match(file_name, "\\d{1,}_\\d{1,}_\\d{1,}").Value;
            }
        }
    }
}
