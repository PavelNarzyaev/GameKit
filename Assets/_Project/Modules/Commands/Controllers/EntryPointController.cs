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
        [Inject] private IUiFontPreloader m_uiFontPreloader;
        [Inject] private ILaunchCommand m_launchCommand;
        [Inject] private IEncryptionKeysProvider m_encryptionKeysProvider;

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
