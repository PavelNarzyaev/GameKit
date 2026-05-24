using GameKit.Core.Contracts;
using GameKit.CurrentTime;
using GameKit.Energy.Contracts;
using GameKit.PlayerState;
using GameKit.PlayerState.Contracts;
using GameKit.ProductionMode;
using JetBrains.Annotations;
using NUnit.Framework;
using Zenject;

namespace GameKit.Energy.Tests
{
    [TestFixture]
    public class EnergyServiceTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            Container.Bind<IPlayerStateStorage>().To<FakePlayerStateStorage>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProductionModeProvider>().AsSingle();
            Container.Bind(typeof(ICurrentTimeSource), typeof(IRealTimeSource)).To<FakeCurrentTimeSource>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrentTimeProvider>().AsSingle();
            PlayerStateInstaller.InstallCore(Container);
            Container.Bind<IEnergyConfig>().To<FakeEnergyConfig>().AsSingle();
            Container.Bind<PlayerStateEnergyGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnergyService>().AsSingle();
        }

        [Test]
        public void TryAdd_WhenAmountIsPositive_UpdatesValueMarksStateDirtyAndRaisesChanged()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = 3,
                    NextRestoreTimestamp = 120
                }
            });

            var energyService = Container.Resolve<IEnergyService>();
            var changedCalls = 0;
            energyService.Changed += () => changedCalls++;

            var result = energyService.TryAdd(2);

            Assert.That(result, Is.True);
            Assert.That(energyService.Energy, Is.EqualTo(5));
            Assert.That(playerStateProvider.Data.EnergyData.NextRestoreTimestamp, Is.EqualTo(120));
            Assert.That(playerStateProvider.IsDirty, Is.True);
            Assert.That(changedCalls, Is.EqualTo(1));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void TryAdd_WhenAmountIsNotPositive_ReturnsFalseWithoutChangingState(int amount)
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = 5
                }
            });

            var energyService = Container.Resolve<IEnergyService>();
            var changedCalls = 0;
            energyService.Changed += () => changedCalls++;

            var result = energyService.TryAdd(amount);

            Assert.That(result, Is.False);
            Assert.That(energyService.Energy, Is.EqualTo(5));
            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(changedCalls, Is.EqualTo(0));
        }

        [Test]
        public void TryAdd_WhenAmountWouldReachRestorationLimit_ClearsNextRestoreTimestamp()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = 8,
                    NextRestoreTimestamp = 145
                }
            });

            var energyService = Container.Resolve<IEnergyService>();

            var result = energyService.TryAdd(2);

            Assert.That(result, Is.True);
            Assert.That(energyService.Energy, Is.EqualTo(10));
            Assert.That(playerStateProvider.Data.EnergyData.NextRestoreTimestamp, Is.EqualTo(0));
            Assert.That(energyService.IsRestorationInProgress, Is.False);
        }

        [Test]
        public void TryAdd_WhenAmountWouldOverflow_ReturnsFalseWithoutChangingState()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = int.MaxValue,
                    NextRestoreTimestamp = 145
                }
            });

            var energyService = Container.Resolve<IEnergyService>();
            var changedCalls = 0;
            energyService.Changed += () => changedCalls++;

            var result = energyService.TryAdd(1);

            Assert.That(result, Is.False);
            Assert.That(energyService.Energy, Is.EqualTo(int.MaxValue));
            Assert.That(playerStateProvider.Data.EnergyData.NextRestoreTimestamp, Is.EqualTo(145));
            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(changedCalls, Is.EqualTo(0));
        }

        [Test]
        public void TrySpend_WhenBalanceIsEnough_StartsRestorationMarksStateDirtyAndRaisesChanged()
        {
            const long currentTimestamp = 100;
            var currentTimeSource = (FakeCurrentTimeSource)Container.Resolve<ICurrentTimeSource>();
            currentTimeSource.SetTimestamp(currentTimestamp);

            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = 10
                }
            });

            var energyService = Container.Resolve<IEnergyService>();
            var changedCalls = 0;
            energyService.Changed += () => changedCalls++;

            var result = energyService.TrySpend(4);

            Assert.That(result, Is.True);
            Assert.That(energyService.Energy, Is.EqualTo(6));
            Assert.That(playerStateProvider.Data.EnergyData.NextRestoreTimestamp, Is.EqualTo(110));
            Assert.That(playerStateProvider.IsDirty, Is.True);
            Assert.That(changedCalls, Is.EqualTo(1));
            Assert.That(energyService.IsRestorationInProgress, Is.True);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(11)]
        public void TrySpend_WhenAmountIsInvalidOrBalanceIsInsufficient_ReturnsFalseWithoutChangingState(int amount)
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = 10
                }
            });

            var energyService = Container.Resolve<IEnergyService>();
            var changedCalls = 0;
            energyService.Changed += () => changedCalls++;

            var result = energyService.TrySpend(amount);

            Assert.That(result, Is.False);
            Assert.That(energyService.Energy, Is.EqualTo(10));
            Assert.That(playerStateProvider.Data.EnergyData.NextRestoreTimestamp, Is.EqualTo(0));
            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(changedCalls, Is.EqualTo(0));
        }

        [Test]
        public void GetRestorationTimer_WhenRestorationIsInProgress_ReturnsRemainingTimeUntilNextUnit()
        {
            const long currentTimestamp = 103;
            var currentTimeSource = (FakeCurrentTimeSource)Container.Resolve<ICurrentTimeSource>();
            currentTimeSource.SetTimestamp(currentTimestamp);

            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = 4,
                    NextRestoreTimestamp = 110
                }
            });

            var energyService = Container.Resolve<IEnergyService>();

            var timer = energyService.GetRestorationTimer();

            Assert.That(timer, Is.EqualTo(System.TimeSpan.FromSeconds(7)));
        }

        [Test]
        public void GetRestorationTimer_WhenEnergyIsAtLimit_ReturnsZero()
        {
            const long currentTimestamp = 103;
            var currentTimeSource = (FakeCurrentTimeSource)Container.Resolve<ICurrentTimeSource>();
            currentTimeSource.SetTimestamp(currentTimestamp);

            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = 10,
                    NextRestoreTimestamp = 110
                }
            });

            var energyService = Container.Resolve<IEnergyService>();

            var timer = energyService.GetRestorationTimer();

            Assert.That(timer, Is.EqualTo(System.TimeSpan.Zero));
        }

        [Test]
        public void GetRestorationTimer_WhenNextRestoreTimestampIsMissing_ReturnsFullStep()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = 4,
                    NextRestoreTimestamp = 0
                }
            });

            var energyService = Container.Resolve<IEnergyService>();

            var timer = energyService.GetRestorationTimer();

            Assert.That(timer, Is.EqualTo(System.TimeSpan.FromSeconds(10)));
        }

        [Test]
        public void ProcessPendingRestoration_WhenTimerIsNotScheduled_InitializesNextRestoreTimestamp()
        {
            const long currentTimestamp = 100;
            var currentTimeSource = (FakeCurrentTimeSource)Container.Resolve<ICurrentTimeSource>();
            currentTimeSource.SetTimestamp(currentTimestamp);

            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = 4,
                    NextRestoreTimestamp = 0
                }
            });

            var energyService = Container.Resolve<IEnergyService>();
            var changedCalls = 0;
            energyService.Changed += () => changedCalls++;

            energyService.ProcessPendingRestoration();

            Assert.That(energyService.Energy, Is.EqualTo(4));
            Assert.That(playerStateProvider.Data.EnergyData.NextRestoreTimestamp, Is.EqualTo(110));
            Assert.That(playerStateProvider.IsDirty, Is.True);
            Assert.That(changedCalls, Is.EqualTo(1));
        }

        [Test]
        public void ProcessPendingRestoration_WhenCurrentTimeIsBeforeNextRestore_DoesNotChangeState()
        {
            const long currentTimestamp = 109;
            var currentTimeSource = (FakeCurrentTimeSource)Container.Resolve<ICurrentTimeSource>();
            currentTimeSource.SetTimestamp(currentTimestamp);

            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = 4,
                    NextRestoreTimestamp = 110
                }
            });

            var energyService = Container.Resolve<IEnergyService>();
            var changedCalls = 0;
            energyService.Changed += () => changedCalls++;

            energyService.ProcessPendingRestoration();

            Assert.That(energyService.Energy, Is.EqualTo(4));
            Assert.That(playerStateProvider.Data.EnergyData.NextRestoreTimestamp, Is.EqualTo(110));
            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(changedCalls, Is.EqualTo(0));
        }

        [Test]
        public void ProcessPendingRestoration_WhenEnoughTimePassed_RestoresMultipleUnitsAndAdvancesTimestamp()
        {
            const long currentTimestamp = 131;
            var currentTimeSource = (FakeCurrentTimeSource)Container.Resolve<ICurrentTimeSource>();
            currentTimeSource.SetTimestamp(currentTimestamp);

            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = 4,
                    NextRestoreTimestamp = 110
                }
            });

            var energyService = Container.Resolve<IEnergyService>();

            energyService.ProcessPendingRestoration();

            Assert.That(energyService.Energy, Is.EqualTo(7));
            Assert.That(playerStateProvider.Data.EnergyData.NextRestoreTimestamp, Is.EqualTo(140));
            Assert.That(energyService.IsRestorationInProgress, Is.True);
        }

        [Test]
        public void ProcessPendingRestoration_WhenRecoveryReachesLimit_CapsEnergyAndStopsRestoration()
        {
            const long currentTimestamp = 200;
            var currentTimeSource = (FakeCurrentTimeSource)Container.Resolve<ICurrentTimeSource>();
            currentTimeSource.SetTimestamp(currentTimestamp);

            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = 9,
                    NextRestoreTimestamp = 110
                }
            });

            var energyService = Container.Resolve<IEnergyService>();

            energyService.ProcessPendingRestoration();

            Assert.That(energyService.Energy, Is.EqualTo(10));
            Assert.That(playerStateProvider.Data.EnergyData.NextRestoreTimestamp, Is.EqualTo(0));
            Assert.That(energyService.IsRestorationInProgress, Is.False);
        }

        [Test]
        public void ProcessPendingRestoration_WhenEnergyIsAtLimitButTimestampIsStale_ClearsTimestamp()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = 10,
                    NextRestoreTimestamp = 110
                }
            });

            var energyService = Container.Resolve<IEnergyService>();
            var changedCalls = 0;
            energyService.Changed += () => changedCalls++;

            energyService.ProcessPendingRestoration();

            Assert.That(energyService.Energy, Is.EqualTo(10));
            Assert.That(playerStateProvider.Data.EnergyData.NextRestoreTimestamp, Is.EqualTo(0));
            Assert.That(playerStateProvider.IsDirty, Is.True);
            Assert.That(changedCalls, Is.EqualTo(1));
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
        private class FakeEnergyConfig : IEnergyConfig
        {
            public int OneEnergyRestorationSeconds => 10;
            public int EnergyRestorationLimit => 10;
        }
    }
}
