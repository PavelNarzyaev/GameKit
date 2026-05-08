using GameKit.Core;
using JetBrains.Annotations;
using NUnit.Framework;
using Zenject;

namespace GameKit.CurrentTime.Tests
{
    [TestFixture]
    public class CurrentTimeProviderTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            Container.Bind<ICurrentTimeSource>().To<FakeCurrentTimeSource>().AsSingle();
            Container.Bind<CurrentTimeProvider>().AsSingle();
        }

        [Test]
        public void GetTimestamp_WhenCurrentTimeSourceHasTimestamp_ReturnsSourceTimestamp()
        {
            const long startupTimestamp = 1_735_689_600;
            var currentTimeSource = (FakeCurrentTimeSource)Container.Resolve<ICurrentTimeSource>();
            currentTimeSource.SetTimestamp(startupTimestamp);

            var currentTimeProvider = Container.Resolve<CurrentTimeProvider>();

            Assert.That(currentTimeProvider.GetTimestamp(), Is.EqualTo(startupTimestamp));
        }
    }

    [UsedImplicitly]
    public class FakeCurrentTimeSource : ICurrentTimeSource
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
