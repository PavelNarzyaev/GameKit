using System;
using JetBrains.Annotations;
using Mediators;
using MonoBehaviours.Screens;
using Proxies;
using ScriptableObjects.Configs;
using UnityEngine;
using Zenject;

namespace Commands
{
    [UsedImplicitly]
    public class LaunchCommand
    {
        [Inject] private InitializeStateCommand m_initializeStateCommand;
        [Inject] private LocalStateProxy m_localStateProxy;
        [Inject] private PopupsLayerMediator m_popupsLayerMediator;
        [Inject] private ResetUiCommand m_resetUiCommand;

        // TODO: <remove_temporary_code>
        [Inject] private MainConfig m_mainConfig;
        // TODO: </remove_temporary_code>

        public void Execute()
        {
            try
            {
                m_popupsLayerMediator.Reset();

                var isFirstLaunch = !LocalStateProxy.CheckIfExists();
                if (isFirstLaunch)
                {
                    m_initializeStateCommand.Execute();
                }
                else
                {
                    m_localStateProxy.Refresh();
                }

                m_localStateProxy.Data.launchesCounter++;
                m_localStateProxy.MarkAsDirty();

                m_resetUiCommand.Execute();

                // TODO: <remove_temporary_code>
                Debug.Log("Energy restoration limit: " + m_mainConfig.energyRestorationLimit);
                Debug.Log("One energy restoration seconds: " + m_mainConfig.oneEnergyRestorationSeconds);
                // TODO: </remove_temporary_code>
            }
            catch (Exception e)
            {
                Debug.Log($"Launch error: «{e.Message}»");
                m_popupsLayerMediator.Open<ErrorPopupScreen>();
            }
        }
    }
}
