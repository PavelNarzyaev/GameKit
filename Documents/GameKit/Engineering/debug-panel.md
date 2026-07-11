# Debug Panel

## Product Article

- [Debug Panel](../Product/debug-panel.md)

## How It Works

During startup, the application checks [Production Mode](production-mode.md). If the game is not in production mode, the startup flow activates the debug panel regions and shows the toolbar.

The system uses [UiRegions](ui-regions.md) to host three separate regions:

- the toolbar region;
- the debug page region;
- the debug message region.

## Boundaries

- The system depends on [Production Mode](production-mode.md) to decide whether the panel should exist in the running application.
- The system depends on [UiRegions](ui-regions.md) to place the toolbar, the currently open page, and debug messages into dedicated UI regions.
- Individual debug pages depend on feature modules.

## Modules

### UiDebugToolBar

`UiDebugToolBar` contains the bottom debug panel toolbar. Its page tabs select and highlight the active debug page, the logs indicator opens the Logs page and shows the current warning/error state, and the close button closes the active debug page.

### UiDebugPanel

`UiDebugPanel` contains the core debug pages navigation logic.

### UiStateDebugPage
### UiTimeDebugPage
### UiCurrenciesDebugPage
### UiEnergyDebugPage
### UiLogsDebugPage
