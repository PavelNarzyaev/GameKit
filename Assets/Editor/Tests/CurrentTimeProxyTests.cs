using NUnit.Framework;
using Zenject;

[TestFixture]
public class CurrentTimeProxyTests : ZenjectUnitTestFixture
{
    [SetUp]
    public void SetUp()
    {
        Container.Bind<CurrentTimeProxy>().AsSingle();
    }

    [Test]
    public void GetTimestamp_WhenCalled_ReturnsPositiveValue()
    {
        // Arrange
        var currentTimeProxy = Container.Resolve<CurrentTimeProxy>();

        // Act
        var timestamp = currentTimeProxy.GetTimestamp();

        // Assert
        Assert.That(timestamp, Is.GreaterThan(0));
    }
}
