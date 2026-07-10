using System;
using System.IO;
using GameKit.PlayerState.Contracts;
using NUnit.Framework;

namespace GameKit.PlayerState.Tests
{
    [TestFixture]
    public class PlayerStateStorageTests
    {
        private const string k_StateFileName = "state.dat";

        private string m_storageDirectoryPath;
        private string m_stateFilePath;

        [SetUp]
        public void SetUp()
        {
            m_storageDirectoryPath = Path.Combine(
                Path.GetTempPath(),
                nameof(PlayerStateStorageTests),
                Guid.NewGuid().ToString("N"));

            Directory.CreateDirectory(m_storageDirectoryPath);
            m_stateFilePath = Path.Combine(m_storageDirectoryPath, k_StateFileName);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(m_storageDirectoryPath))
            {
                Directory.Delete(m_storageDirectoryPath, true);
            }
        }

        [Test]
        public void FileStorage_SaveAndLoad_WhenStateIsSaved_ReturnsSavedState()
        {
            var state = CreateState("user-1", 3);
            var storage = CreateFileStorage();

            storage.Save(state);
            var result = storage.Load();

            Assert.That(storage.Exists(), Is.True);
            AssertState(result, "user-1", 3);
        }

        [Test]
        public void FileStorage_Save_WhenFileAlreadyExists_ReplacesSavedState()
        {
            var storage = CreateFileStorage();
            storage.Save(CreateState("old-user", 1));

            storage.Save(CreateState("new-user", 2));

            AssertState(storage.Load(), "new-user", 2);
        }

        [Test]
        public void FileStorage_Delete_WhenFileExists_RemovesSavedFile()
        {
            var storage = CreateFileStorage();
            storage.Save(CreateState("user-1", 3));

            storage.Delete();

            Assert.That(storage.Exists(), Is.False);
        }

        [Test]
        public void FileStorage_Save_WhenStateIsSaved_DoesNotStorePlainText()
        {
            var state = CreateState("user-1", 3);
            var stateJson = new JsonPlayerStateSerializer().Serialize(state);
            var storage = CreateFileStorage();

            storage.Save(state);
            var storedContent = File.ReadAllText(m_stateFilePath);

            Assert.That(storedContent, Is.Not.EqualTo(stateJson));
            Assert.That(storedContent, Does.Not.Contain("user-1"));
            Assert.That(() => Convert.FromBase64String(storedContent), Throws.Nothing);
        }

        [Test]
        public void FileStorage_Load_WhenStoredDataIsInvalid_Throws()
        {
            var storage = CreateFileStorage();
            File.WriteAllText(m_stateFilePath, "not encrypted state");

            Assert.That(storage.Load, Throws.Exception);
        }

        private FilePlayerStateStorage CreateFileStorage()
        {
            return FilePlayerStateStorage.CreateForDirectory(m_storageDirectoryPath);
        }

        private static PlayerStateDto CreateState(string userId, int launchesCounter)
        {
            return new PlayerStateDto
            {
                UserId = userId,
                LaunchesCounter = launchesCounter
            };
        }

        private static void AssertState(PlayerStateDto state, string userId, int launchesCounter)
        {
            Assert.That(state.UserId, Is.EqualTo(userId));
            Assert.That(state.LaunchesCounter, Is.EqualTo(launchesCounter));
        }
    }
}
