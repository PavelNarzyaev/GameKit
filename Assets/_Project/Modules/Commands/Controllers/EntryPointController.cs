using GameKit.PlayerState;
using GameKit.UiFonts;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class EntryPointController : IInitializable
    {
        [Inject] private UiFontPreloader m_uiFontPreloader;
        [Inject] private LaunchCommand m_launchCommand;

        public void Initialize()
        {
            if (!EncryptionKeys.HasValues)
            {
                Debug.LogError("Encryption keys are not configured. Open \"GameKit\"/\"Encryption Keys\" and save valid key values.");
                return;
            }

            m_uiFontPreloader.Preload();
            m_launchCommand.Execute();
        }
    }
}
