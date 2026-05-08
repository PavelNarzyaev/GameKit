# How to Start

## 1. Clone the Repository

Clone the repository to your local machine using Git.

```bash
git clone https://github.com/PavelNarzyaev/GameKit.git
```

## 2. Open the Project in Unity Hub

Open Unity Hub and add the cloned repository folder as a project.

If Unity Hub asks to install or select an editor version, use the version required by the project.

Open `Assets/_Project/Scenes/MainScene.unity`.

## 3. Generate Save Encryption Keys

After opening the project in Unity, generate local encryption keys for save files.

For more details, see [Encryption Keys Editor Window](encryption-keys-editor-window.md).

## 4. Optional: Windows Setup for AI Agents

Install `PowerShell 7` (`pwsh`) so the agent can use modern PowerShell syntax:

```powershell
winget install --id Microsoft.PowerShell --source winget
```

Install `ripgrep` (`rg`) so the agent can search the project quickly:

```powershell
winget install BurntSushi.ripgrep.MSVC
```

Make sure both `pwsh` and `rg` are available in `PATH`.

## 5. AGENTS.md

This project assumes that the repository remains public, so [AGENTS.md](../../../AGENTS.md) tells agents not to add materials whose licenses do not allow redistribution in a public source repository. If you make your fork private, you can remove or adjust that rule for your own repository.
