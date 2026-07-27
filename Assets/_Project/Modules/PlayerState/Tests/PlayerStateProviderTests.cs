using System;
using GameKit.Core.Contracts;
using GameKit.CurrentTime;
using GameKit.PlayerState.Contracts;
using GameKit.ProductionMode.Contracts;
using JetBrains.Annotations;
using NUnit.Framework;
using Zenject;

namespace GameKit.PlayerState.Tests
{
    [TestFixture]
    public class PlayerStateProviderTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            Container.Bind<IPlayerStateStorage>().To<FakePlayerStateStorage>().AsSingle();
            Container.Bind<IProductionModeProvider>().To<FakeProductionModeProvider>().AsSingle();
            Container.Bind(typeof(ICurrentTimeSource), typeof(IRealTimeSource)).To<FakeCurrentTimeSource>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrentTimeProvider>().AsSingle();
            PlayerStateInstaller.InstallCore(Container);
        }

        [Test]
        public void Refresh_WhenStorageIsEmpty_InitializesState()
        {
            const long currentTimestamp = 1_735_689_600;
            var currentTimeSource = (FakeCurrentTimeSource)Container.Resolve<ICurrentTimeSource>();
            currentTimeSource.SetTimestamp(currentTimestamp);

            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();

            playerStateProvider.Refresh();

            Assert.That(playerStateProvider.UserId, Is.Not.Empty);
            Assert.That(playerStateProvider.FirstLaunchTimestamp, Is.EqualTo(currentTimestamp));
            Assert.That(playerStateProvider.IsDirty, Is.True);
            Assert.That(storage.LoadCalls, Is.EqualTo(0));
        }

        [Test]
        public void Refresh_WhenStorageIsEmpty_CreatesStateThroughFactory()
        {
            Container.Rebind<IPlayerStateFactory>().To<FakePlayerStateFactory>().AsSingle();

            var playerStateProvider = Container.Resolve<PlayerStateProvider>();

            playerStateProvider.Refresh();

            Assert.That(playerStateProvider.UserId, Is.EqualTo(FakePlayerStateFactory.k_UserId));
            Assert.That(playerStateProvider.FirstLaunchTimestamp, Is.EqualTo(FakePlayerStateFactory.k_FirstLaunchTimestamp));
            Assert.That(playerStateProvider.LaunchesCounter, Is.EqualTo(FakePlayerStateFactory.k_LaunchesCounter));
        }

        [Test]
        public void Refresh_WhenSavedStateIsValid_LoadsStateWithoutMarkingDirty()
        {
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            storage.SetStoredState(CreatePlayerState());
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            var replacedCalls = 0;
            playerStateProvider.Replaced += () => replacedCalls++;

            playerStateProvider.Refresh();

            Assert.That(playerStateProvider.UserId, Is.EqualTo("user-1"));
            Assert.That(playerStateProvider.FirstLaunchTimestamp, Is.EqualTo(123));
            Assert.That(playerStateProvider.SoftCurrency.CurrentValue, Is.EqualTo(7));
            Assert.That(playerStateProvider.HardCurrency.CurrentValue, Is.EqualTo(9));
            Assert.That(playerStateProvider.Energy.CurrentValue, Is.EqualTo(7));
            Assert.That(playerStateProvider.EnergyNextRestoreTimestamp.CurrentValue, Is.EqualTo(10));
            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(storage.LoadCalls, Is.EqualTo(1));
            Assert.That(storage.SaveCalls, Is.EqualTo(0));
            Assert.That(replacedCalls, Is.EqualTo(0));
        }

        [Test]
        public void IncrementLaunchesCounter_WhenCalled_UpdatesValueAndMarksStateDirtyWithoutRaisingReplaced()
        {
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            InitializeCleanState(playerStateProvider, storage);
            var replacedCalls = 0;
            playerStateProvider.Replaced += () => replacedCalls++;
            var launchesCounter = playerStateProvider.LaunchesCounter;

            playerStateProvider.IncrementLaunchesCounter();

            Assert.That(playerStateProvider.LaunchesCounter, Is.EqualTo(launchesCounter + 1));
            Assert.That(playerStateProvider.IsDirty, Is.True);
            Assert.That(replacedCalls, Is.EqualTo(0));
        }

        [Test]
        public void SetTimeOffsetSeconds_WhenValueIsSame_DoesNotMarkStateDirty()
        {
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            InitializeCleanState(playerStateProvider, storage);

            playerStateProvider.SetTimeOffsetSeconds(playerStateProvider.TimeOffsetSeconds.CurrentValue);

            Assert.That(playerStateProvider.IsDirty, Is.False);
        }

        [Test]
        public void ReplaceFromJson_WhenJsonIsValid_SavesAndRaisesReplaced()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            var replacedCalls = 0;
            playerStateProvider.Replaced += () => replacedCalls++;

            playerStateProvider.ReplaceFromJson(GetPlayerStateJson());

            Assert.That(playerStateProvider.UserId, Is.EqualTo("user-1"));
            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(storage.SaveCalls, Is.EqualTo(1));
            Assert.That(replacedCalls, Is.EqualTo(1));
        }

        [Test]
        public void ReplaceFromJson_WhenJsonIsIncompatible_ThrowsWithoutSavingOrRaisingEvents()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            var replacedCalls = 0;
            playerStateProvider.Replaced += () => replacedCalls++;

            Assert.That(() => playerStateProvider.ReplaceFromJson("{}"), Throws.Exception);
            Assert.That(storage.SaveCalls, Is.EqualTo(0));
            Assert.That(replacedCalls, Is.EqualTo(0));
        }

        [Test]
        public void Reset_WhenCalled_RecreatesSavesAndRaisesReplaced()
        {
            const long currentTimestamp = 1_735_689_600;
            var currentTimeSource = (FakeCurrentTimeSource)Container.Resolve<ICurrentTimeSource>();
            currentTimeSource.SetTimestamp(currentTimestamp);
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            storage.SetStoredState(CreatePlayerState());
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            var replacedCalls = 0;
            playerStateProvider.Replaced += () => replacedCalls++;

            playerStateProvider.Reset();

            Assert.That(playerStateProvider.UserId, Is.Not.Empty);
            Assert.That(playerStateProvider.FirstLaunchTimestamp, Is.EqualTo(currentTimestamp));
            Assert.That(playerStateProvider.LaunchesCounter, Is.EqualTo(1));
            Assert.That(playerStateProvider.SoftCurrency.CurrentValue, Is.EqualTo(100));
            Assert.That(playerStateProvider.HardCurrency.CurrentValue, Is.EqualTo(50));
            Assert.That(playerStateProvider.Energy.CurrentValue, Is.EqualTo(100));
            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(storage.DeleteCalls, Is.EqualTo(1));
            Assert.That(storage.SaveCalls, Is.EqualTo(1));
            Assert.That(replacedCalls, Is.EqualTo(1));
        }

        [Test]
        public void Refresh_WhenSavedStateIsInvalidAndProduction_Throws()
        {
            var productionModeProvider = (FakeProductionModeProvider)Container.Resolve<IProductionModeProvider>();
            productionModeProvider.SetIsProduction(true);
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            storage.SetStoredState(new PlayerStateDto());
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();

            Assert.That(() => playerStateProvider.Refresh(), Throws.Exception);
        }

        [Test]
        public void PlayerStateSavingController_WhenTickedAndStateIsDirty_SavesState()
        {
            var tickSource = new FakeGameTickSource();
            Container.Bind<IGameTickSource>().FromInstance(tickSource);
            PlayerStateInstaller.InstallAutoSave(Container);
            Container.Resolve<PlayerStateSavingController>();
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            playerStateProvider.Refresh();

            tickSource.Tick();

            Assert.That(storage.SaveCalls, Is.EqualTo(1));
            Assert.That(playerStateProvider.IsDirty, Is.False);
        }

        [Test]
        public void PlayerStateSavingController_WhenTickedAndStateIsClean_DoesNotSaveState()
        {
            var tickSource = new FakeGameTickSource();
            Container.Bind<IGameTickSource>().FromInstance(tickSource);
            PlayerStateInstaller.InstallAutoSave(Container);
            Container.Resolve<PlayerStateSavingController>();
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            storage.SetStoredState(CreatePlayerState());
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            playerStateProvider.Refresh();

            tickSource.Tick();

            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(storage.SaveCalls, Is.EqualTo(0));
        }

        [Test]
        public void PlayerStateSavingController_WhenDisposedBeforeTick_DoesNotSaveState()
        {
            var tickSource = new FakeGameTickSource();
            Container.Bind<IGameTickSource>().FromInstance(tickSource);
            PlayerStateInstaller.InstallAutoSave(Container);
            var savingController = Container.Resolve<PlayerStateSavingController>();
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            playerStateProvider.Refresh();

            savingController.Dispose();
            tickSource.Tick();

            Assert.That(playerStateProvider.IsDirty, Is.True);
            Assert.That(storage.SaveCalls, Is.EqualTo(0));
        }

        [Test]
        public void ExportJson_WhenStateExists_LoadsStoredStateAndSerializesJson()
        {
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            storage.SetStoredState(CreatePlayerState());
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();

            var json = playerStateProvider.ExportJson();
            var exportedState = new JsonPlayerStateSerializer().Deserialize(json);

            Assert.That(exportedState.UserId, Is.EqualTo("user-1"));
            Assert.That(exportedState.FirstLaunchTimestamp, Is.EqualTo(123));
            Assert.That(exportedState.Currencies.SoftCurrency, Is.EqualTo(7));
            Assert.That(exportedState.Currencies.HardCurrency, Is.EqualTo(9));
            Assert.That(exportedState.EnergyData.Energy, Is.EqualTo(7));
            Assert.That(exportedState.EnergyData.NextRestoreTimestamp, Is.EqualTo(10));
            Assert.That(storage.LoadCalls, Is.EqualTo(1));
        }

        [UsedImplicitly]
        private class FakePlayerStateStorage : IPlayerStateStorage
        {
            public int LoadCalls { get; private set; }
            public int SaveCalls { get; private set; }
            public int DeleteCalls { get; private set; }
            private PlayerStateDto m_storedState;

            public bool Exists()
            {
                return m_storedState != null;
            }

            public void Save(PlayerStateDto state)
            {
                SaveCalls++;
                m_storedState = state;
            }

            public PlayerStateDto Load()
            {
                LoadCalls++;
                return m_storedState;
            }

            public void Delete()
            {
                DeleteCalls++;
                m_storedState = null;
            }

            public void SetStoredState(PlayerStateDto storedState)
            {
                m_storedState = storedState;
            }
        }

        [UsedImplicitly]
        private class FakeCurrentTimeSource : ICurrentTimeSource, IRealTimeSource
        {
            private long m_currentTimestamp;

            public void SetTimestamp(long currentTimestamp)
            {
                m_currentTimestamp = currentTimestamp;
            }

            public long GetTimestamp()
            {
                return m_currentTimestamp;
            }
        }

        [UsedImplicitly]
        private class FakePlayerStateFactory : IPlayerStateFactory
        {
            public const string k_UserId = "factory-user";
            public const long k_FirstLaunchTimestamp = 123;
            public const int k_LaunchesCounter = 4;

            public PlayerStateDto Create()
            {
                return new PlayerStateDto(k_UserId, k_FirstLaunchTimestamp)
                {
                    LaunchesCounter = k_LaunchesCounter
                };
            }
        }

        [UsedImplicitly]
        private class FakeProductionModeProvider : IProductionModeProvider
        {
            public bool IsProduction { get; private set; }

            public void SetIsProduction(bool isProduction)
            {
                IsProduction = isProduction;
            }
        }

        [UsedImplicitly]
        private class FakeGameTickSource : IGameTickSource
        {
            public event Action Ticked;

            public void Tick()
            {
                Ticked?.Invoke();
            }
        }

        private static string GetPlayerStateJson()
        {
            return @"{
  ""userId"": ""user-1"",
  ""firstLaunchTimestamp"": 123,
  ""launchesCounter"": 1,
  ""currencies"": {
    ""softCurrency"": 7,
    ""hardCurrency"": 9
  },
  ""energyData"": {
    ""energy"": 7,
    ""nextRestoreTimestamp"": 10
  }
}";
        }

        private static PlayerStateDto CreatePlayerState()
        {
            return new PlayerStateDto("user-1", 123)
            {
                LaunchesCounter = 1,
                Currencies =
                {
                    SoftCurrency = 7,
                    HardCurrency = 9
                },
                EnergyData =
                {
                    Energy = 7,
                    NextRestoreTimestamp = 10
                }
            };
        }

        private static void InitializeCleanState(PlayerStateProvider playerStateProvider, FakePlayerStateStorage storage)
        {
            storage.SetStoredState(CreatePlayerState());
            playerStateProvider.Refresh();
        }
    }
}
