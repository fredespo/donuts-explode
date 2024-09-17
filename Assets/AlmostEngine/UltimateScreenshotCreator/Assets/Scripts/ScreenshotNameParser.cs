using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AlmostEngine.Screenshot
{
    public class ScreenshotNameParser
    {

        public enum DestinationFolder
        {
            CUSTOM_FOLDER,
            DATA_PATH,
            PERSISTENT_DATA_PATH,
            PICTURES_FOLDER
        }
        ;

        public static string ParseSymbols(string name, ScreenshotResolution resolution, System.DateTime time, int frameNumberPadding)
        {
            // Date
            name = name.Replace("{year}", time.Year.ToString());
            name = name.Replace("{month}", time.Month.ToString().PadLeft(2, '0'));
            name = name.Replace("{day}", time.Day.ToString().PadLeft(2, '0'));
            name = name.Replace("{hour}", time.Hour.ToString().PadLeft(2, '0'));
            name = name.Replace("{minute}", time.Minute.ToString().PadLeft(2, '0'));
            name = name.Replace("{second}", time.Second.ToString().PadLeft(2, '0'));

            // Dimensions
            name = name.Replace("{width}", resolution.m_Width.ToString());
            name = name.Replace("{height}", resolution.m_Height.ToString());
            name = name.Replace("{scale}", resolution.m_Scale.ToString());
            name = name.Replace("{ratio}", resolution.m_Ratio).Replace(":", "_");

            // Resolution
            name = name.Replace("{orientation}", resolution.m_Orientation.ToString());
            name = name.Replace("{name}", resolution.m_ResolutionName);
            name = name.Replace("{ppi}", resolution.m_PPI.ToString());
            name = name.Replace("{category}", resolution.m_Category);
            //			name = name.Replace ("{percent}", resolution.m_Stats.ToString ());

            // Scene
            name = name.Replace("{scene}", SceneManager.GetActiveScene().name);
#if UNITY_EDITOR
            name = name.Replace("{activeObject}", Selection.activeObject != null ? Selection.activeObject.name : "");
            name = name.Replace("{gameObjects}", (Selection.gameObjects.Length > 0) ? Selection.gameObjects.Select(x => x.name).Aggregate("", (x, y) => x + " " + y) : "");
#endif

            // Application
            name = name.Replace("{companyName}", Application.companyName);
            name = name.Replace("{productName}", Application.productName);
            name = name.Replace("{version}", Application.version);

            // Time
            name = name.Replace("{time}", Time.time.ToString());
            name = name.Replace("{unscaledTime}", Time.unscaledTime.ToString());
            name = name.Replace("{deltaTime}", Time.deltaTime.ToString());
            name = name.Replace("{unscaledDeltaTime}", Time.unscaledDeltaTime.ToString());
            name = name.Replace("{frameCount}", Time.frameCount.ToString().PadLeft(frameNumberPadding, '0'));



            return name;
        }

        public static string ParseExtension(TextureExporter.ImageFileFormat format)
        {
            switch (format)
            {
                case TextureExporter.ImageFileFormat.JPG:
                    return "jpeg";
                // case TextureExporter.ImageFileFormat.EXR:
                //     return "exr";
                case TextureExporter.ImageFileFormat.TGA:
                    return "tga";
                default:
                    return "png";
            }
        }

        public static string ParsePath(DestinationFolder destinationFolder, string customPath)
        {
            string path = "";

#if !UNITY_EDITOR && UNITY_ANDROID
                if (destinationFolder == DestinationFolder.PICTURES_FOLDER) {
				    path = AndroidUtils.GetExternalPictureDirectory() + "/" +customPath;
                } else {
				    path = AndroidUtils.GetFirstAvailableMediaStorage() + "/" +customPath;
                }
#elif !UNITY_EDITOR && UNITY_IOS
				path = Application.persistentDataPath + "/" +customPath;
#else
            if (destinationFolder == DestinationFolder.CUSTOM_FOLDER)
            {
                path = customPath;
            }
            else if (destinationFolder == DestinationFolder.PERSISTENT_DATA_PATH)
            {
                // #if UNITY_EDITOR || UNITY_STANDALONE
                // path = Application.persistentDataPath + "/" + customPath;
                // #elif UNITY_ANDROID
                // 				path = AndroidUtils.GetFirstAvailableMediaStorage()  + "/" + customPath;
                // #else 
                path = Application.persistentDataPath + "/" + customPath;
                // #endif
            }
            else if (destinationFolder == DestinationFolder.DATA_PATH)
            {
                path = Application.dataPath + "/" + customPath;
            }
            else if (destinationFolder == DestinationFolder.PICTURES_FOLDER)
            {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WSA || UNITY_WSA_10_0
                path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures) + "/" + customPath;
                // #elif UNITY_ANDROID
                // 				path = AndroidUtils.GetExternalPictureDirectory()  + "/" + customPath;
                // #elif UNITY_IOS
                // 				path = Application.persistentDataPath + "/" +customPath;
#else
						path = Application.persistentDataPath + "/" +customPath;
#endif
            }
#endif

            // Add a slash if not already at the end of the folder name
            if (path.Length > 0)
            {
                path = path.Replace("//", "/");
                if (path[path.Length - 1] != '/' && path[path.Length - 1] != '\\')
                {
                    path += "/";
                }
            }

            return path;
        }


        /// <summary>
        /// Returns the parsed screenshot filename using the symbols, extensions and special folders.
        /// </summary>
        public static string ParseFileName(string screenshotName, ScreenshotResolution resolution, DestinationFolder destination, string customPath, TextureExporter.ImageFileFormat format, bool overwriteFiles, System.DateTime time, int frameNumberPadding)
        {
            string filename = "";

#if UNITY_EDITOR || !UNITY_WEBGL
            // Destination Folder can not be parsed in webgl
            filename += ParsePath(destination, customPath);
#endif

            // File name
            filename += ParseSymbols(screenshotName, resolution, time, frameNumberPadding);

            // Get the file extension
            filename += "." + ParseExtension(format);


            // Increment the file number if a file already exist
            if (!overwriteFiles)
            {
                return PathUtils.PreventOverwrite(filename, frameNumberPadding);
            }

            return filename;
        }

    }
}
