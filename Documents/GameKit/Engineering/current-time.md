# Current Time

## Product Article

- [Current Time](../Product/current-time.md)

## Responsibilities

Current Time exposes the current Unix timestamp in seconds to the rest of the project.

Consumers should depend on `CurrentTimeProvider` instead of calling `DateTimeOffset.UtcNow` directly. This keeps the application code independent from the concrete time source and makes tests easier to control.

## Current Implementation

`CurrentTimeProvider` delegates time retrieval to `ICurrentTimeSource`.

**Production binding**

```csharp
Container.Bind(typeof(IRealTimeSource), typeof(ICurrentTimeSource)).To<SystemUtcCurrentTimeSource>().AsSingle();
Container.BindInterfacesAndSelfTo<CurrentTimeProvider>().AsSingle();
```

In production, `SystemUtcCurrentTimeSource` returns `DateTimeOffset.UtcNow.ToUnixTimeSeconds()`, so the project uses the device's UTC clock directly.

**Non-production binding**

```csharp
Container.Bind<IRealTimeSource>().To<SystemUtcCurrentTimeSource>().AsSingle();
Container.BindInterfacesAndSelfTo<CurrentTimeProvider>().AsSingle();

Container.Bind<PlayerStateTimeOffsetGateway>().AsSingle();
Container.BindInterfacesAndSelfTo<TimeOffsetService>().AsSingle();
Container.Bind<ICurrentTimeSource>().To<TimeOffsetCurrentTimeSource>().AsSingle();
```

In non-production builds, `CurrentTimeInstaller` binds the real device UTC source as `IRealTimeSource`, and `DevelopmentInstaller` installs `TimeOffsetInstaller` to bind `ICurrentTimeSource` to `TimeOffsetCurrentTimeSource`. `TimeOffsetCurrentTimeSource` wraps the real source and adds `TimeOffsetService.OffsetSeconds`. The offset is stored in player state and can be changed from the [Debug Panel](../Product/debug-panel.md).

**Test binding**

```csharp
Container.Bind<ICurrentTimeSource>().To<FakeCurrentTimeSource>().AsSingle();
Container.Bind<CurrentTimeProvider>().AsSingle();
```

Use this binding to supply a fake time source in tests.

## Modules

### Core

`Core` defines the `ICurrentTimeSource` abstraction and the `UnixTimestampExtensions` helper.

### CurrentTime

`CurrentTime` contains the runtime provider and the default device-based UTC time source implementation.

### TimeOffset

`TimeOffset` contains debug-only support for offsetting the current time in non-production builds.
