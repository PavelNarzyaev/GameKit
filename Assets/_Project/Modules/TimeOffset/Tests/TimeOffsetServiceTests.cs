using GameKit.Core.Contracts;
using GameKit.CurrentTime;
using GameKit.PlayerState;
using GameKit.PlayerState.Contracts;
using GameKit.ProductionMode;
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
            Container.Bind<IRealTimeSource>().To<FakeCurrentTimeSource>().AsSingle();
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

            Assert.That(playerStateProvider.TimeOffsetSeconds.CurrentValue, Is.EqualTo(3660));
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

            Assert.That(playerStateProvider.TimeOffsetSeconds.CurrentValue, Is.EqualTo(60));
            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(changedCalls, Is.EqualTo(0));
        }

        [TestCase(int.MaxValue, 1)]
        [TestCase(int.MinValue, -1)]
        public void AddSeconds_WhenResultWouldOverflowOrUnderflow_DoesNotChangeStateOrRaiseChanged(
            int offsetSeconds,
            int deltaSeconds)
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                TimeOffsetSeconds = offsetSeconds
            });

            var timeOffsetService = Container.Resolve<TimeOffsetService>();
            var changedCalls = 0;
            timeOffsetService.Changed += () => changedCalls++;

            timeOffsetService.AddSeconds(deltaSeconds);

            Assert.That(playerStateProvider.TimeOffsetSeconds.CurrentValue, Is.EqualTo(offsetSeconds));
            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(changedCalls, Is.EqualTo(0));
        }

        [Test]
        public void Refresh_WhenPlayerStateIsInitializing_UsesZeroOffsetBeforeStateExists()
        {
            const long currentTimestamp = 1_735_689_600;
            var currentTimeSource = (FakeCurrentTimeSource)Container.Resolve<IRealTimeSource>();
            currentTimeSource.SetTimestamp(currentTimestamp);

            var playerStateProvider = Container.Resolve<PlayerStateProvider>();

            playerStateProvider.Refresh();

            Assert.That(playerStateProvider.FirstLaunchTimestamp, Is.EqualTo(currentTimestamp));
            Assert.That(playerStateProvider.TimeOffsetSeconds.CurrentValue, Is.EqualTo(0));
        }

        [Test]
        public void GetTimestamp_WhenOffsetIsStoredInPlayerState_ReturnsTimestampWithOffset()
        {
            const long currentTimestamp = 1_735_689_600;
            var currentTimeSource = (FakeCurrentTimeSource)Container.Resolve<IRealTimeSource>();
            currentTimeSource.SetTimestamp(currentTimestamp);

            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                TimeOffsetSeconds = 3661
            });

            var currentTimeProvider = Container.Resolve<CurrentTimeProvider>();

            Assert.That(currentTimeProvider.GetTimestamp(), Is.EqualTo(currentTimestamp + 3661));
        }

        private void SetCleanState(PlayerStateProvider playerStateProvider, PlayerStateDto state)
        {
            var validState = new PlayerStateDto("test-user", state.FirstLaunchTimestamp)
            {
                LaunchesCounter = 1,
                TimeOffsetSeconds = state.TimeOffsetSeconds
            };

            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            storage.Save(validState);
            playerStateProvider.Refresh();
        }

        [UsedImplicitly]
        private class FakePlayerStateStorage : IPlayerStateStorage
        {
            private PlayerStateDto m_storedState;

            public bool Exists()
            {
                return m_storedState != null;
            }

            public void Save(PlayerStateDto state)
            {
                m_storedState = state;
            }

            public PlayerStateDto Load()
            {
                return m_storedState;
            }

            public void Delete()
            {
            }
        }

        [UsedImplicitly]
        private class FakeCurrentTimeSource : IRealTimeSource
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
