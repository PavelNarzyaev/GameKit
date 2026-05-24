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
        public void FileStorage_SaveAndLoad_WhenStateJsonIsSaved_ReturnsSavedJson()
        {
            const string stateJson = @"{""userId"":""user-1""}";
            var storage = CreateFileStorage();

            storage.Save(stateJson);
            var result = storage.Load();

            Assert.That(storage.Exists(), Is.True);
            Assert.That(result, Is.EqualTo(stateJson));
        }

        [Test]
        public void FileStorage_Save_WhenFileAlreadyExists_ReplacesSavedJson()
        {
            var storage = CreateFileStorage();
            storage.Save(@"{""userId"":""old-user""}");

            storage.Save(@"{""userId"":""new-user""}");

            Assert.That(storage.Load(), Is.EqualTo(@"{""userId"":""new-user""}"));
        }

        [Test]
        public void FileStorage_Delete_WhenFileExists_RemovesSavedFile()
        {
            var storage = CreateFileStorage();
            storage.Save(@"{""userId"":""user-1""}");

            storage.Delete();

            Assert.That(storage.Exists(), Is.False);
        }

        [Test]
        public void EncryptedStorage_SaveLoad_WhenStateJsonIsSaved_ReturnsOriginalJson()
        {
            const string stateJson = @"{""userId"":""user-1"",""launchesCounter"":3}";
            var storage = CreateEncryptedStorage();

            storage.Save(stateJson);
            var result = storage.Load();

            Assert.That(result, Is.EqualTo(stateJson));
        }

        [Test]
        public void EncryptedStorage_Save_WhenStateJsonIsSaved_DoesNotStorePlainText()
        {
            const string stateJson = @"{""userId"":""user-1"",""launchesCounter"":3}";
            var storage = CreateEncryptedStorage();

            storage.Save(stateJson);
            var storedContent = File.ReadAllText(m_stateFilePath);

            Assert.That(storedContent, Is.Not.EqualTo(stateJson));
            Assert.That(storedContent, Does.Not.Contain("user-1"));
            Assert.That(() => Convert.FromBase64String(storedContent), Throws.Nothing);
        }

        [Test]
        public void EncryptedStorage_Load_WhenStoredDataIsInvalid_Throws()
        {
            var storage = CreateEncryptedStorage();
            File.WriteAllText(m_stateFilePath, "not encrypted state");

            Assert.That(storage.Load, Throws.Exception);
        }

        private FilePlayerStateStorage CreateFileStorage()
        {
            return FilePlayerStateStorage.CreateForDirectory(m_storageDirectoryPath);
        }

        private IPlayerStateStorage CreateEncryptedStorage()
        {
            return new EncryptedPlayerStateStorage(CreateFileStorage());
        }

    }
}
