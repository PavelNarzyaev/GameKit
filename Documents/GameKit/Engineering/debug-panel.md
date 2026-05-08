# Debug Panel

## Product Article

- [Debug Panel](../Product/debug-panel.md)

## How It Works

During startup, the application checks [Production Mode](production-mode.md). If the game is not in production mode, the startup flow activates the debug panel regions and shows the tab bar.

The system uses [UiRegions](ui-regions.md) to host two separate regions:

- the tab bar region;
- the debug page region.

## Boundaries

- The system depends on [Production Mode](production-mode.md) to decide whether the panel should exist in the running application.
- The system depends on [UiRegions](ui-regions.md) to place the tab bar and the currently open page into dedicated UI regions.
- Individual debug pages depend on feature modules.

## Modules

### UiDebugPanelTabBar

`UiDebugPanelTabBar` contains the bottom tab bar. It highlights the selected debug page and forwards user actions to `UiDebugPanel`.

### UiDebugPanel

`UiDebugPanel` contains the core debug pages navigation logic.

### UiStateDebugPage
### UiTimeDebugPage
### UiCurrenciesDebugPage
### UiEnergyDebugPage
