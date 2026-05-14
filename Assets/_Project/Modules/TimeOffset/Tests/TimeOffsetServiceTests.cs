using GameKit.Core.Contracts;
using GameKit.CurrentTime;
using GameKit.PlayerState;
using GameKit.PlayerState.Contracts;
using GameKit.ProductionMode;
using GameKit.TimeOffset.Contracts;
using JetBrains.Annotations;
using NUnit.Framework;
using Zenject;

namespace GameKit.TimeOffset.Tests
{
    [TestFixture]
    public class TimeOffsetServiceTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            Container.Bind<IPlayerStateStorage>().To<FakePlayerStateStorage>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProductionModeProvider>().AsSingle();
            Container.Bind<ICurrentTimeSource>().WithId(CurrentTimeSourceIds.k_BaseCurrentTimeSource).To<FakeCurrentTimeSource>().AsSingle();
            Container.Bind<PlayerStateTimeOffsetGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeOffsetService>().AsSingle();
            Container.Bind<ICurrentTimeSource>().To<TimeOffsetCurrentTimeSource>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrentTimeProvider>().AsSingle();
            PlayerStateInstaller.InstallCore(Container);
        }

        [Test]
        public void AddSeconds_WhenDeltaIsApplied_UpdatesPlayerStateAndMarksStateDirty()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                TimeOffsetSeconds = 60
            });

            var timeOffsetService = Container.Resolve<TimeOffsetService>();

            timeOffsetService.AddSeconds(3600);

            Assert.That(playerStateProvider.Data.TimeOffsetSeconds, Is.EqualTo(3660));
            Assert.That(playerStateProvider.IsDirty, Is.True);
        }

        [Test]
        public void AddSeconds_WhenDeltaIsZero_DoesNotMarkStateDirtyOrRaiseChanged()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                TimeOffsetSeconds = 60
            });

            var timeOffsetService = Container.Resolve<TimeOffsetService>();
            var changedCalls = 0;
            timeOffsetService.Changed += () => changedCalls++;

            timeOffsetService.AddSeconds(0);

            Assert.That(playerStateProvider.Data.TimeOffsetSeconds, Is.EqualTo(60));
            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(changedCalls, Is.EqualTo(0));
        }

        [Test]
        public void Refresh_WhenPlayerStateIsInitializing_UsesZeroOffsetBeforeStateExists()
        {
            const long currentTimestamp = 1_735_689_600;
            var currentTimeSource = (FakeCurrentTimeSource)Container.ResolveId<ICurrentTimeSource>(CurrentTimeSourceIds.k_BaseCurrentTimeSource);
            currentTimeSource.SetTimestamp(currentTimestamp);

            var playerStateProvider = Container.Resolve<PlayerStateProvider>();

            playerStateProvider.Refresh();

            Assert.That(playerStateProvider.Data.FirstLaunchTimestamp, Is.EqualTo(currentTimestamp));
            Assert.That(playerStateProvider.Data.TimeOffsetSeconds, Is.EqualTo(0));
        }

        [Test]
        public void GetTimestamp_WhenOffsetIsStoredInPlayerState_ReturnsTimestampWithOffset()
        {
            const long currentTimestamp = 1_735_689_600;
            var currentTimeSource = (FakeCurrentTimeSource)Container.ResolveId<ICurrentTimeSource>(CurrentTimeSourceIds.k_BaseCurrentTimeSource);
            currentTimeSource.SetTimestamp(currentTimestamp);

            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                TimeOffsetSeconds = 3661
            });

            var currentTimeProvider = Container.Resolve<CurrentTimeProvider>();

            Assert.That(currentTimeProvider.GetTimestamp(), Is.EqualTo(currentTimestamp + 3661));
        }

        private static void SetCleanState(PlayerStateProvider playerStateProvider, PlayerStateDto state)
        {
            playerStateProvider.Replace(state);
            playerStateProvider.Save();
        }

        [UsedImplicitly]
        private class FakePlayerStateStorage : IPlayerStateStorage
        {
            public bool Exists()
            {
                return false;
            }

            public void Save(string stateJson)
            {
            }

            public string Load()
            {
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
