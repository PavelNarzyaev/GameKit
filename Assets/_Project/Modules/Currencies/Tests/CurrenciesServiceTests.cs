using GameKit.Core.Contracts;
using GameKit.Currencies.Contracts;
using GameKit.CurrentTime;
using GameKit.PlayerState;
using GameKit.PlayerState.Contracts;
using GameKit.ProductionMode;
using JetBrains.Annotations;
using NUnit.Framework;
using Zenject;

namespace GameKit.Currencies.Tests
{
    [TestFixture]
    public class CurrenciesServiceTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            Container.Bind<IPlayerStateStorage>().To<FakePlayerStateStorage>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProductionModeProvider>().AsSingle();
            Container.Bind(typeof(ICurrentTimeSource), typeof(IRealTimeSource)).To<FakeCurrentTimeSource>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrentTimeProvider>().AsSingle();
            PlayerStateInstaller.InstallCore(Container);
            Container.BindInterfacesAndSelfTo<PlayerStateCurrenciesGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrenciesService>().AsSingle();
        }

        [Test]
        public void TryAdd_WhenAmountIsPositive_UpdatesValueAndMarksStateDirty()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto());

            var currenciesService = Container.Resolve<ICurrenciesService>();

            var result = currenciesService.TryAdd(CurrencyType.Soft, 10);

            Assert.That(result, Is.True);
            Assert.That(playerStateProvider.Data.Currencies.SoftCurrency, Is.EqualTo(10));
            Assert.That(playerStateProvider.IsDirty, Is.True);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void TryAdd_WhenAmountIsNotPositive_ReturnsFalseWithoutChangingState(int amount)
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                Currencies = new PlayerCurrenciesDto
                {
                    SoftCurrency = 5
                }
            });

            var currenciesService = Container.Resolve<ICurrenciesService>();

            var result = currenciesService.TryAdd(CurrencyType.Soft, amount);

            Assert.That(result, Is.False);
            Assert.That(playerStateProvider.Data.Currencies.SoftCurrency, Is.EqualTo(5));
            Assert.That(playerStateProvider.IsDirty, Is.False);
        }

        [Test]
        public void TryAdd_WhenAmountWouldOverflow_ReturnsFalseWithoutChangingState()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                Currencies = new PlayerCurrenciesDto
                {
                    SoftCurrency = int.MaxValue
                }
            });

            var currenciesService = Container.Resolve<ICurrenciesService>();

            var result = currenciesService.TryAdd(CurrencyType.Soft, 1);

            Assert.That(result, Is.False);
            Assert.That(playerStateProvider.Data.Currencies.SoftCurrency, Is.EqualTo(int.MaxValue));
            Assert.That(playerStateProvider.IsDirty, Is.False);
        }

        [Test]
        public void TryAdd_WhenServiceWasResolvedBeforeStateRefresh_UsesRefreshedState()
        {
            var currenciesService = Container.Resolve<ICurrenciesService>();
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                Currencies = new PlayerCurrenciesDto
                {
                    SoftCurrency = 5
                }
            });

            var result = currenciesService.TryAdd(CurrencyType.Soft, 1);

            Assert.That(result, Is.True);
            Assert.That(playerStateProvider.Data.Currencies.SoftCurrency, Is.EqualTo(6));
        }

        [Test]
        public void Set_WhenValueIsSame_DoesNotMarkStateDirty()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                Currencies = new PlayerCurrenciesDto
                {
                    SoftCurrency = 5
                }
            });

            var gateway = Container.Resolve<PlayerStateCurrenciesGateway>();

            gateway.Set(CurrencyType.Soft, 5);

            Assert.That(playerStateProvider.IsDirty, Is.False);
        }

        [Test]
        public void TrySpend_WhenBalanceIsEnough_UpdatesValueAndMarksStateDirty()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                Currencies = new PlayerCurrenciesDto
                {
                    HardCurrency = 10
                }
            });

            var currenciesService = Container.Resolve<ICurrenciesService>();

            var result = currenciesService.TrySpend(CurrencyType.Hard, 4);

            Assert.That(result, Is.True);
            Assert.That(playerStateProvider.Data.Currencies.HardCurrency, Is.EqualTo(6));
            Assert.That(playerStateProvider.IsDirty, Is.True);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(11)]
        public void TrySpend_WhenAmountIsInvalidOrBalanceIsInsufficient_ReturnsFalseWithoutChangingState(int amount)
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                Currencies = new PlayerCurrenciesDto
                {
                    HardCurrency = 10
                }
            });

            var currenciesService = Container.Resolve<ICurrenciesService>();

            var result = currenciesService.TrySpend(CurrencyType.Hard, amount);

            Assert.That(result, Is.False);
            Assert.That(playerStateProvider.Data.Currencies.HardCurrency, Is.EqualTo(10));
            Assert.That(playerStateProvider.IsDirty, Is.False);
        }

        [Test]
        public void Reset_WhenStateIsReplacedAfterGatewayWasCreated_UpdatesReactiveProperties()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto
            {
                Currencies = new PlayerCurrenciesDto
                {
                    SoftCurrency = 5,
                    HardCurrency = 10
                }
            });
            var currenciesService = Container.Resolve<ICurrenciesService>();

            playerStateProvider.Reset();
            var addResult = currenciesService.TryAdd(CurrencyType.Soft, 1);
            var spendResult = currenciesService.TrySpend(CurrencyType.Hard, 1);

            Assert.That(addResult, Is.True);
            Assert.That(spendResult, Is.True);
            Assert.That(playerStateProvider.Data.Currencies.SoftCurrency, Is.EqualTo(101));
            Assert.That(playerStateProvider.Data.Currencies.HardCurrency, Is.EqualTo(49));
        }

        private void SetCleanState(PlayerStateProvider playerStateProvider, PlayerStateDto state)
        {
            state.UserId = "test-user";

            var storage = (FakePlayerStateStorage)Container.Resolve<IPlayerStateStorage>();
            storage.Save(state);
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
        private class FakeCurrentTimeSource : ICurrentTimeSource, IRealTimeSource
        {
            public long GetTimestamp()
            {
                return 0;
            }
        }
    }
}
