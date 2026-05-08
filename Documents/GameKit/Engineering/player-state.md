# Player State

## Product Article

- [Player State](../Product/player-state.md)

## Description

The runtime state is represented as `PlayerStateDto`, serialized as a JSON document.

## Modules

### PlayerState

`PlayerState` contains the DTOs, the provider, the storage abstractions, the encrypted file-based implementation, and the editor tooling for encryption keys.

### StateClipboardProxy

`StateClipboardProxy` provides debug-only copy and paste operations for the serialized player state.

### UiStateDebugPage

`UiStateDebugPage` exposes debug panel actions for viewing basic state fields, applying clipboard state, and resetting the current state.

## Related

- [Local State Encryption](local-state-encryption.md)
- [Encryption Keys Editor Window](encryption-keys-editor-window.md)
