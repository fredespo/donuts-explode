using System;
using System.IO;
using UnityEngine;

namespace AlmostEngine
{
    public class PathUtils
    {
        public static bool IsValidPath(string path)
        {
#if UNITY_EDITOR_WIN
            // Check thar first 3 chars are a drive letter
            if (path.Length < 3)
                return false;
            if (!Char.IsLetter(path[0]))
                return false;
            if (path[1] != ':')
                return false;
            if (path[2] != '\\' && path[2] != '/')
                return false;
#endif

            if (String.IsNullOrEmpty(path))
            {
                return false;
            }

            char[] invalids = Path.GetInvalidPathChars();
            foreach (char c in invalids)
            {
                if (path.Contains(c.ToString()))
                    return false;
            }

            try
            {
                Path.GetFullPath(path);
            }
            catch
            {
                return false;
            }


            return true;
        }

        public static bool CreateExportDirectory(string path)
        {

            // Create the folder if needed
            string fullpath = path;
            if (string.IsNullOrEmpty(fullpath))
            {
                Debug.LogError("Can not create directory, filename is null or empty.");
                return false;
            }

            fullpath = fullpath.Replace("\\", "/");

            if (!fullpath.Contains("/"))
            {
                Debug.LogError("Can not create directory, filename is not a valid path : " + path);
                return false;
            }

            fullpath = fullpath.Substring(0, fullpath.LastIndexOf('/'));

            if (!System.IO.Directory.Exists(fullpath))
            {
                Debug.Log("Creating directory " + fullpath);
                try
                {
                    System.IO.Directory.CreateDirectory(fullpath);
                }
                catch
                {
                    Debug.LogError("Failed to create directory : " + fullpath);
                    return false;
                }
            }

            return true;
        }

        public static string PreventOverwrite(string fullname, int frameNumberPadding = 0)
        {
            if (File.Exists(fullname))
            {
                string filename = Path.GetDirectoryName(fullname) + "/" + Path.GetFileName(fullname);
                string extension = Path.GetExtension(fullname);
                filename = filename.Replace(extension, "");
                int i = 1;
                while (File.Exists(filename + " (" + i.ToString().PadLeft(frameNumberPadding, '0') + ")" + extension))
                {
                    i++;
                }
                return filename + " (" + i.ToString().PadLeft(frameNumberPadding, '0') + ")" + extension;
            }
            else
            {
                return fullname;
            }
        }
    }
}

