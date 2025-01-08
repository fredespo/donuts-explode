using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gley.CrossPromo.Internal
{
    public class FileLoader : MonoBehaviour
    {
        private UnityAction<string> CompleteMethod;
        private string error;


        /// <summary>
        /// Loads settings file
        /// </summary>
        /// <param name="url">link to config file</param>
        /// <param name="CompleteMethod"></param>
        public void LoadCrossPromo(string url, UnityAction<string> CompleteMethod)
        {
            error = null;
            this.CompleteMethod = CompleteMethod;
            StartCoroutine(LoadFile(url));
        }


        /// <summary>
        /// Load all data from file
        /// </summary>
        /// <param name="url">link to config file</param>
        /// <returns></returns>
        private IEnumerator LoadFile(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                error = "External file url is null or empty";
            }
            else
            {

                UnityWebRequest www = UnityWebRequest.Get(url);
                yield return www.SendWebRequest();
#if UNITY_2020_1_OR_NEWER
            if (www.result != UnityWebRequest.Result.ConnectionError)
#else
                if (!www.isNetworkError && !www.isHttpError)
#endif
                {
                    string imageUrl = ReadFileContent(www.downloadHandler.text);
                    yield return StartCoroutine(LoadImage(imageUrl));
                }
                else
                {
                    error = "Could not download cross promo file " + www.error;
                }

            }

            if (CompleteMethod != null)
            {
                CompleteMethod(error);
                CompleteMethod = null;
            }

            Destroy(this);
        }


        /// <summary>
        /// Load and store an image
        /// </summary>
        /// <param name="url">image url</param>
        /// <returns></returns>
        private IEnumerator LoadImage(string url)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();
#if UNITY_2020_1_OR_NEWER
            if (www.result != UnityWebRequest.Result.ConnectionError)
#else
            if (!www.isNetworkError && !www.isHttpError)
#endif
            {
                Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                if (texture)
                {
                    SaveValues.SaveTexture(texture);
                    string[] path = url.Split('/');
                    string pictureName = path[path.Length - 1];
                    SaveValues.SavePictureName(pictureName);
                }

                else
                {
                    error = "Texture is null";
                }
            }
            else
            {
                error = "Could not download cross promo image " + www.error;
            }
        }


        /// <summary>
        /// Convert from JSON to class object
        /// </summary>
        /// <param name="text">json</param>
        /// <returns></returns>
        private string ReadFileContent(string text)
        {
            try
            {
                PromoFile promoFile = JsonUtility.FromJson<PromoFile>(text);
                SaveValues.SaveGameName(promoFile.gameName);
                SaveValues.SaveURL(promoFile.storeLink);
                return promoFile.imageUrls[Random.Range(0, promoFile.imageUrls.Count)];
            }
            catch
            {
                error = "Could not parse config file";
                return null;
            }
        }
    }
}
