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
            Container.Bind<ICurrentTimeSource>().To<FakeCurrentTimeSource>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrentTimeProvider>().AsSingle();
            PlayerStateInstaller.Install(Container);
            Container.Bind<PlayerStateCurrenciesGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrenciesService>().AsSingle();
        }

        [Test]
        public void TryAdd_WhenAmountIsPositive_UpdatesValueMarksStateDirtyAndRaisesChanged()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            playerStateProvider.Data = new PlayerStateDto();

            var currencyWallet = Container.Resolve<ICurrencyWallet>();
            var changedCalls = 0;
            currencyWallet.Changed += () => changedCalls++;

            var result = currencyWallet.TryAdd(CurrencyType.Soft, 10);

            Assert.That(result, Is.True);
            Assert.That(currencyWallet.Get(CurrencyType.Soft), Is.EqualTo(10));
            Assert.That(playerStateProvider.IsDirty, Is.True);
            Assert.That(changedCalls, Is.EqualTo(1));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void TryAdd_WhenAmountIsNotPositive_ReturnsFalseWithoutChangingState(int amount)
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            playerStateProvider.Data = new PlayerStateDto
            {
                Currencies = new PlayerCurrenciesDto
                {
                    SoftCurrency = 5
                }
            };

            var currencyWallet = Container.Resolve<ICurrencyWallet>();
            var changedCalls = 0;
            currencyWallet.Changed += () => changedCalls++;

            var result = currencyWallet.TryAdd(CurrencyType.Soft, amount);

            Assert.That(result, Is.False);
            Assert.That(currencyWallet.Get(CurrencyType.Soft), Is.EqualTo(5));
            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(changedCalls, Is.EqualTo(0));
        }

        [Test]
        public void TryAdd_WhenAmountWouldOverflow_ReturnsFalseWithoutChangingState()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            playerStateProvider.Data = new PlayerStateDto
            {
                Currencies = new PlayerCurrenciesDto
                {
                    SoftCurrency = int.MaxValue
                }
            };

            var currencyWallet = Container.Resolve<ICurrencyWallet>();
            var changedCalls = 0;
            currencyWallet.Changed += () => changedCalls++;

            var result = currencyWallet.TryAdd(CurrencyType.Soft, 1);

            Assert.That(result, Is.False);
            Assert.That(currencyWallet.Get(CurrencyType.Soft), Is.EqualTo(int.MaxValue));
            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(changedCalls, Is.EqualTo(0));
        }

        [Test]
        public void Set_WhenValueIsSame_DoesNotMarkStateDirtyOrRaiseChanged()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            playerStateProvider.Data = new PlayerStateDto
            {
                Currencies = new PlayerCurrenciesDto
                {
                    SoftCurrency = 5
                }
            };

            var gateway = Container.Resolve<PlayerStateCurrenciesGateway>();
            var changedCalls = 0;
            gateway.Changed += () => changedCalls++;

            gateway.Set(CurrencyType.Soft, 5);

            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(changedCalls, Is.EqualTo(0));
        }

        [Test]
        public void TrySpend_WhenBalanceIsEnough_UpdatesValueMarksStateDirtyAndRaisesChanged()
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            playerStateProvider.Data = new PlayerStateDto
            {
                Currencies = new PlayerCurrenciesDto
                {
                    HardCurrency = 10
                }
            };

            var currencyWallet = Container.Resolve<ICurrencyWallet>();
            var changedCalls = 0;
            currencyWallet.Changed += () => changedCalls++;

            var result = currencyWallet.TrySpend(CurrencyType.Hard, 4);

            Assert.That(result, Is.True);
            Assert.That(currencyWallet.Get(CurrencyType.Hard), Is.EqualTo(6));
            Assert.That(playerStateProvider.IsDirty, Is.True);
            Assert.That(changedCalls, Is.EqualTo(1));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(11)]
        public void TrySpend_WhenAmountIsInvalidOrBalanceIsInsufficient_ReturnsFalseWithoutChangingState(int amount)
        {
            var playerStateProvider = Container.Resolve<PlayerStateProvider>();
            playerStateProvider.Data = new PlayerStateDto
            {
                Currencies = new PlayerCurrenciesDto
                {
                    HardCurrency = 10
                }
            };

            var currencyWallet = Container.Resolve<ICurrencyWallet>();
            var changedCalls = 0;
            currencyWallet.Changed += () => changedCalls++;

            var result = currencyWallet.TrySpend(CurrencyType.Hard, amount);

            Assert.That(result, Is.False);
            Assert.That(currencyWallet.Get(CurrencyType.Hard), Is.EqualTo(10));
            Assert.That(playerStateProvider.IsDirty, Is.False);
            Assert.That(changedCalls, Is.EqualTo(0));
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
            public long GetTimestamp()
            {
                return 0;
            }
        }

    }
}
