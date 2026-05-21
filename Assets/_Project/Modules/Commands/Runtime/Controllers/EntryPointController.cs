using GameKit.Commands.Contracts;
using GameKit.PlayerState.Contracts;
using GameKit.UiFonts.Contracts;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class EntryPointController : IInitializable
    {
        private readonly IUiFontPreloader m_uiFontPreloader;
        private readonly ILaunchCommand m_launchCommand;
        private readonly IEncryptionKeysProvider m_encryptionKeysProvider;

        public EntryPointController(
            IUiFontPreloader uiFontPreloader,
            ILaunchCommand launchCommand,
            IEncryptionKeysProvider encryptionKeysProvider)
        {
            m_uiFontPreloader = uiFontPreloader;
            m_launchCommand = launchCommand;
            m_encryptionKeysProvider = encryptionKeysProvider;
        }

        public void Initialize()
        {
            if (!m_encryptionKeysProvider.HasValues)
            {
                Debug.LogError("Encryption keys are not configured. Open \"GameKit\"/\"Encryption Keys\" and save valid key values.");
                return;
            }

            m_uiFontPreloader.Preload();
            m_launchCommand.Execute();
        }
    }
}
