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
            Container.Bind<PlayerStateCurrenciesGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrenciesService>().AsSingle();
        }

        [Test]
        public void TryAdd_WhenAmountIsPositive_UpdatesValueAndMarksStateDirty()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            SetCleanState(playerStateProvider, new PlayerStateDto());

            var currencyWallet = Container.Resolve<ICurrencyWallet>();

            var result = currencyWallet.TryAdd(CurrencyType.Soft, 10);

            Assert.That(result, Is.True);
            Assert.That(currencyWallet.Get(CurrencyType.Soft), Is.EqualTo(10));
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

            var currencyWallet = Container.Resolve<ICurrencyWallet>();

            var result = currencyWallet.TryAdd(CurrencyType.Soft, amount);

            Assert.That(result, Is.False);
            Assert.That(currencyWallet.Get(CurrencyType.Soft), Is.EqualTo(5));
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

            var currencyWallet = Container.Resolve<ICurrencyWallet>();

            var result = currencyWallet.TryAdd(CurrencyType.Soft, 1);

            Assert.That(result, Is.False);
            Assert.That(currencyWallet.Get(CurrencyType.Soft), Is.EqualTo(int.MaxValue));
            Assert.That(playerStateProvider.IsDirty, Is.False);
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

            var currencyWallet = Container.Resolve<ICurrencyWallet>();

            var result = currencyWallet.TrySpend(CurrencyType.Hard, 4);

            Assert.That(result, Is.True);
            Assert.That(currencyWallet.Get(CurrencyType.Hard), Is.EqualTo(6));
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

            var currencyWallet = Container.Resolve<ICurrencyWallet>();

            var result = currencyWallet.TrySpend(CurrencyType.Hard, amount);

            Assert.That(result, Is.False);
            Assert.That(currencyWallet.Get(CurrencyType.Hard), Is.EqualTo(10));
            Assert.That(playerStateProvider.IsDirty, Is.False);
        }

        [Test]
        public void Refresh_WhenStateIsReinitializedAfterGatewayWasCreated_UpdatesReactiveProperties()
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
            var gateway = Container.Resolve<PlayerStateCurrenciesGateway>();

            playerStateProvider.Refresh();

            Assert.That(GetCurrentValue(gateway, "SoftCurrency"), Is.EqualTo(100));
            Assert.That(GetCurrentValue(gateway, "HardCurrency"), Is.EqualTo(50));
        }

        private static void SetCleanState(PlayerStateProvider playerStateProvider, PlayerStateDto state)
        {
            playerStateProvider.Replace(state);
            playerStateProvider.Save();
        }

        private static int GetCurrentValue(object owner, string propertyName)
        {
            var property = owner.GetType().GetProperty(propertyName);
            var reactiveProperty = property.GetValue(owner);
            var currentValueProperty = reactiveProperty.GetType().GetProperty("CurrentValue");
            return (int)currentValueProperty.GetValue(reactiveProperty);
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
            public long GetTimestamp()
            {
                return 0;
            }
        }
    }
}
