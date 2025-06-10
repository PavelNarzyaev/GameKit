using NUnit.Framework;
using Proxies;
using Zenject;

namespace Editor.Tests
{
    [TestFixture]
    public class CurrentTimeProxyTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void SetUp() => Container.Bind<CurrentTimeProxy>().AsSingle();

        [Test]
        public void GetTimestamp_WhenCalled_ReturnsPositiveValue()
        {
            // Arrange
            var currentTimeProxy = Container.Resolve<CurrentTimeProxy>();

            // Act
            long timestamp = currentTimeProxy.GetTimestamp();

            // Assert
            Assert.That(timestamp, Is.GreaterThan(0));
        }
    }
}
