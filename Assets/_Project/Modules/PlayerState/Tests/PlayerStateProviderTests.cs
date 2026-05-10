using GameKit.Core;
using GameKit.CurrentTime;
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
            Container.BindInterfacesAndSelfTo<PlayerStateProvider>().AsSingle();
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

        [UsedImplicitly]
        private class FakePlayerStateStorage : IPlayerStateStorage
        {
            public int LoadCalls { get; private set; }

            public bool Exists()
            {
                return false;
            }

            public void Save(string stateJson)
            {
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
    }
}
