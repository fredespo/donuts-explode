using UnityEngine;
using UnityEngine.Events;

using System.IO;
using System.Linq;
using System.Collections.Generic;

#if (NET_4_6 || NET_STANDARD_2_0) && !UNITY_WEBGL
using System.Threading.Tasks;
#endif


namespace AlmostEngine.Screenshot
{
    public class TextureExporter
    {

        #region Saving

        public enum ImageFileFormat
        {
            PNG,
            JPG,
            TGA,
            // EXR
        }
        ;

        public static bool ExportToFile(Texture2D texture, string fullpath, ImageFileFormat imageFormat, int JPGQuality = 70, bool addToGallery = true, bool async = false, UnityAction onExportSuccess = null, UnityAction onExportFailure = null)
        {
            // Init callbacks if needed
            if (onExportSuccess == null)
            {
                onExportSuccess = () => { };
            }
            if (onExportFailure == null)
            {
                onExportFailure = () => { };
            }

#if UNITY_WEBPLAYER
            Debug.LogError("WebPlayer is not supported.");
            onExportFailure();
            return false;
#endif

            // Fails with empty texture   
            if (texture == null)
            {
                Debug.LogError("Can not export the texture to file " + fullpath + ", texture is empty.");
                onExportFailure();
                return false;
            }

            // Export to file
#if (NET_4_6 || NET_STANDARD_2_0) && UNITY_2019_3_OR_NEWER && !UNITY_WEBGL
#pragma warning disable 4014
            if (async)
            {
                EncodeAndExportToFileAsync(texture, fullpath, imageFormat, JPGQuality, addToGallery, onExportSuccess, onExportFailure);
                return true;
            }
#pragma warning restore 4014

#elif (NET_4_6 || NET_STANDARD_2_0) &&  !UNITY_2019_3_OR_NEWER && !UNITY_WEBGL
#pragma warning disable 4014
            if (async)
            {
                ExportToFileWriteAsync(texture, fullpath, imageFormat, JPGQuality, addToGallery, onExportSuccess, onExportFailure);
                return true;
            }
#pragma warning restore 4014
#endif

            return ExportToFileNoAsync(texture, fullpath, imageFormat, JPGQuality, addToGallery, async, onExportSuccess, onExportFailure);

        }


        // DEFAULT - NOT ASYNC
        static bool ExportToFileNoAsync(Texture2D texture, string fullpath, ImageFileFormat imageFormat, int JPGQuality = 70, bool addToGallery = true, bool async = false, UnityAction onExportSuccess = null, UnityAction onExportFailure = null)
        {
            // Encode texture to bytes
            byte[] encodedBytes = EncodeTextureToBytes(texture, imageFormat, JPGQuality);
            if (encodedBytes == null)
            {
                onExportFailure();
                return false;
            }

            // Export to file
            if (!WriteBytesToFile(encodedBytes, fullpath, imageFormat, JPGQuality))
            {
                onExportFailure();
                return false;
            }

            // Add to gallery
            if (addToGallery && !GalleryUtils.AddToGallery(fullpath))
            {
                onExportFailure();
                return false;
            }

            onExportSuccess();
            return true;
        }



        #endregion




        #region ASYNC PROCESSES



#if (NET_4_6 || NET_STANDARD_2_0) && UNITY_2019_3_OR_NEWER && !UNITY_WEBGL
        // FULL ASYNC - ENCODE AND WRITE
        public static async Task<bool> EncodeAndExportToFileAsync(Texture2D texture, string fullpath, ImageFileFormat imageFormat, int JPGQuality = 70, bool addToGallery = true, UnityAction onExportSuccess = null, UnityAction onExportFailure = null)
        {
            // Copy raw texture data
            var textureRawData = texture.GetRawTextureData();
            byte[] textureRawDataClone = textureRawData.ToArray();
            UnityEngine.Experimental.Rendering.GraphicsFormat format = texture.graphicsFormat;
            int width = texture.width;
            int height = texture.height;

            // Export thread
            var exportIsSuccess = await System.Threading.Tasks.Task.Run(() =>
            {
                Debug.Log("Starting fully threaded texture file export " + fullpath + " current thread ID : " + System.Threading.Thread.CurrentThread.ManagedThreadId);
                // System.Threading.Thread.Sleep(2000);

                // In 2019 and later we can use a thread safe encoding process
                byte[] encodedBytes = EncodeRawTextureToBytes(textureRawDataClone, format, width, height, imageFormat, JPGQuality);

                // Export to file
                return WriteBytesToFile(encodedBytes, fullpath, imageFormat, JPGQuality);
            });
            if (!exportIsSuccess)
            {
                onExportFailure();
                return false;
            }

            // Show in gallery
            if (addToGallery && !GalleryUtils.AddToGallery(fullpath))
            {
                onExportFailure();
                return false;
            }

            // Success
            onExportSuccess();
            return true;

        }
#endif


#if (NET_4_6 || NET_STANDARD_2_0) && !UNITY_WEBGL
        // PARTIAL ASYNC - WRITE ONLY
        public static async Task<bool> ExportToFileWriteAsync(Texture2D texture, string fullpath, ImageFileFormat imageFormat, int JPGQuality = 70, bool addToGallery = true, UnityAction onExportSuccess = null, UnityAction onExportFailure = null)
        {
            // Encode texture to bytes
            byte[] encodedBytes = EncodeTextureToBytes(texture, imageFormat, JPGQuality);
            if (encodedBytes == null)
            {
                onExportFailure();
                return false;
            }

            // Export to file thread
            var exportIsSuccess = await System.Threading.Tasks.Task.Run(() =>
            {
                Debug.Log("Starting partially threaded texture file export " + fullpath + " current thread ID : " + System.Threading.Thread.CurrentThread.ManagedThreadId);

                // Export to file
                return WriteBytesToFile(encodedBytes, fullpath, imageFormat, JPGQuality);
            });
            if (!exportIsSuccess)
            {
                onExportFailure();
                return false;
            }

            // Show in gallery
            if (addToGallery && !GalleryUtils.AddToGallery(fullpath))
            {
                onExportFailure();
                return false;
            }

            // Success
            onExportSuccess();
            return true;
        }
#endif

