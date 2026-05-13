using GameKit.PlayerState.Contracts;
using NUnit.Framework;

namespace GameKit.PlayerState.Tests
{
    [TestFixture]
    public class PlayerStateValidatorTests
    {
        [Test]
        public void Validate_WhenStateHasUserId_DoesNotThrow()
        {
            var validator = new PlayerStateValidator();
            var state = new PlayerStateDto
            {
                UserId = "user-1"
            };

            Assert.That(() => validator.Validate(state), Throws.Nothing);
        }

        [Test]
        public void Validate_WhenStateIsNull_Throws()
        {
            var validator = new PlayerStateValidator();

            Assert.That(() => validator.Validate(null), Throws.Exception);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Validate_WhenUserIdIsMissing_Throws(string userId)
        {
            var validator = new PlayerStateValidator();
            var state = new PlayerStateDto
            {
                UserId = userId
            };

            Assert.That(() => validator.Validate(state), Throws.Exception);
        }
    }
}
