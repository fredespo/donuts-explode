using UnityEngine;
using AlmostEngine.Screenshot;


namespace AlmostEngine.Screenshot.Extra
{
    /// <summary>
    /// The SetScreenshotGalleryFolderPath component can be used to automatically set the ScreenshotGallery path.
    /// </summary>
    [RequireComponent(typeof(ScreenshotGallery))]
    public class SetScreenshotGalleryFolderPath : MonoBehaviour
    {
        [Tooltip("The screenshot manager to use as reference to get the screenshots folder path.")]
        public ScreenshotManager m_Manager;

        void Start()
        {
            if (m_Manager != null)
            {
                ScreenshotGallery gallery = GetComponent<ScreenshotGallery>();
                gallery.m_ScreenshotFolderPath = m_Manager.GetExportPath();
                // m_Manager.m_Config.m_DestinationFolder == ScreenshotNameParser.DestinationFolder.CUSTOM_FOLDER 
                // ? m_Manager.m_Config.m_RootedPath : m_Manager.m_Config.m_RelativePath;
                // gallery.m_DestinationFolder = m_Manager.m_Config.m_DestinationFolder;
                gallery.LoadImageFiles();
                gallery.UpdateGallery();
            }
        }
    }
}
