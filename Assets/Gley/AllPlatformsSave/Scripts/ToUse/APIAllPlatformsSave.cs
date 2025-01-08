using Gley.AllPlatformsSave.Internal;
using UnityEngine.Events;

namespace Gley.AllPlatformsSave
{
    public static class API
    {
        /// <summary>
        /// Save the specified dataToSave, to fileName and encrypt it.
        /// </summary>
        /// <typeparam name="T">Generic data type</typeparam>
        /// <param name="dataToSave">Any type of serializable class</param>
        /// <param name="path">File path (ex: Application.persistentDataPath + "/" + "LocalFileName")</param>
        /// <param name="completeMethod">Callback called after all is saved</param>
        /// <param name="encrypted">If set to true, data will be encrypted.</param>
        public static void Save<T>(T dataToSave, string path, UnityAction<SaveResult, string> completeMethod, bool encrypted) where T : class, new()
        {
            SaveManager.Instance.Save(dataToSave, path, completeMethod, encrypted);
        }


        /// <summary>
        /// Save the specified dataToSave, into a file and encrypt it.
        /// </summary>
        /// <typeparam name="T">Generic data type</typeparam>
        /// <param name="dataToSave">Any type of serializable class</param>
        /// <param name="completeMethod">Callback called after all is saved. The serialized saved string will be returned inside this method</param>
        /// <param name="encrypted">If set to true, data will be encrypted.</param>
        public static void SaveString<T>(T dataToSave, UnityAction<SaveResult, string> completeMethod, bool encrypted) where T : class, new()
        {
            SaveManager.Instance.SaveString(dataToSave, completeMethod, encrypted);
        }


        /// <summary>
        /// Load the specified fileName and decrypt it.
        /// If specified file does not exists, a new one is generated and the default values from serializable class are saved.
        /// </summary>
        /// <typeparam name="T">Generic data type to be loaded</typeparam>
        /// <param name="path">File path (ex: Application.persistentDataPath + "/" + "LocalFileName")</param>
        /// <param name="completeMethod">Callback in which saved data is returned as parameter</param>
        /// <param name="encrypted">If set to true, original data was encrypted. Needs to be identically with the one from the Save method</param>
        public static void Load<T>(string path, UnityAction<T, SaveResult, string> completeMethod, bool encrypted) where T : class, new()
        {
            SaveManager.Instance.Load(path, completeMethod, encrypted);
        }


        /// <summary>
        /// Deserialize data from the provided string and decrypt it.
        /// </summary>
        /// <typeparam name="T">Generic data type to be loaded</typeparam>
        /// <param name="stringToLoad">String that stores the saved values </param>
        /// <param name="completeMethod">Callback in which saved date is returned as parameter</param>
        /// <param name="encrypted">If set to true, original data was encrypted. Needs to be identically with the one from the Save method</param>
        public static void LoadString<T>(string stringToLoad, UnityAction<T, SaveResult, string> completeMethod, bool encrypted) where T : class, new()
        {
            SaveManager.Instance.LoadString(stringToLoad, completeMethod, encrypted);
        }


        /// <summary>
        /// Clear all files from a directory
        /// </summary>
        /// <param name="path">Directory path</param>
        public static void ClearAllData(string path)
        {
            SaveManager.Instance.ClearAllData(path);
        }


        /// <summary>
        /// Clear a single file
        /// </summary>
        /// <param name="path">File path</param>
        public static void ClearFile(string path)
        {
            SaveManager.Instance.ClearFile(path);
        }
    }
}