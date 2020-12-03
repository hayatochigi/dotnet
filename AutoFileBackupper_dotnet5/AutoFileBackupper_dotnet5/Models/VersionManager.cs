using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutoFileBackupper_dotnet5.Models
{
    internal class VersionManager
    {
        internal static string GetVersionFormat()
        {
            return "\\d{1,}_\\d{1,}_\\d{1,}"; 
        }

        /// <summary>
        /// Convert Version string (ex, "1_2_3") to int array (ex, {1, 2, 3})
        /// </summary>
        /// <param name="ver">Input version string "%d_%d_%d"</param>
        /// <returns>Converted version string to int[]</returns>
        internal static IEnumerable<int> ToVerionInfoArray(string ver)
        {
            if (ver == "")
            {
                // Return default version info.
                return new[] { 1, 0, 0 };
            }
            else
            {

                //return Array.ConvertAll(Regex.Split(ver, "_"), s => int.Parse(s));
                return null;
            }
        }
    }
}
