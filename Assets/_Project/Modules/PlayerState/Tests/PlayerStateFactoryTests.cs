using GameKit.Core.Contracts;
using GameKit.CurrentTime;
using NUnit.Framework;
using Zenject;

namespace GameKit.PlayerState.Tests
{
    [TestFixture]
    public class PlayerStateFactoryTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            Container.Bind<ICurrentTimeSource>().To<FakeCurrentTimeSource>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrentTimeProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStateFactory>().AsSingle();
        }

        [Test]
        public void Create_WhenCalled_CreatesStateWithUserIdAndCurrentTimestamp()
        {
            const long currentTimestamp = 1_735_689_600;
            var currentTimeSource = (FakeCurrentTimeSource)Container.Resolve<ICurrentTimeSource>();
            currentTimeSource.SetTimestamp(currentTimestamp);
            var factory = Container.Resolve<IPlayerStateFactory>();

            var result = factory.Create();

            Assert.That(result.UserId, Is.Not.Empty);
            Assert.That(result.FirstLaunchTimestamp, Is.EqualTo(currentTimestamp));
        }

        [Test]
        public void Create_WhenCalled_CreatesStateWithoutDemoResources()
        {
            var factory = Container.Resolve<IPlayerStateFactory>();

            var result = factory.Create();

            Assert.That(result.Currencies.SoftCurrency, Is.EqualTo(0));
            Assert.That(result.Currencies.HardCurrency, Is.EqualTo(0));
            Assert.That(result.EnergyData.Energy, Is.EqualTo(0));
        }

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
