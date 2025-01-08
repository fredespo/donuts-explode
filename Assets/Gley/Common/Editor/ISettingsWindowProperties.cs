namespace Gley.Common
{
    public interface ISettingsWindowProperties
    {
        string VersionFilePath { get; }
        string WindowName { get;}
        int MinWidth { get;}
        int MinHeight { get;}

        string FolderName { get; }
        string ParentFolder { get; }
    }
}
