#if UNITY_WINRT && !UNITY_EDITOR
using UnityEngine.Windows;
#endif
using UnityEngine;
using UnityEngine.Events;


namespace Gley.AllPlatformsSave.Internal
{
    public class SaveManager
    {
        static ISaveClass saveMethod;

        static SaveManager instance;
        public static SaveManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SaveManager();
                    AddRequiredScript();
                }

                return instance;
            }
        }


        static void AddRequiredScript()
        {
#if JSONSerializationFileSave
            saveMethod = new JSONSerializationFileSave();
#endif

#if JSONSerializationPlayerPrefs
            saveMethod = new JSONSerializationPlayerPrefs();
#endif

#if BinarySerializationFileSave
            saveMethod = new BinarySerializationFileSave();
#endif

#if BinarySerializationPlayerPrefs
            saveMethod = new BinarySerializationPlayerPrefs();
#endif
        }


        /// <summary>
        /// Save the specified dataToSave, to fileName and encrypt it.
        /// </summary>
        /// <param name="dataToSave">Any type of serializable class</param>
        /// <param name="path">File name path.</param>
        /// <param name="encrypt">If set to <c>true</c> encrypt.</param>
        /// <param name="CompleteMethod">Called after all is done</param>
        public void Save<T>(T dataToSave, string path, UnityAction<SaveResult, string> CompleteMethod, bool encrypted) where T : class, new()
        {
            if (saveMethod == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Current platform (" + UnityEditor.EditorUserBuildSettings.activeBuildTarget + ") is not added to plugin settings. Go to Tools->Gley->All Platforms Save and add your current platform");
#else
             Debug.LogError("Current platform is not added to plugin settings. Go to Tools->Gley->All Platforms Save and add your current platform");
#endif
                return;
            }
            saveMethod.Save<T>(dataToSave, path, CompleteMethod, encrypted);
        }


        public void SaveString<T>(T dataToSave, UnityAction<SaveResult, string> CompleteMethod, bool encrypted) where T : class, new()
        {
            saveMethod.SaveString<T>(dataToSave, CompleteMethod, encrypted);
        }


        /// <summary>
        /// Load the specified fileName and decrypt it.
        /// Returns any type of serializable data
        /// If specified file does not exists, a new one is generated and the default values from serializable class are saved 
        /// </summary>
        /// <param name="path">File name.</param>
        /// <param name="CompleteMethod">Called after all is done</param>
        /// <param name="encrypted">If set to <c>true</c> encrypted.</param>
        public void Load<T>(string path, UnityAction<T, SaveResult, string> CompleteMethod, bool encrypted) where T : class, new()
        {
            if (saveMethod == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Current platform (" + UnityEditor.EditorUserBuildSettings.activeBuildTarget + ") is not added to plugin settings. Go to Tools->Gley->All Platforms Save and add your current platform");
#else
             Debug.LogError("Current platform is not added to plugin settings. Go to Tools->Gley->All Platforms Save and add your current platform");
#endif
                return;
            }
            saveMethod.Load<T>(path, CompleteMethod, encrypted);
        }


        public void LoadString<T>(string stringToLoad, UnityAction<T, SaveResult, string> LoadCompleteMethod, bool encrypted) where T : class, new()
        {
            saveMethod.LoadString<T>(stringToLoad, LoadCompleteMethod, encrypted);
        }


        public void ClearAllData(string path)
        {
            if (saveMethod == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Current platform (" + UnityEditor.EditorUserBuildSettings.activeBuildTarget + ") is not added to plugin settings. Go to Tools->Gley->All Platforms Save and add your current platform");
#else
             Debug.LogError("Current platform is not added to plugin settings. Go to Tools->Gley->All Platforms Save and add your current platform");
#endif
                return;
            }
            saveMethod.ClearAllData(path);
        }


        public void ClearFile(string path)
        {
            if (saveMethod == null)
            {
                Debug.LogError("Current platform is not added to plugin settings. Go to Tools->Gley->All Platforms Save and add your current platform");
                return;
            }
            saveMethod.ClearFile(path);
        }
    }
}


