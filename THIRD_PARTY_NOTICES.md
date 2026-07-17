# Third-Party Notices

This file is a working inventory of third-party materials currently present in or referenced by the project.

The root [LICENSE](LICENSE) applies only to materials authored by the GameKit project. Third-party materials remain subject to their own licenses and terms.

## Vendored Code And Assets

| Material | Location | License or terms reference |
| --- | --- | --- |
| Extenject / Zenject | `Assets/Plugins/Zenject` | MIT License. See [`Assets/Plugins/Zenject/LICENSE.txt`](Assets/Plugins/Zenject/LICENSE.txt). Upstream: https://github.com/modesttree/Zenject. |
| UniClipboard | `Assets/UniClipboard` | MIT License. See [`Assets/UniClipboard/LICENSE.md`](Assets/UniClipboard/LICENSE.md). Upstream: https://github.com/sanukin39/UniClipboard. |
| Background images | `Assets/_Project/Images/Backgrounds` | AI-generated images created with ChatGPT Images 2.0. Subject to OpenAI terms and policies: https://openai.com/policies/row-terms-of-use/, https://openai.com/policies/usage-policies/. |
| Debug panel images | `Assets/_Project/Images/DebugPanel` | AI-generated images created with ChatGPT Images 2.0. Subject to OpenAI terms and policies: https://openai.com/policies/row-terms-of-use/, https://openai.com/policies/usage-policies/. |
| BackgroundMusic.mp3 | `Assets/_Project/Audio/BackgroundMusic.mp3` | AI-generated audio created with Suno. Subject to Suno terms: https://suno.com/terms. |
| Click.mp3 | `Assets/_Project/Audio/Click/Click.mp3` | Derived from Kenney UI Audio, renamed and modified. Creative Commons Zero (CC0). See [`Assets/_Project/Audio/Click/License.txt`](Assets/_Project/Audio/Click/License.txt). Upstream: https://kenney.nl/assets/ui-audio. |

## NuGet Packages

NuGet package dependencies are declared in [`Assets/packages.config`](Assets/packages.config) and restored under `Assets/Packages`.

| Material | Location | License or terms reference |
| --- | --- | --- |
| R3 | `Assets/Packages/R3.1.3.1` | MIT License. See [`Assets/Packages/R3.1.3.1/R3.nuspec`](Assets/Packages/R3.1.3.1/R3.nuspec). Upstream: https://github.com/Cysharp/R3. |
| Microsoft.Bcl.AsyncInterfaces | `Assets/Packages/Microsoft.Bcl.AsyncInterfaces.6.0.0` | MIT License. See [`Assets/Packages/Microsoft.Bcl.AsyncInterfaces.6.0.0/LICENSE.TXT`](Assets/Packages/Microsoft.Bcl.AsyncInterfaces.6.0.0/LICENSE.TXT). Upstream: https://github.com/dotnet/runtime. |
| Microsoft.Bcl.TimeProvider | `Assets/Packages/Microsoft.Bcl.TimeProvider.8.0.0` | MIT License. See [`Assets/Packages/Microsoft.Bcl.TimeProvider.8.0.0/LICENSE.TXT`](Assets/Packages/Microsoft.Bcl.TimeProvider.8.0.0/LICENSE.TXT). Upstream: https://github.com/dotnet/runtime. |
| System.ComponentModel.Annotations | `Assets/Packages/System.ComponentModel.Annotations.5.0.0` | MIT License. See [`Assets/Packages/System.ComponentModel.Annotations.5.0.0/LICENSE.TXT`](Assets/Packages/System.ComponentModel.Annotations.5.0.0/LICENSE.TXT). Upstream: https://github.com/dotnet/runtime. |
| System.Threading.Channels | `Assets/Packages/System.Threading.Channels.8.0.0` | MIT License. See [`Assets/Packages/System.Threading.Channels.8.0.0/LICENSE.TXT`](Assets/Packages/System.Threading.Channels.8.0.0/LICENSE.TXT). Upstream: https://github.com/dotnet/runtime. |

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

- `com.github-glitchenzo.nugetforunity` - MIT License. Upstream: https://github.com/GlitchEnzo/NuGetForUnity. License file is restored by Unity under `Library/PackageCache/com.github-glitchenzo.nugetforunity@*/LICENSE.md`.
- `com.gamelovers.mcp-unity` - MIT License. Upstream: https://github.com/CoderGamester/mcp-unity. License file is restored by Unity under `Library/PackageCache/com.gamelovers.mcp-unity@*/LICENSE.md`.
- `com.unity.adaptiveperformance`
- `com.unity.adaptiveperformance.google.android`
- `com.unity.addressables`
- `com.unity.collab-proxy`
- `com.unity.device-simulator.devices`
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
