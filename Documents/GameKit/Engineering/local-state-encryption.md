# Local State Encryption

The purpose of the save file encryption system is to provide a minimal level of protection against the simplest form of cheating: editing local files in a text editor and changing the game state outside the game itself.

> [!WARNING]
> The current solution is suitable only for basic protection against data tampering. It is not suitable for protecting valuable secrets or providing strong security guarantees.

The encryption keys are included in the final build, which means the system remains vulnerable to reverse engineering. In other words, an experienced user can still decrypt and modify save files.

It is also important to note that protection against transferring a save file from one device to another has not yet been implemented.

> [!WARNING]
> Save encryption keys are stored locally in the project on the developer's machine, but they are excluded from git. This means you must create a backup of them before the first release, or remove `EncryptionKeys.Generated` from `.gitignore` if your repository is private.
>
> Also note that the project currently does not support key rotation. Keys used in the first release must not be changed afterward.
>
> If you release a new version of the application with different keys, existing user save files will no be readable.

## Related

- [Encryption Keys Editor Window](encryption-keys-editor-window.md)
