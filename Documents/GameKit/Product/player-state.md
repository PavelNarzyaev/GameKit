# Player State

Player State defines the user data that the game keeps between sessions, such as progress, resources, and other runtime values tied to the current player.

In non-[production](production-mode.md) builds, the [Debug Panel](debug-panel.md) includes a state page with tools for working with the current player state.

## Storage Approaches

### Local Storage

The current implementation stores player state on the device.

**Advantages:**
- Works without an internet connection
- Is available immediately during application startup
- Is simple to set up for prototypes, MVPs, and small games

**Disadvantages:**
- Progress is tied to a single device
- Reinstalling the game can reset progress
- Shared progression between devices is not supported

### Server Storage

Server storage keeps player state in a backend associated with the player account or profile.

**Advantages:**
- Makes it possible to keep progress across devices
- Improves reliability when the player changes or loses a device
- Supports account-based progression

**Disadvantages:**
- Requires backend infrastructure
- Requires network access for synchronization
- Adds implementation and maintenance cost

## Technical Article

- [Player State](../Engineering/player-state.md)
