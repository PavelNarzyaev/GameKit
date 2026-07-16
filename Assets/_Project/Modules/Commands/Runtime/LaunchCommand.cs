using System;
using GameKit.Commands.Contracts;
using GameKit.PlayerState.Contracts;
using GameKit.UiPopups.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;
using UnityEngine;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class LaunchCommand : ILaunchCommand
    {
        private readonly IPlayerStateProvider m_playerStateProvider;
        private readonly IPopupNavigator m_popupNavigator;
        private readonly IShowInitialUiCommand m_showInitialUiCommand;
        private readonly GameKitTickController m_gameKitTickController;

        public LaunchCommand(
            IPlayerStateProvider playerStateProvider,
            IPopupNavigator popupNavigator,
            IShowInitialUiCommand showInitialUiCommand,
            GameKitTickController gameKitTickController)
        {
            m_playerStateProvider = playerStateProvider;
            m_popupNavigator = popupNavigator;
            m_showInitialUiCommand = showInitialUiCommand;
            m_gameKitTickController = gameKitTickController;
        }

        public void Execute()
        {
            try
            {
                m_playerStateProvider.Refresh();
                m_playerStateProvider.IncrementLaunchesCounter();

                m_showInitialUiCommand.Execute();
                m_gameKitTickController.Launch();
            }
            catch (Exception e)
            {
                Debug.LogError($"Launch error: \"{e.Message}\"");
                m_popupNavigator.Open(UiRegionElementAddressableIds.k_ErrorPopup);
            }
        }
    }
}
