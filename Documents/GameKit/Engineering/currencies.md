# Currencies

## Product Article

- [Currencies](../Product/currencies.md)

## Description

The current implementation provides two currency types through `CurrencyType`: `Soft` and `Hard`.

The runtime values are stored in `PlayerCurrenciesDto` as part of [Player State](../Product/player-state.md).

## Modules

### Currencies

`Currencies` contains the wallet interface, the service implementation, the currency type enum, and the gateway that reads and writes currency values in [Player State](../Product/player-state.md).

### UiCurrenciesDebugPage

`UiCurrenciesDebugPage` exposes debug panel controls for viewing and changing the current currency values.
