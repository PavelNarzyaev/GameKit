# Third-Party Notices

This file is a working inventory of third-party materials currently present in or referenced by the project.

The root [LICENSE](LICENSE) applies only to materials authored by the GameKit project. Third-party materials remain subject to their own licenses and terms.

## Vendored Code And Assets

| Material | Location | License or terms reference |
| --- | --- | --- |
| Extenject / Zenject | `Assets/Plugins/Zenject` | MIT License. See [`Assets/Plugins/Zenject/LICENSE.txt`](Assets/Plugins/Zenject/LICENSE.txt). Upstream: https://github.com/modesttree/Zenject. |
| UniClipboard | `Assets/UniClipboard` | MIT License. See [`Assets/UniClipboard/LICENSE.md`](Assets/UniClipboard/LICENSE.md). Upstream: https://github.com/sanukin39/UniClipboard. |

## Fonts And Text Assets

| Material | Location | License or terms reference |
| --- | --- | --- |
| Liberation Sans | `Assets/TextMesh Pro/Fonts/LiberationSans.ttf` | SIL Open Font License 1.1. See [`Assets/TextMesh Pro/Fonts/LiberationSans - OFL.txt`](Assets/TextMesh%20Pro/Fonts/LiberationSans%20-%20OFL.txt). |
| Roboto Mono | `Assets/_Project/Modules/UiFonts/Runtime/Fonts/Roboto_Mono` | SIL Open Font License 1.1. See [`Assets/_Project/Modules/UiFonts/Runtime/Fonts/Roboto_Mono/OFL.txt`](Assets/_Project/Modules/UiFonts/Runtime/Fonts/Roboto_Mono/OFL.txt). Upstream: https://github.com/googlefonts/RobotoMono. |

## Unity Package Imported Resources

| Material | Location | License or terms reference |
| --- | --- | --- |
| TextMesh Pro Essential Resources | `Assets/TextMesh Pro/Resources`, `Assets/TextMesh Pro/Shaders` | Unity Companion License. See https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/license/LICENSE.html. |

## Unity Packages

Direct Unity Package Manager dependencies are declared in [`Packages/manifest.json`](Packages/manifest.json).

Current direct non-module package dependencies:

- `com.unity.adaptiveperformance`
- `com.unity.adaptiveperformance.google.android`
- `com.unity.addressables`
- `com.unity.collab-proxy`
- `com.unity.feature.2d`
- `com.unity.feature.mobile`
- `com.unity.ide.rider`
- `com.unity.ide.visualstudio`
- `com.unity.inputsystem`
- `com.unity.multiplayer.center`
- `com.unity.nuget.newtonsoft-json`
- `com.unity.render-pipelines.universal`
- `com.unity.timeline`
- `com.unity.ugui`
- `com.unity.visualscripting`

Package-specific license files are restored by Unity under `Library/PackageCache`, which is not committed to this repository. Many Unity packages are licensed under the Unity Companion License for Unity-dependent projects: https://unity.com/legal/licenses/unity-companion-license.
