using GameKit.PlayerState.Contracts;
using NUnit.Framework;

namespace GameKit.PlayerState.Tests
{
    [TestFixture]
    public class PlayerStateSerializerTests
    {
        [Test]
        public void SerializeAndDeserialize_WhenStateHasData_PreservesFields()
        {
            var serializer = new JsonPlayerStateSerializer();
            var state = CreateState();

            var json = serializer.Serialize(state);
            var result = serializer.Deserialize(json);

            Assert.That(result.UserId, Is.EqualTo(state.UserId));
            Assert.That(result.FirstLaunchTimestamp, Is.EqualTo(state.FirstLaunchTimestamp));
            Assert.That(result.LaunchesCounter, Is.EqualTo(state.LaunchesCounter));
            Assert.That(result.TimeOffsetSeconds, Is.EqualTo(state.TimeOffsetSeconds));
            Assert.That(result.Currencies.SoftCurrency, Is.EqualTo(state.Currencies.SoftCurrency));
            Assert.That(result.Currencies.HardCurrency, Is.EqualTo(state.Currencies.HardCurrency));
            Assert.That(result.EnergyData.Energy, Is.EqualTo(state.EnergyData.Energy));
            Assert.That(result.EnergyData.NextRestoreTimestamp, Is.EqualTo(state.EnergyData.NextRestoreTimestamp));
        }

        [Test]
        public void Serialize_WhenStateHasData_UsesCamelCaseFieldNames()
        {
            var serializer = new JsonPlayerStateSerializer();

            var json = serializer.Serialize(CreateState());

            Assert.That(json, Does.Contain("\"userId\""));
            Assert.That(json, Does.Contain("\"firstLaunchTimestamp\""));
            Assert.That(json, Does.Contain("\"launchesCounter\""));
            Assert.That(json, Does.Contain("\"timeOffsetSeconds\""));
            Assert.That(json, Does.Contain("\"currencies\""));
            Assert.That(json, Does.Contain("\"softCurrency\""));
            Assert.That(json, Does.Contain("\"hardCurrency\""));
            Assert.That(json, Does.Contain("\"energyData\""));
            Assert.That(json, Does.Contain("\"nextRestoreTimestamp\""));
            Assert.That(json, Does.Not.Contain("\"UserId\""));
            Assert.That(json, Does.Not.Contain("\"FirstLaunchTimestamp\""));
            Assert.That(json, Does.Not.Contain("\"SoftCurrency\""));
            Assert.That(json, Does.Not.Contain("\"EnergyData\""));
        }

        [TestCase("")]
        [TestCase("null")]
        [TestCase("[]")]
        public void Deserialize_WhenJsonDoesNotContainStateObject_Throws(string json)
        {
            var serializer = new JsonPlayerStateSerializer();

            Assert.That(() => serializer.Deserialize(json), Throws.Exception);
        }

        [Test]
        public void Deserialize_WhenJsonDoesNotContainUserId_ReturnsState()
        {
            var serializer = new JsonPlayerStateSerializer();

            var result = serializer.Deserialize("{}");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserId, Is.Null);
        }

        private static PlayerStateDto CreateState()
        {
            return new PlayerStateDto
            {
                UserId = "user-1",
                FirstLaunchTimestamp = 123,
                LaunchesCounter = 4,
                TimeOffsetSeconds = 3600,
                Currencies = new PlayerCurrenciesDto
                {
                    SoftCurrency = 5,
                    HardCurrency = 6
                },
                EnergyData = new PlayerEnergyDataDto
                {
                    Energy = 7,
                    NextRestoreTimestamp = 890
                }
            };
        }
    }
}
