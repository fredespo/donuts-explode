namespace Gley.AllPlatformsSave.Internal
{
    public enum SupportedBuildTargetGroup
    {
        // Summary:
        //     PC (Windows, Mac, Linux) target.
        Standalone = 1,

        // Summary:
        //     Apple iOS target.
        iOS = 4,

        // Summary:
        //     Android target.
        Android = 7,

        // Summary:
        //     WebGL.
        WebGL = 13,

        // Summary:
        //     Windows Store Apps target.
        WSA = 14,

        // Summary:
        //     Sony Playstation 4 target.
        PS4 = 19,

        // Summary:
        //     Microsoft Xbox One target.
        XboxOne = 21,

        // Summary:
        //     Apple's tvOS target.
        tvOS = 25,

        // Summary:
        //     Nintendo Switch target.
        Switch = 27,

        // Summary:
        //     LinuxHeadlessSimulation target.
        LinuxHeadlessSimulation = 30,
        GameCoreXboxSeries = 31,
        GameCoreXboxOne = 32,

        // Summary:
        //     Sony Playstation 5 target.
        PS5 = 33,
        EmbeddedLinux = 34,
    }
}