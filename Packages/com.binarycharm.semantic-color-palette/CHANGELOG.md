# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.1] - 2023-02-13
### Changed
- Avoided use of the `??=` C# operator to prevent compilation errors in Unity
  2019.4.x
- Made `scn_SemanticColorPaletteDemo.unity` and `p_SCP_RuntimeManagerUI.prefab`
  compatible with Unity 2019.4.x (backported `TextMeshPro` layout settings)

## [1.1.0] - 2022-12-05
### Added
- Documentation - new page: "Packages Architecture"
### Changed
- Asset deployed to `Packages` directory (previously, went to `Assets`)
- Some directories renamed from `Release` to `Runtime` 
- Restructured the directory layout of the `Demo` sample (that must now be
  installed explicitly from the `Package Manager`)
- Documentation: minor changes related to the installation as `Package`
- Replaced third party images (spinner animation) with our own version

## [1.0.2] - 2022-11-01
### Fixed
- `SCP_RuntimeManager.SetSystemRunning`: bugfix (palette providers not properly
  disabled)
- `SCP_RuntimeManager`: fixed wrong access modifiers
### Added
- Documentation - new manual page: "Performance Considerations"
- Added new utility behaviour: `SCP_EditModeOnly`, to have the system running
  only at edit time
### Changed
- Colorers: improved performances (avoided some unneeded struct copying with no
  public API change)
- RuntimeManagerUI: improved performances (UI objects caching)
- Improved changelog format

## [1.0.1] - 2022-10-17
### Fixed
- Adjusted directory structure to match namespaces (BinaryCharm.Samples.UI)
### Added
- Documentation - new man pages: "Understanding Colorers", "Importing Palettes"

## [1.0.0] - 2022-10-10
- Initial release of BinaryCharm.SemanticColorPalette
