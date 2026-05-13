using GameKit.Core.Contracts;
using GameKit.CurrentTime;
using GameKit.PlayerState.Contracts;
using GameKit.ProductionMode;
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
            Container.BindInterfacesAndSelfTo<ProductionModeProvider>().AsSingle();
            Container.Bind<ICurrentTimeSource>().To<FakeCurrentTimeSource>().AsSingle();
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
        public void Set_WhenJsonIsIncompatible_ThrowsWithoutSavingOrRaisingRefreshedFromJson()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            var refreshedFromJsonCalls = 0;
            playerStateProvider.RefreshedFromJson += () => refreshedFromJsonCalls++;

            Assert.That(() => playerStateProvider.Set("{}"), Throws.Exception);
            Assert.That(storage.SaveCalls, Is.EqualTo(0));
            Assert.That(refreshedFromJsonCalls, Is.EqualTo(0));
        }

        [UsedImplicitly]
        private class FakePlayerStateStorage : IPlayerStateStorage
        {
            public int LoadCalls { get; private set; }
            public int SaveCalls { get; private set; }

            public bool Exists()
            {
                return false;
            }

            public void Save(string stateJson)
            {
                SaveCalls++;
            }

            public string Load()
            {
                LoadCalls++;
                return string.Empty;
            }

            public void Delete()
            {
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
    }
}
