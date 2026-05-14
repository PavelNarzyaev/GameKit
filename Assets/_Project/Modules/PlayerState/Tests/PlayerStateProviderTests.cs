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
            Container.Bind<ICurrentTimeSource>().To<FakeCurrentTimeSource>().AsSingle();
            Container.Bind<IGameTickSource>().To<FakeGameTickSource>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrentTimeProvider>().AsSingle();
            PlayerStateInstaller.Install(Container);
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

            Assert.That(playerStateProvider.Data, Is.Not.Null);
            Assert.That(playerStateProvider.Data.UserId, Is.Not.Empty);
            Assert.That(playerStateProvider.Data.FirstLaunchTimestamp, Is.EqualTo(currentTimestamp));
            Assert.That(playerStateProvider.IsDirty, Is.True);
            Assert.That(storage.LoadCalls, Is.EqualTo(0));
        }

        [Test]
        public void Refresh_WhenStorageIsEmpty_CreatesStateThroughFactory()
        {
            Container.Rebind<IPlayerStateFactory>().To<FakePlayerStateFactory>().AsSingle();

            var playerStateProvider = Container.Resolve<PlayerStateProvider>();

            playerStateProvider.Refresh();

            Assert.That(playerStateProvider.Data.UserId, Is.EqualTo(FakePlayerStateFactory.k_UserId));
            Assert.That(playerStateProvider.Data.FirstLaunchTimestamp, Is.EqualTo(FakePlayerStateFactory.k_FirstLaunchTimestamp));
            Assert.That(playerStateProvider.Data.LaunchesCounter, Is.EqualTo(FakePlayerStateFactory.k_LaunchesCounter));
        }

        [Test]
        public void Edit_WhenActionCompletes_MarksStateDirtyWithoutRaisingReplaced()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            playerStateProvider.Replace(new PlayerStateDto());
            playerStateProvider.Save();
            var replacedCalls = 0;
            playerStateProvider.Replaced += () => replacedCalls++;

            playerStateProvider.Edit(state => state.LaunchesCounter++);

            Assert.That(playerStateProvider.IsDirty, Is.True);
            Assert.That(replacedCalls, Is.EqualTo(0));
        }

        [Test]
        public void Edit_WhenActionThrows_DoesNotMarkStateDirty()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            playerStateProvider.Replace(new PlayerStateDto());
            playerStateProvider.Save();

            Assert.That(
                () => playerStateProvider.Edit(_ => throw new InvalidOperationException("edit failed")),
                Throws.TypeOf<InvalidOperationException>());
            Assert.That(playerStateProvider.IsDirty, Is.False);
        }

        [Test]
        public void Replace_ReplacesStateMarksDirtyAndRaisesReplaced()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            var replacedCalls = 0;
            playerStateProvider.Replaced += () => replacedCalls++;

            playerStateProvider.Replace(new PlayerStateDto
            {
                UserId = "replaced-user"
            });

            Assert.That(playerStateProvider.Data.UserId, Is.EqualTo("replaced-user"));
            Assert.That(playerStateProvider.IsDirty, Is.True);
            Assert.That(replacedCalls, Is.EqualTo(1));
        }

        [Test]
        public void ReplaceFromJson_WhenJsonIsValid_SavesAndRaisesReplaced()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            var replacedCalls = 0;
            playerStateProvider.Replaced += () => replacedCalls++;

            playerStateProvider.ReplaceFromJson(GetPlayerStateJson());

            Assert.That(playerStateProvider.Data.UserId, Is.EqualTo("user-1"));
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
        public void Refresh_WhenSavedStateIsInvalidAndProduction_Throws()
        {
            var productionModeProvider = (FakeProductionModeProvider)Container.Resolve<IProductionModeProvider>();
            productionModeProvider.SetIsProduction(true);
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            storage.SetStoredState("{}");
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();

            Assert.That(() => playerStateProvider.Refresh(), Throws.Exception);
        }

        [Test]
        public void PlayerStateSavingController_WhenTickedAndStateIsDirty_SavesState()
        {
            var tickSource = (FakeGameTickSource)Container.Resolve<IGameTickSource>();
            Container.Resolve<PlayerStateSavingController>();
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            playerStateProvider.Replace(new PlayerStateDto());

            tickSource.Tick();

            Assert.That(storage.SaveCalls, Is.EqualTo(1));
            Assert.That(playerStateProvider.IsDirty, Is.False);
        }

        [UsedImplicitly]
        private class FakePlayerStateStorage : IPlayerStateStorage
        {
            public int LoadCalls { get; private set; }
            public int SaveCalls { get; private set; }
            private string m_storedState;

            public bool Exists()
            {
                return m_storedState != null;
            }

            public void Save(string stateJson)
            {
                SaveCalls++;
            }

            public string Load()
            {
                LoadCalls++;
                return m_storedState;
            }

            public void Delete()
            {
            }

            public void SetStoredState(string storedState)
            {
                m_storedState = storedState;
            }
        }

        [UsedImplicitly]
        private class FakeCurrentTimeSource : ICurrentTimeSource
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

        private class FakePlayerStateFactory : IPlayerStateFactory
        {
            public const string k_UserId = "factory-user";
            public const long k_FirstLaunchTimestamp = 123;
            public const int k_LaunchesCounter = 4;

            public PlayerStateDto Create()
            {
                return new PlayerStateDto
                {
                    UserId = k_UserId,
                    FirstLaunchTimestamp = k_FirstLaunchTimestamp,
                    LaunchesCounter = k_LaunchesCounter
                };
            }
        }

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
  ""launchesCounter"": 0,
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
    }
}
