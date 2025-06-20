using System;
using JetBrains.Annotations;
using Mediators;
using MonoBehaviours.Screens;
using Proxies;
using UnityEngine;
using Zenject;

namespace Commands
{
    [UsedImplicitly]
    public class LaunchCommand
    {
        [Inject] private InitializeStateCommand m_initializeStateCommand;
        [Inject] private LocalStateProxy m_localStateProxy;
        [Inject] private ResetUiCommand m_resetUiCommand;
        [Inject] private PopupsLayerMediator m_popupsLayerMediator;

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
            }
            catch (Exception e)
            {
                Debug.Log($"Launch error: «{e.Message}»");
                m_popupsLayerMediator.Open<ErrorPopupScreen>();
            }
        }
    }
}
