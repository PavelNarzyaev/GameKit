# Encryption Keys Editor Window

This window is used to manage encryption keys for local state. To open it, use `GameKit/Encryption Keys` in the Unity Editor menu.

> [!NOTE]
> Application startup is blocked when encryption keys are missing.

## How To Use

- If compatibility with existing saves is not required, generate new values.
- If a new environment needs to remain compatible with existing saves, copy the existing key and IV instead of generating new ones.

> [!WARNING]
> The editor window overwrites the local file immediately and does not create backups, history, or recovery points. For more information about the limitations and risks of the local state encryption system, see [Local State Encryption](local-state-encryption.md).
