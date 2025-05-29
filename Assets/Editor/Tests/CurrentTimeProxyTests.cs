using NUnit.Framework;
using Zenject;

public class CurrentTimeProxyTests
{
	private DiContainer _diContainer;

	[SetUp]
	public void SetUp()
	{
		_diContainer = new DiContainer();
		_diContainer.Bind<CurrentTimeProxy>().AsSingle();
	}

	[Test]
	public void GetTimestamp_WhenCalled_ReturnsPositiveValue()
	{
		// Arrange
		var currentTimeProxy = _diContainer.Resolve<CurrentTimeProxy>();

		// Act
		var timestamp = currentTimeProxy.GetTimestamp();

		// Assert
		Assert.That(timestamp, Is.GreaterThan(0));
	}
}
