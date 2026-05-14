# How to Start

## 1. Create a Project Repository

Open the GameKit repository on GitHub:

https://github.com/PavelNarzyaev/GameKit

Select `Use this template` -> `Create a new repository`.

Enter the project name in `Repository name` and choose the repository visibility.

After GitHub creates the new repository, open its main page, select `Code`, and copy the clone URL.

## 2. Clone the Project

Clone the new repository to your local machine using Fork git client (`File` -> `Clone`).

## 3. Open the Project in Unity Hub

Open Unity Hub and add the cloned repository folder as a project (`Add` -> `Add project from disk`).

If Unity Hub asks to install or select an editor version, use the version required by the project.

When installing the editor version, include these modules:

- Android Build Support;
  - OpenJDK;
  - Android SDK & NDK Tools.

When selecting the editor version, you can choose Android as the target platform immediately.

Alternatively, open the project with the current platform first, then use `File` -> `Build Profiles` in Unity and switch the active build profile to Android.

Open `Assets/_Project/Scenes/MainScene.unity` in Unity Editor.

## 4. Generate Save Encryption Keys

After opening the project in Unity, generate local encryption keys for save files.

For more details, see [Encryption Keys Editor Window](encryption-keys-editor-window.md).

Application startup is blocked while encryption keys are missing.

## 5. Decide How to Store Generated Keys

Generated encryption keys are ignored by `Assets/_Project/Modules/PlayerState/.gitignore` so they are not committed to a public repository by accident.

If your new project repository is private and you want to store the generated keys in Git, remove `Assets/_Project/Modules/PlayerState/.gitignore` and commit that repository setup change.

## 6. Run the Project

Press Play in Unity with `Assets/_Project/Scenes/MainScene.unity` open.

If the project starts correctly, continue with the project-specific setup.

## 7. Update Project Settings

Open `Edit` -> `Project Settings` -> `Player` and update the project metadata:

- product name;
- version;
- company name;
- Android package name.

After that you can start adapting the project for the game.

## 8. Optional: Update AGENTS.md

This project assumes that the repository remains public, so [AGENTS.md](../../../AGENTS.md) tells agents not to add materials whose licenses do not allow redistribution in a public source repository. If you make your fork private, you can remove or adjust that rule for your own repository.

## 9. Optional: Include Workspace documents folder in git

`Documents/Workspace` stores local working notes and is ignored by `Documents/Workspace/.gitignore` by default.

If you want to keep these documents in Git for your project, remove or adjust `Documents/Workspace/.gitignore`, then commit the workspace documents that should be shared.

## 10. Optional: Windows Setup for AI Agents

Install `PowerShell 7` (`pwsh`) so the agent can use modern PowerShell syntax:

```powershell
winget install --id Microsoft.PowerShell --source winget
```

Install `ripgrep` (`rg`) so the agent can search the project quickly:

```powershell
winget install BurntSushi.ripgrep.MSVC
```

Make sure both `pwsh` and `rg` are available in `PATH`.