        #endregion



        #region ENCODING

        public static byte[] EncodeTextureToBytes(Texture2D texture, ImageFileFormat imageFormat, int JPGQuality = 70)
        {
            try
            {
                switch (imageFormat)
                {
                    case ImageFileFormat.JPG:
                        return texture.EncodeToJPG(JPGQuality);
#if UNITY_2018_1_OR_NEWER
                    case ImageFileFormat.TGA:
                        return texture.EncodeToTGA();
#endif
                    default:
                        return texture.EncodeToPNG();
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to encode the texture: " + e.Message);
            }
            return null;
        }



#if UNITY_2019_3_OR_NEWER
        public static byte[] EncodeRawTextureToBytes(byte[] rawTextureData, UnityEngine.Experimental.Rendering.GraphicsFormat format, int width, int height, ImageFileFormat imageFormat, int JPGQuality = 70)
        {
            byte[] bytes = null;
            try
            {
                switch (imageFormat)
                {
                    case ImageFileFormat.JPG:
                        bytes = ImageConversion.EncodeArrayToJPG(rawTextureData, format, (uint)width, (uint)height, 0, JPGQuality);
                        break;
#if UNITY_2018_1_OR_NEWER
                    case ImageFileFormat.TGA:
                        bytes = ImageConversion.EncodeArrayToTGA(rawTextureData, format, (uint)width, (uint)height);
                        break;
#endif
                    default:
                        bytes = ImageConversion.EncodeArrayToPNG(rawTextureData, format, (uint)width, (uint)height);
                        break;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to encode the texture: " + e.Message);
                return null;
            }
            return bytes;
        }
#endif

        #endregion


        #region WRITE TO FILE

        public static bool WriteBytesToFile(byte[] encodedBytes, string fullpath, ImageFileFormat imageFormat, int JPGQuality)
        {
#if !UNITY_EDITOR && UNITY_WEBGL

            // Create a downloadable image for the web browser
            try {
                string shortFileName = fullpath;
                int index = fullpath.LastIndexOf('/');
                if (index >= 0) {
                    shortFileName = fullpath.Substring(index+1);
                }
                string format = ScreenshotNameParser.ParseExtension(imageFormat);
                WebGLUtils.ExportImage(encodedBytes, shortFileName, format);
            }
            catch (System.Exception e)
            {
                Debug.LogError ("Failed to create downloadable image: " + e.Message) ;
                return false;
            }
            return true;
#endif

            // Create the directory
            if (!PathUtils.CreateExportDirectory(fullpath))
            {
                Debug.LogError("Could not create the export directory : " + fullpath);
                return false;
            }

            // Export the image
            try
            {
                System.IO.File.WriteAllBytes(fullpath, encodedBytes);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to create the file : " + fullpath + "   " + e.Message);
                return false;
            }

            return true;
        }


        #endregion






        #region LOADING

        public static Texture2D LoadFromFile(string fullname)
        {
            if (!System.IO.File.Exists(fullname))
            {
                Debug.LogError("Can not load texture from file " + fullname + ", file does not exists.");
                return null;
            }

            byte[] bytes = System.IO.File.ReadAllBytes(fullname);
            Texture2D texture = new Texture2D(2, 2);
            if (!texture.LoadImage(bytes))
            {
                Debug.LogError("Failed to load the texture " + fullname + ".");
            }

            return texture;

        }

        [System.Serializable]
        public class ImageFile
        {
            public Texture2D m_Texture;
            public string m_Name;
            public string m_Fullname;
            public System.DateTime m_CreationDate;
        }

        public static List<ImageFile> LoadFromPath(string path)
        {

            List<ImageFile> images = new List<ImageFile>();

            if (!System.IO.Directory.Exists(path))
            {
                Debug.LogError("Can not load images from directory " + path + ", directory does not exists.");
                return images;
            }

            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Extension == ".jpg" || file.Extension == ".png")
                {

                    ImageFile item = new ImageFile();
                    item.m_Name = file.Name;
                    item.m_Fullname = file.FullName;
                    item.m_CreationDate = file.CreationTime;
                    item.m_Texture = TextureExporter.LoadFromFile(file.FullName);

                    images.Add(item);
                }
            }

            return images;
        }

        #endregion
    }
}
