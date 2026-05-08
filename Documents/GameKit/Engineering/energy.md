# Energy

## Product Article

- [Energy](../Product/energy.md)

## Description

Gameplay code works with `IEnergyService`. The runtime state is stored in [Player State](../Product/player-state.md) through `PlayerStateEnergyGateway` and `PlayerEnergyDataDto`.

## Configuration

The restoration behavior is defined by `IEnergyConfig`:

- `OneEnergyRestorationSeconds`
- `EnergyRestorationLimit`

In the current project, `MainConfig` implements `IEnergyConfig`, and `GameSettingsInstaller` binds that config instance into Zenject:

```csharp
Container.Bind(typeof(MainConfig), typeof(IEnergyConfig)).FromInstance(mainConfig);
```

`EnergyService` receives `IEnergyConfig` through injection and uses it to calculate restoration intervals and the maximum value that can be restored automatically.

## Current Implementation

The runtime bindings are installed in `MainInstaller`:

```csharp
Container.Bind<PlayerStateEnergyGateway>().AsSingle();
Container.BindInterfacesAndSelfTo<EnergyService>().AsSingle();
Container.Bind<EnergyRestorationController>().AsSingle().NonLazy();
```

`EnergyService` supports spending energy, adding energy, returning the restoration timer, and processing pending restoration.

The service uses `CurrentTimeProvider` to compare the current timestamp with `NextRestoreTimestamp`. If enough time has passed, it restores one or more energy points until it reaches `EnergyRestorationLimit`.

`EnergyRestorationController` subscribes to `IGameTickSource` and calls `IEnergyService.ProcessPendingRestoration()` on each tick so restoration is applied during runtime.

## Modules

### Energy

`Energy` contains `IEnergyConfig`, `IEnergyService`, `EnergyService`, `EnergyRestorationController`, and `PlayerStateEnergyGateway`.

### UiEnergyDebugPage

`UiEnergyDebugPage` exposes debug panel controls for viewing the current energy, adding or spending energy, and inspecting restoration settings in non-production builds.
